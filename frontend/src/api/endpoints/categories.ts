import { apiClient } from '../client';
import type { 
  TopCategoryResponse,
  PaginatedResponse,
  CategoryResponse,
  CategoryDetailResponse,
  CreateCategoryRequest,
  UpdateCategoryRequest,
  DeleteCategoryResponse,
} from '../../types/api';

export const categoriesApi = {
  async getTopCategories(): Promise<TopCategoryResponse[]> {
    const response = await apiClient.get<TopCategoryResponse[]>('/Categories/top-categories');
    return response.data;
  },

  async getCategories(
    pageIndex: number = 0,
    pageSize: number = 10
  ): Promise<PaginatedResponse<CategoryResponse>> {
    const response = await apiClient.get<PaginatedResponse<CategoryResponse>>(
      '/Categories',
      {
        params: { PageIndex: pageIndex, PageSize: pageSize },
      }
    );
    return response.data;
  },

  async getCategoryById(id: string): Promise<CategoryDetailResponse> {
    const response = await apiClient.get<CategoryDetailResponse>(`/Categories/${id}`);
    return response.data;
  },

  async createCategory(data: CreateCategoryRequest): Promise<CategoryResponse> {
    const response = await apiClient.post<CategoryResponse>('/Categories', data);
    return response.data;
  },

  async updateCategory(id: string, data: UpdateCategoryRequest): Promise<CategoryResponse> {
    const response = await apiClient.put<CategoryResponse>(`/Categories/${id}`, data);
    return response.data;
  },

  async deleteCategory(id: string): Promise<DeleteCategoryResponse> {
    const response = await apiClient.delete<DeleteCategoryResponse>(`/Categories/${id}`);
    return response.data;
  },
};
