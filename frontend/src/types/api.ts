export interface PaginationRequest {
  PageIndex: number;
  PageSize: number;
}

export interface PaginatedResponse<T> {
  items: T[];
  index: number;
  size: number;
  count: number;
  pages: number;
  hasPrevious: boolean;
  hasNext: boolean;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
}

export interface AccessToken {
  token: string;
  expirationDate: string;
}

export interface LoginResponse {
  accessToken: AccessToken;
  requiredAuthenticatorType: string | null;
}

export interface RegisterResponse {
  token: string;
  expirationDate: string;
}

export interface RefreshTokenResponse {
  token: string;
  expirationDate: string;
}

export interface UserMeResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  status: boolean;
}

export interface RevokeTokenResponse {
  id: string;
  token: string;
}

export interface TopCategoryResponse {
  id: string;
  name: string;
  productCount: number;
}

export interface ProductImage {
  id: string;
  imageUrl: string;
  isPrimary: boolean;
  displayOrder: number;
}

export interface ProductSpecification {
  id: string;
  key: string;
  value: string;
}

export interface ProductListItemResponse {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  categoryName: string;
  primaryImageUrl: string;
}

export interface ProductDetailResponse {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  primaryImageUrl: string;
  images: ProductImage[];
  specifications: ProductSpecification[];
}

export interface ProductSearchRequest {
  SearchTerm?: string;
  CategoryId?: string;
  MinPrice?: number;
  MaxPrice?: number;
  InStock?: boolean;
  SortBy?: ProductSortBy;
  PageRequest: PaginationRequest;
}

export const ProductSortBy = {
  Default: 0,
  PriceLowToHigh: 1,
  PriceHighToLow: 2,
  Newest: 3,
} as const;

export type ProductSortBy = typeof ProductSortBy[keyof typeof ProductSortBy];

export interface ProductSearchItemResponse {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  categoryName: string;
  primaryImageUrl: string;
  createdDate: string;
}

export interface AddToCartRequest {
  productId: string;
  quantity: number;
}

export interface UpdateCartItemRequest {
  quantity: number;
}

export interface CartItemResponse {
  id: string;
  productId: string;
  quantity: number;
}

export interface RemoveCartItemResponse {
  id: string;
}

export interface ClearCartResponse {
  success: boolean;
  deletedItemsCount: number;
}

export interface GetCartResponse {
  items: CartItemResponse[];
}

export interface CreateOrderRequest {
  shippingAddress: string;
  shippingCity: string;
  shippingCountry: string;
  shippingPostalCode: string;
}

export interface CreateOrderResponse {
  id: string;
  totalAmount: number;
  status: OrderStatus;
  shippingAddress: string;
}

export interface CreateCategoryRequest {
  name: string;
  description: string;
}

export interface UpdateCategoryRequest {
  name: string;
  description: string;
}

export interface CategoryResponse {
  id: string;
  name: string;
  description: string;
}

export interface CategoryDetailResponse {
  id: string;
  name: string;
  description: string;
}

export interface DeleteCategoryResponse {
  id: string;
}

export const OrderStatus = {
  Pending: 0,
  Processing: 1,
  Shipped: 2,
  Delivered: 3,
  Cancelled: 4,
} as const;

export type OrderStatus = typeof OrderStatus[keyof typeof OrderStatus];

export interface OrderListItemResponse {
  id: string;
  totalAmount: number;
  status: OrderStatus;
  shippingAddress: string;
  createdDate: string;
  totalItems: number;
}

export interface OrderDetailResponse {
  id: string;
  userId: string;
  totalAmount: number;
  subtotalAmount: number;
  shippingAmount: number;
  taxAmount: number;
  status: OrderStatus;
  createdDate: string;
  customerEmail: string;
  shippingFullName: string;
  shippingPhone: string;
  shippingAddress: string;
  shippingCity: string;
  shippingCountry: string;
  shippingPostalCode: string;
  items: OrderItemResponse[];
}

export interface OrderItemResponse {
  productId: string;
  productName: string;
  productImageUrl: string;
  quantity: number;
  price: number;
}

export interface UpdateOrderStatusRequest {
  status: OrderStatus;
}

export interface UpdateOrderStatusResponse {
  id: string;
  status: OrderStatus;
  updatedDate: string;
}

export interface CreateProductRequest {
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
}

export interface CreateProductResponse {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  primaryImageUrl: string | null;
}

export interface UpdateProductRequest {
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
}

export interface UpdateProductResponse {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  primaryImageUrl: string | null;
}

export interface UploadProductImageRequest {
  ImageFile: File;
  DisplayOrder: number;
}

export interface UploadProductImageResponse {
  id: string;
  productId: string;
  imageUrl: string;
  isPrimary: boolean;
  displayOrder: number;
}

export interface DeleteProductImageResponse {
  id: string;
}

export interface SetPrimaryImageResponse {
  id: string;
  productId: string;
  imageUrl: string;
  isPrimary: boolean;
}

export interface CreateSpecificationRequest {
  key: string;
  value: string;
}

export interface UpdateSpecificationRequest {
  key: string;
  value: string;
}

export interface SpecificationResponse {
  id: string;
  productId: string;
  key: string;
  value: string;
}

export interface DeleteSpecificationResponse {
  id: string;
}

export interface UserListItemResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  status: boolean;
}

export interface UserDetailResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  status: boolean;
}
