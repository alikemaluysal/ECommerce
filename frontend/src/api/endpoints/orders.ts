import { apiClient } from '../client';
import type {
  PaginatedResponse,
  OrderListItemResponse,
  OrderDetailResponse,
  CreateOrderRequest,
  CreateOrderResponse,
} from '../../types/api';

export const ordersApi = {
  async createOrder(orderData: CreateOrderRequest): Promise<CreateOrderResponse> {
    const response = await apiClient.post<CreateOrderResponse>('/Orders/checkout', orderData);
    return response.data;
  },

  async getOrders(
    pageIndex: number = 0,
    pageSize: number = 10
  ): Promise<PaginatedResponse<OrderListItemResponse>> {
    const response = await apiClient.get<PaginatedResponse<OrderListItemResponse>>(
      '/Orders',
      {
        params: { PageIndex: pageIndex, PageSize: pageSize },
      }
    );
    return response.data;
  },

  async getOrderById(id: string): Promise<OrderDetailResponse> {
    const response = await apiClient.get<OrderDetailResponse>(`/Orders/${id}`);
    return response.data;
  },
};
