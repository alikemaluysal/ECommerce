import { apiClient } from '../client';
import type {
  PaginatedResponse,
  ProductListItemResponse,
  ProductDetailResponse,
  ProductSearchRequest,
  ProductSearchItemResponse,
  ProductSpecification,
  CreateProductRequest,
  CreateProductResponse,
} from '../../types/api';

export const productsApi = {
  async getProducts(params?: {
    page?: number;
    pageSize?: number;
  }): Promise<PaginatedResponse<ProductListItemResponse>> {
    const response = await apiClient.get<PaginatedResponse<ProductListItemResponse>>(
      '/Products',
      {
        params: { 
          PageIndex: params?.page ?? 0, 
          PageSize: params?.pageSize ?? 10 
        },
      }
    );
    return response.data;
  },

  async getProductsByCategory(categoryId: string): Promise<ProductListItemResponse[]> {
    const response = await apiClient.get<ProductListItemResponse[]>(
      `/Products/category/${categoryId}`
    );
    return response.data;
  },

  async getProductById(id: string): Promise<ProductDetailResponse> {
    const response = await apiClient.get<ProductDetailResponse>(`/Products/${id}`);
    return response.data;
  },

  async searchProducts(
    searchRequest: ProductSearchRequest
  ): Promise<PaginatedResponse<ProductSearchItemResponse>> {
    const params: any = {
      'PageRequest.PageIndex': searchRequest.PageRequest.PageIndex,
      'PageRequest.PageSize': searchRequest.PageRequest.PageSize,
    };

    if (searchRequest.SearchTerm) {
      params.SearchTerm = searchRequest.SearchTerm;
    }

    if (searchRequest.CategoryId) {
      params.CategoryId = searchRequest.CategoryId;
    }

    if (searchRequest.MinPrice !== undefined) {
      params.MinPrice = searchRequest.MinPrice;
    }

    if (searchRequest.MaxPrice !== undefined) {
      params.MaxPrice = searchRequest.MaxPrice;
    }

    if (searchRequest.InStock !== undefined) {
      params.InStock = searchRequest.InStock;
    }

    if (searchRequest.SortBy !== undefined) {
      params.SortBy = searchRequest.SortBy;
    }

    const response = await apiClient.get<PaginatedResponse<ProductSearchItemResponse>>(
      '/Products/search',
      { params }
    );
    return response.data;
  },

  async getProductSpecifications(productId: string): Promise<ProductSpecification[]> {
    const response = await apiClient.get<ProductSpecification[]>(
      `/Products/${productId}/specifications`
    );
    return response.data;
  },

  async createProduct(data: CreateProductRequest): Promise<CreateProductResponse> {
    const response = await apiClient.post<CreateProductResponse>('/Products', data);
    return response.data;
  },

  async updateProduct(
    id: string,
    data: import('../../types/api').UpdateProductRequest
  ): Promise<import('../../types/api').UpdateProductResponse> {
    const response = await apiClient.put<import('../../types/api').UpdateProductResponse>(
      `/Products/${id}`,
      data
    );
    return response.data;
  },

  async deleteProduct(id: string): Promise<void> {
    await apiClient.delete(`/Products/${id}`);
  },

  async uploadProductImage(
    productId: string,
    imageFile: File,
    displayOrder: number = 0
  ): Promise<import('../../types/api').UploadProductImageResponse> {
    const formData = new FormData();
    formData.append('ImageFile', imageFile);
    formData.append('DisplayOrder', displayOrder.toString());

    const response = await apiClient.post<import('../../types/api').UploadProductImageResponse>(
      `/Products/${productId}/images`,
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      }
    );
    return response.data;
  },

  async deleteProductImage(
    productId: string,
    imageId: string
  ): Promise<import('../../types/api').DeleteProductImageResponse> {
    const response = await apiClient.delete<import('../../types/api').DeleteProductImageResponse>(
      `/Products/${productId}/images/${imageId}`
    );
    return response.data;
  },

  async setPrimaryImage(
    productId: string,
    imageId: string
  ): Promise<import('../../types/api').SetPrimaryImageResponse> {
    const response = await apiClient.put<import('../../types/api').SetPrimaryImageResponse>(
      `/Products/${productId}/images/${imageId}/set-primary`
    );
    return response.data;
  },

  async createSpecification(
    productId: string,
    data: import('../../types/api').CreateSpecificationRequest
  ): Promise<import('../../types/api').SpecificationResponse> {
    const response = await apiClient.post<import('../../types/api').SpecificationResponse>(
      `/Products/${productId}/specifications`,
      data
    );
    return response.data;
  },

  async updateSpecification(
    productId: string,
    specId: string,
    data: import('../../types/api').UpdateSpecificationRequest
  ): Promise<import('../../types/api').SpecificationResponse> {
    const response = await apiClient.put<import('../../types/api').SpecificationResponse>(
      `/Products/${productId}/specifications/${specId}`,
      data
    );
    return response.data;
  },

  async deleteSpecification(
    productId: string,
    specId: string
  ): Promise<import('../../types/api').DeleteSpecificationResponse> {
    const response = await apiClient.delete<import('../../types/api').DeleteSpecificationResponse>(
      `/Products/${productId}/specifications/${specId}`
    );
    return response.data;
  },
};
