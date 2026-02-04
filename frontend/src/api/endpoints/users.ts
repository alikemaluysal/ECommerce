import { apiClient } from '../client';
import type {
  PaginatedResponse,
  UserListItemResponse,
  UserDetailResponse,
} from '../../types/api';

export const usersApi = {

  async getUsers(
    pageIndex: number = 0,
    pageSize: number = 10
  ): Promise<PaginatedResponse<UserListItemResponse>> {
    const response = await apiClient.get<PaginatedResponse<UserListItemResponse>>(
      '/Users',
      {
        params: { PageIndex: pageIndex, PageSize: pageSize },
      }
    );
    return response.data;
  },


  async getUserById(id: string): Promise<UserDetailResponse> {
    const response = await apiClient.get<UserDetailResponse>(`/Users/${id}`);
    return response.data;
  },
};
