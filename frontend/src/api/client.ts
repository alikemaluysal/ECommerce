import axios, { type AxiosInstance, AxiosError, type InternalAxiosRequestConfig } from 'axios';
import { ApiError, type ProblemDetails } from '../types/errors';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5278/api';

export const tokenManager = {
  getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  },

  setAccessToken(token: string): void {
    localStorage.setItem('accessToken', token);
  },

  getTokenExpiration(): string | null {
    return localStorage.getItem('tokenExpiration');
  },

  setTokenExpiration(expiration: string): void {
    localStorage.setItem('tokenExpiration', expiration);
  },

  clearTokens(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('tokenExpiration');
  },

  isTokenExpired(): boolean {
    const expiration = this.getTokenExpiration();
    if (!expiration) return true;
    
    const expirationDate = new Date(expiration);
    const now = new Date();
    
    const bufferTime = 30 * 1000; // 30 seconds
    return now.getTime() + bufferTime >= expirationDate.getTime();
  },
};

const apiClient: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true, 
});

apiClient.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const token = tokenManager.getAccessToken();
    
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

let isRefreshing = false;
let refreshSubscribers: ((token: string) => void)[] = [];

function subscribeTokenRefresh(cb: (token: string) => void) {
  refreshSubscribers.push(cb);
}

function onTokenRefreshed(token: string) {
  refreshSubscribers.forEach((cb) => cb(token));
  refreshSubscribers = [];
}

apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as InternalAxiosRequestConfig & { _retry?: boolean };

    if (error.response?.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        return new Promise((resolve) => {
          subscribeTokenRefresh((token: string) => {
            if (originalRequest.headers) {
              originalRequest.headers.Authorization = `Bearer ${token}`;
            }
            resolve(apiClient(originalRequest));
          });
        });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const response = await axios.get(`${API_BASE_URL}/Auth/RefreshToken`, {
          withCredentials: true,
        });

        const { token, expirationDate } = response.data;
        tokenManager.setAccessToken(token);
        tokenManager.setTokenExpiration(expirationDate);

        onTokenRefreshed(token);
        isRefreshing = false;

        if (originalRequest.headers) {
          originalRequest.headers.Authorization = `Bearer ${token}`;
        }
        return apiClient(originalRequest);
      } catch (refreshError) {
        isRefreshing = false;
        refreshSubscribers = [];
        tokenManager.clearTokens();
        
        window.location.href = '/admin/login';
        return Promise.reject(refreshError);
      }
    }

    if (error.response?.data && typeof error.response.data === 'object') {
      const data = error.response.data as any;
      
      if (data.type && data.title && data.status !== undefined) {
        const problemDetails = data as ProblemDetails;
        throw new ApiError(problemDetails, error.response.status);
      }
    }

    return Promise.reject(error);
  }
);

export { apiClient };
