import type { Order, OrderStatus, CheckoutFormData, KPIData } from '../types';
import { mockOrders } from '../data/mockData';

class OrderService {
  private orders: Order[] = [...mockOrders];

  async getAllOrders(): Promise<Order[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const sorted = [...this.orders].sort(
          (a, b) => b.createdAt.getTime() - a.createdAt.getTime()
        );
        resolve(sorted);
      }, 100);
    });
  }

  async getOrderById(id: number): Promise<Order | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const order = this.orders.find((o) => o.id === id);
        resolve(order);
      }, 100);
    });
  }

  async getOrderByNumber(orderNumber: string): Promise<Order | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const order = this.orders.find((o) => o.orderNumber === orderNumber);
        resolve(order);
      }, 100);
    });
  }

  async createOrder(
    checkoutData: CheckoutFormData,
    items: Array<{ productId: number; productName: string; productImage: string; quantity: number; price: number }>,
    totals: { subtotal: number; shipping: number; tax: number; total: number }
  ): Promise<Order> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const orderNumber = `ORD-${Math.floor(Math.random() * 9000) + 1000}`;
        
        const newOrder: Order = {
          id: Math.max(...this.orders.map((o) => o.id), 0) + 1,
          orderNumber,
          customerId: Math.floor(Math.random() * 1000) + 1,
          customerName: checkoutData.shippingAddress.fullName,
          customerEmail: checkoutData.email,
          items: items.map((item, index) => ({
            id: index + 1,
            ...item,
            total: item.price * item.quantity,
          })),
          subtotal: totals.subtotal,
          shipping: totals.shipping,
          tax: totals.tax,
          total: totals.total,
          status: 'Received',
          shippingAddress: checkoutData.shippingAddress,
          billingAddress: checkoutData.sameAsShipping
            ? checkoutData.shippingAddress
            : checkoutData.billingAddress,
          createdAt: new Date(),
          updatedAt: new Date(),
        };

        this.orders.push(newOrder);
        resolve(newOrder);
      }, 200);
    });
  }

  async updateOrderStatus(id: number, status: OrderStatus): Promise<Order | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const index = this.orders.findIndex((o) => o.id === id);
        if (index !== -1) {
          this.orders[index] = {
            ...this.orders[index],
            status,
            updatedAt: new Date(),
          };
          resolve(this.orders[index]);
        } else {
          resolve(undefined);
        }
      }, 100);
    });
  }

  async getRecentOrders(limit: number = 5): Promise<Order[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const sorted = [...this.orders].sort(
          (a, b) => b.createdAt.getTime() - a.createdAt.getTime()
        );
        resolve(sorted.slice(0, limit));
      }, 100);
    });
  }

  async getKPIData(): Promise<KPIData> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const totalRevenue = this.orders.reduce((sum, order) => {
          if (order.status !== 'Cancelled') {
            return sum + order.total;
          }
          return sum;
        }, 0);

        const totalOrders = this.orders.filter((o) => o.status !== 'Cancelled').length;
        
        const totalCustomers = new Set(this.orders.map((o) => o.customerId)).size;

        const conversionRate = 3.24;

        resolve({
          totalRevenue,
          totalOrders,
          totalCustomers,
          conversionRate,
          revenueChange: 12.5,
          ordersChange: 8.2,
          customersChange: 5.4,
          conversionRateChange: 0.4,
        });
      }, 100);
    });
  }

  async filterOrdersByStatus(status: OrderStatus): Promise<Order[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const filtered = this.orders.filter((o) => o.status === status);
        resolve(filtered);
      }, 100);
    });
  }
}

export const orderService = new OrderService();
