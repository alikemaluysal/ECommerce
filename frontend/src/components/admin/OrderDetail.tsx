import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import StatusBadge from './StatusBadge';
import { ArrowLeft } from 'lucide-react';
import { ordersApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import type { OrderDetailResponse, OrderStatus } from '../../types/api';

export default function OrderDetail() {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const [order, setOrder] = useState<OrderDetailResponse | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (id) {
      loadOrder();
    }
  }, [id]);

  const loadOrder = async () => {
    if (!id) return;
    
    setLoading(true);
    try {
      const data = await ordersApi.getOrderById(id);
      setOrder(data);
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };

  const getStatusLabel = (status: OrderStatus): 'Received' | 'Preparing' | 'Shipped' | 'Completed' | 'Cancelled' => {
    const statusMap: Record<OrderStatus, 'Received' | 'Preparing' | 'Shipped' | 'Completed' | 'Cancelled'> = {
      0: 'Received',
      1: 'Preparing',
      2: 'Shipped',
      3: 'Completed',
      4: 'Cancelled',
    };
    return statusMap[status] || 'Received';
  };

  if (loading || !order) {
    return (
      <div className="flex min-h-screen bg-slate-50">
        <AdminSidebar />
        <div className="flex-1 flex items-center justify-center">
          <div className="text-slate-600">Loading...</div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex min-h-screen bg-slate-50">
      <AdminSidebar />
      
      <div className="flex-1 flex flex-col min-w-0">
        <AdminTopbar title="Order Details" />
        
        <main className="flex-1 p-6">
          <button 
            onClick={() => navigate('/admin/orders')}
            className="inline-flex items-center gap-2 text-sm text-slate-600 hover:text-slate-900 font-semibold mb-6 focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded"
          >
            <ArrowLeft className="size-4" />
            Back to Orders
          </button>

          <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm mb-6">
            <div className="flex flex-col sm:flex-row justify-between gap-4">
              <div>
                <div className="text-sm text-slate-600 mb-1">Order ID</div>
                <div className="text-slate-900 font-mono font-semibold mb-3">#{order.id.slice(0, 8).toUpperCase()}</div>
                <div className="text-sm text-slate-600">Placed on {new Date(order.createdDate).toLocaleString()}</div>
              </div>
              <div className="flex items-start">
                <StatusBadge status={getStatusLabel(order.status)} />
              </div>
            </div>
          </div>

          <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
            <div className="lg:col-span-2 space-y-6">
              <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm">
                <h2 className="font-semibold text-slate-900 mb-4">Customer Information</h2>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4 text-sm">
                  <div>
                    <div className="text-slate-600 mb-1">Email</div>
                    <div className="text-slate-900">{order.customerEmail}</div>
                  </div>
                  <div>
                    <div className="text-slate-600 mb-1">Phone</div>
                    <div className="text-slate-900">{order.shippingPhone}</div>
                  </div>
                </div>
              </div>

              <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm">
                <h2 className="font-semibold text-slate-900 mb-4">Shipping Address</h2>
                <div className="text-sm text-slate-600 space-y-1">
                  <div className="text-slate-900 font-semibold">{order.shippingFullName}</div>
                  <div>{order.shippingAddress}</div>
                </div>
              </div>

              <div className="bg-white border border-slate-200 rounded-xl shadow-sm overflow-hidden">
                <div className="p-6 border-b border-slate-200">
                  <h2 className="font-semibold text-slate-900">Order Items</h2>
                </div>
                <div className="divide-y divide-slate-200">
                  {order.items.map((item) => (
                    <div key={item.productId} className="p-6 flex gap-4">
                      <div className="bg-slate-100 size-16 rounded-lg flex-shrink-0">
                        {item.productImageUrl ? (
                          <img 
                            src={item.productImageUrl} 
                            alt={item.productName}
                            className="w-full h-full object-cover rounded-lg"
                          />
                        ) : (
                          <div className="w-full h-full bg-gradient-to-br from-slate-200 to-slate-100 rounded-lg"></div>
                        )}
                      </div>
                      <div className="flex-1 min-w-0">
                        <div className="font-semibold text-slate-900 mb-1">{item.productName}</div>
                        <div className="text-sm text-slate-600">Quantity: {item.quantity}</div>
                      </div>
                      <div className="text-right">
                        <div className="font-semibold text-slate-900">${(item.price * item.quantity).toFixed(2)}</div>
                        <div className="text-sm text-slate-500">${item.price.toFixed(2)} each</div>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </div>

            <div className="lg:col-span-1 space-y-6">
              <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm sticky top-24">
                <h2 className="font-semibold text-slate-900 mb-4">Order Summary</h2>
                
                <div className="space-y-3 mb-4 pb-4 border-b border-slate-200 text-sm">
                  <div className="flex justify-between">
                    <span className="text-slate-600">Subtotal</span>
                    <span className="text-slate-900">${order.subtotalAmount.toFixed(2)}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-slate-600">Shipping</span>
                    <span className="text-slate-900">${order.shippingAmount.toFixed(2)}</span>
                  </div>
                  <div className="flex justify-between">
                    <span className="text-slate-600">Tax</span>
                    <span className="text-slate-900">${order.taxAmount.toFixed(2)}</span>
                  </div>
                </div>

                <div className="flex justify-between mb-6">
                  <span className="font-semibold text-slate-900">Total</span>
                  <span className="font-semibold text-slate-900">${order.totalAmount.toFixed(2)}</span>
                </div>

                <div className="space-y-3 pt-4 border-t border-slate-200">
                  <label htmlFor="updateStatus" className="block text-sm font-semibold text-slate-900">
                    Update Status
                  </label>
                  <select
                    id="updateStatus"
                    value={order.status}
                    className="w-full px-4 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                  >
                    <option value={0}>Received</option>
                    <option value={1}>Preparing</option>
                    <option value={2}>Shipped</option>
                    <option value={3}>Completed</option>
                    <option value={4}>Cancelled</option>
                  </select>
                  <button className="w-full bg-indigo-600 text-white px-4 py-2 rounded-lg font-semibold text-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md">
                    Update Status
                  </button>
                </div>
              </div>
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}
