import { createContext, useContext, useState, useEffect } from 'react';
import type { ReactNode } from 'react';
import type { User } from '../types';
import { authApi, tokenManager } from '../api';
import { handleApiError, showSuccess } from '../utils/errorHandler';
import { hasRole } from '../utils/jwt';

interface AuthContextType {
  user: User | null;
  isLoading: boolean;
  login: (email: string, password: string) => Promise<boolean>;
  register: (email: string, password: string) => Promise<boolean>;
  logout: () => Promise<void>;
  isAuthenticated: () => boolean;
  isAdmin: () => boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const initAuth = async () => {
      const token = tokenManager.getAccessToken();
      
      if (token && !tokenManager.isTokenExpired()) {
        try {
          const userMe = await authApi.getMe();
          const isAdmin = hasRole(token, 'Admin');
          setUser({
            id: userMe.id,
            email: userMe.email,
            firstName: userMe.firstName,
            lastName: userMe.lastName,
            role: isAdmin ? 'Admin' : 'User',
            status: userMe.status,
          });
        } catch (error) {
          tokenManager.clearTokens();
        }
      }
      
      setIsLoading(false);
    };

    initAuth();
  }, []);

  const login = async (email: string, password: string): Promise<boolean> => {
    try {
      await authApi.login({ email, password });
      const token = tokenManager.getAccessToken();
      const userMe = await authApi.getMe();
      const isAdmin = hasRole(token, 'Admin');
      
      setUser({
        id: userMe.id,
        email: userMe.email,
        firstName: userMe.firstName,
        lastName: userMe.lastName,
        role: isAdmin ? 'Admin' : 'User',
        status: userMe.status,
      });
      
      showSuccess('Giriş başarılı!');
      return true;
    } catch (error) {
      handleApiError(error);
      return false;
    }
  };

  const register = async (email: string, password: string): Promise<boolean> => {
    try {
      await authApi.register({ email, password });
      
      const userMe = await authApi.getMe();
      setUser({
        id: userMe.id,
        email: userMe.email,
        firstName: userMe.firstName,
        lastName: userMe.lastName,
        role: 'User',
        status: userMe.status,
      });
      
      showSuccess('Kayıt başarılı!');
      return true;
    } catch (error) {
      handleApiError(error);
      return false;
    }
  };

  const logout = async () => {
    try {
      await authApi.logout();
      setUser(null);
      showSuccess('Çıkış yapıldı.');
    } catch (error) {
      handleApiError(error);
      setUser(null);
    }
  };

  const isAuthenticated = () => {
    return user !== null && tokenManager.getAccessToken() !== null;
  };

  const isAdmin = () => {
    return user?.role === 'Admin';
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        isLoading,
        login,
        register,
        logout,
        isAuthenticated,
        isAdmin,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider');
  }
  return context;
}
