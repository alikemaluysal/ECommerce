export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;
  categoryName: string;
  primaryImageUrl: string;
  images?: ProductImage[];
  specifications?: ProductSpecification[];
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

export interface Category {
  name: string;
  productCount: number;
}

export interface CartItem {
  id: string;
  productId: string;
  product?: Product;
  quantity: number;
}

export interface Order {
  id: string;
  orderNumber: string;
  customerId: string;
  customerName: string;
  customerEmail: string;
  items: OrderItem[];
  subtotal: number;
  shipping: number;
  tax: number;
  total: number;
  status: OrderStatus;
  shippingAddress: Address;
  billingAddress: Address;
  createdAt: Date;
  updatedAt: Date;
}

export interface OrderItem {
  id: string;
  productId: string;
  productName: string;
  productImage: string;
  quantity: number;
  price: number;
  total: number;
}

export type OrderStatus = 'Received' | 'Preparing' | 'Shipped' | 'Completed' | 'Cancelled';

export interface Address {
  fullName: string;
  phone: string;
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: 'Admin' | 'User';
  status: boolean;
}

export interface CheckoutFormData {
  email: string;
  shippingAddress: Address;
  billingAddress: Address;
  paymentMethod: 'credit_card' | 'paypal' | 'cash_on_delivery';
  sameAsShipping: boolean;
}

export interface KPIData {
  totalRevenue: number;
  totalOrders: number;
  totalCustomers: number;
  conversionRate: number;
  revenueChange: number;
  ordersChange: number;
  customersChange: number;
  conversionRateChange: number;
}
