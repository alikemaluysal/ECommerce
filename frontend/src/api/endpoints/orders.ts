import { apiClient } from '../client';
import type {
  PaginatedResponse,
  OrderListItemResponse,
  OrderDetailResponse,
  CreateOrderRequest,
  CreateOrderResponse,
  UpdateOrderStatusRequest,
  UpdateOrderStatusResponse,
} from '../../types/api';

export const ordersApi = {
  async checkout(orderData: CreateOrderRequest): Promise<CreateOrderResponse> {
    const response = await apiClient.post<CreateOrderResponse>('/Orders/checkout', orderData);
    return response.data;
  },

  async getUserOrders(
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

  async updateOrderStatus(
    id: string,
    request: UpdateOrderStatusRequest
  ): Promise<UpdateOrderStatusResponse> {
    const response = await apiClient.put<UpdateOrderStatusResponse>(
      `/Orders/${id}/status`,
      request
    );
    return response.data;
  },

  async getAllOrders(
    pageIndex: number = 0,
    pageSize: number = 10
  ): Promise<PaginatedResponse<OrderListItemResponse>> {
    const response = await apiClient.get<PaginatedResponse<OrderListItemResponse>>(
      '/admin/orders',
      {
        params: { PageIndex: pageIndex, PageSize: pageSize },
      }
    );
    return response.data;
  },

  async adminUpdateOrderStatus(
    id: string,
    request: UpdateOrderStatusRequest
  ): Promise<UpdateOrderStatusResponse> {
    const response = await apiClient.put<UpdateOrderStatusResponse>(
      `/admin/orders/${id}/status`,
      request
    );
    return response.data;
  },
};
