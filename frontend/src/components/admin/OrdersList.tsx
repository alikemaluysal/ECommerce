import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import StatusBadge from './StatusBadge';
import { Search, Eye } from 'lucide-react';
import { ordersApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import type { OrderListItemResponse, OrderStatus } from '../../types/api';

export default function OrdersList() {
  const navigate = useNavigate();
  const [orders, setOrders] = useState<OrderListItemResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    loadOrders();
  }, [currentPage]);

  const loadOrders = async () => {
    setLoading(true);
    try {
      const response = await ordersApi.getOrders(currentPage, 10);
      setOrders(response.items);
      setTotalPages(response.pages);
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

  return (
    <div className="flex min-h-screen bg-slate-50">
      <AdminSidebar />
      
      <div className="flex-1 flex flex-col min-w-0">
        <AdminTopbar title="Orders" />
        
        <main className="flex-1 p-6">
          <div className="bg-white border border-slate-200 rounded-xl p-4 shadow-sm mb-6">
            <div className="flex flex-col sm:flex-row gap-4">
              <div className="relative flex-1">
                <input
                  type="search"
                  placeholder="Search orders..."
                  className="w-full pl-10 pr-4 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                />
                <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-slate-400" />
              </div>
              
              <select className="px-4 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500">
                <option>All Status</option>
                <option>Received</option>
                <option>Preparing</option>
                <option>Shipped</option>
                <option>Completed</option>
                <option>Canceled</option>
              </select>
            </div>
          </div>

          {loading ? (
            <div className="text-center py-16 text-slate-600">Loading...</div>
          ) : (
          <div className="bg-white border border-slate-200 rounded-xl shadow-sm overflow-hidden">
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead>
                  <tr className="border-b border-slate-200">
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Order ID
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Customer
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Total
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Date
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Status
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Action
                    </th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-200">
                  {orders.map((order) => (
                    <tr key={order.id} className="hover:bg-slate-50 transition-colors">
                      <td className="px-6 py-4 text-sm font-semibold text-slate-900">
                        #{order.id.slice(0, 8).toUpperCase()}
                      </td>
                      <td className="px-6 py-4 text-sm text-slate-900">
                        {order.shippingAddress}
                      </td>
                      <td className="px-6 py-4 text-sm font-semibold text-slate-900">
                        ${order.totalAmount.toFixed(2)}
                      </td>
                      <td className="px-6 py-4 text-sm text-slate-600">
                        {new Date(order.createdDate).toLocaleDateString()}
                      </td>
                      <td className="px-6 py-4">
                        <StatusBadge status={getStatusLabel(order.status)} />
                      </td>
                      <td className="px-6 py-4">
                        <button 
                          onClick={() => navigate(`/admin/orders/${order.id}`)}
                          className="inline-flex items-center gap-1 text-sm text-indigo-600 hover:text-indigo-700 font-semibold hover:underline focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded"
                        >
                          <Eye className="size-4" />
                          View
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            
            {totalPages > 1 && (
              <div className="flex items-center justify-between px-6 py-4 border-t border-slate-200">
                <button
                  onClick={() => setCurrentPage(p => Math.max(0, p - 1))}
                  disabled={currentPage === 0}
                  className="px-4 py-2 text-sm font-medium text-slate-700 bg-white border border-slate-300 rounded-lg hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  Previous
                </button>
                <span className="text-sm text-slate-700">
                  Page {currentPage + 1} of {totalPages}
                </span>
                <button
                  onClick={() => setCurrentPage(p => Math.min(totalPages - 1, p + 1))}
                  disabled={currentPage >= totalPages - 1}
                  className="px-4 py-2 text-sm font-medium text-slate-700 bg-white border border-slate-300 rounded-lg hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  Next
                </button>
              </div>
            )}
          </div>
          )}
        </main>
      </div>
    </div>
  );
}
