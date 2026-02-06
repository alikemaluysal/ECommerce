import { apiClient } from '../client';
import type {
  AddToCartRequest,
  CartItemResponse,
  RemoveCartItemResponse,
  ClearCartResponse,
  GetCartResponse,
  UpdateCartItemRequest,
} from '../../types/api';

export const cartApi = {
  async getCart(): Promise<GetCartResponse> {
    const response = await apiClient.get<GetCartResponse>('/Cart');
    return response.data;
  },

  async addItem(item: AddToCartRequest): Promise<CartItemResponse> {
    const response = await apiClient.post<CartItemResponse>('/Cart/add', item);
    return response.data;
  },

  async updateItem(itemId: string, data: UpdateCartItemRequest): Promise<CartItemResponse> {
    const response = await apiClient.put<CartItemResponse>(`/Cart/update/${itemId}`, data);
    return response.data;
  },

  async removeItem(itemId: string): Promise<RemoveCartItemResponse> {
    const response = await apiClient.delete<RemoveCartItemResponse>(`/Cart/remove/${itemId}`);
    return response.data;
  },

  async clearCart(): Promise<ClearCartResponse> {
    const response = await apiClient.delete<ClearCartResponse>('/Cart/clear');
    return response.data;
  },
};
