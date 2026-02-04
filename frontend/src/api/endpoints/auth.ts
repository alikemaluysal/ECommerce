import { apiClient, tokenManager } from '../client';
import type {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  RegisterResponse,
  RefreshTokenResponse,
  UserMeResponse,
  RevokeTokenResponse,
} from '../../types/api';

export const authApi = {
  async login(credentials: LoginRequest): Promise<LoginResponse> {
    const response = await apiClient.post<LoginResponse>('/Auth/Login', credentials);
    
    if (response.data.accessToken) {
      tokenManager.setAccessToken(response.data.accessToken.token);
      tokenManager.setTokenExpiration(response.data.accessToken.expirationDate);
    }
    
    return response.data;
  },


  async register(credentials: RegisterRequest): Promise<RegisterResponse> {
    const response = await apiClient.post<RegisterResponse>('/Auth/Register', credentials);
    
    if (response.data.token) {
      tokenManager.setAccessToken(response.data.token);
      tokenManager.setTokenExpiration(response.data.expirationDate);
    }
    
    return response.data;
  },


  async refreshToken(): Promise<RefreshTokenResponse> {
    const response = await apiClient.get<RefreshTokenResponse>('/Auth/RefreshToken');
    
    if (response.data.token) {
      tokenManager.setAccessToken(response.data.token);
      tokenManager.setTokenExpiration(response.data.expirationDate);
    }
    
    return response.data;
  },


  async getMe(): Promise<UserMeResponse> {
    const response = await apiClient.get<UserMeResponse>('/Auth/Me');
    return response.data;
  },


  async revokeToken(): Promise<RevokeTokenResponse> {
    const response = await apiClient.put<RevokeTokenResponse>('/Auth/RevokeToken');
    
    tokenManager.clearTokens();
    
    return response.data;
  },


  async logout(): Promise<void> {
    try {
      await this.revokeToken();
    } catch (error) {
      console.error('Logout error:', error);
    } finally {
      tokenManager.clearTokens();
    }
  },
};
