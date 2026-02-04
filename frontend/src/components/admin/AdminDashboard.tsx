import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import KPICard from './KPICard';
import StatusBadge from './StatusBadge';
import { DollarSign, ShoppingBag, Users, TrendingUp, Eye } from 'lucide-react';
import { ordersApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import type { KPIData } from '../../types';
import type { OrderListItemResponse, OrderStatus } from '../../types/api';

// Helper function to convert OrderStatus enum to label
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

export default function AdminDashboard() {
  const navigate = useNavigate();
  const [recentOrders, setRecentOrders] = useState<OrderListItemResponse[]>([]);
  const [kpiData, setKPIData] = useState<KPIData | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadDashboardData();
  }, []);

  const loadDashboardData = async () => {
    setLoading(true);
    try {
      const ordersResponse = await ordersApi.getOrders(0, 5);
      setRecentOrders(ordersResponse.items);
      
      // KPI data için mock data kullanıyoruz (backend'de endpoint yok)
      setKPIData({
        totalRevenue: 0,
        totalOrders: ordersResponse.count,
        totalCustomers: 0,
        conversionRate: 0,
        revenueChange: 0,
        ordersChange: 0,
        customersChange: 0,
        conversionRateChange: 0,
      });
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };

  const handleViewOrder = (orderId: string) => {
    navigate(`/admin/orders/${orderId}`);
  };

  if (loading) {
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
        <AdminTopbar title="Dashboard" />
        
        <main className="flex-1 p-6">
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
            <KPICard
              title="Total Revenue"
              value={`$${kpiData?.totalRevenue.toFixed(2) || 0}`}
              change={`+${kpiData?.revenueChange || 0}% from last month`}
              icon={DollarSign}
              trend="up"
            />
            <KPICard
              title="Orders"
              value={kpiData?.totalOrders.toString() || '0'}
              change={`+${kpiData?.ordersChange || 0}% from last month`}
              icon={ShoppingBag}
              trend="up"
            />
            <KPICard
              title="Customers"
              value={kpiData?.totalCustomers.toString() || '0'}
              change={`+${kpiData?.customersChange || 0}% from last month`}
              icon={Users}
              trend="up"
            />
            <KPICard
              title="Conversion Rate"
              value={`${kpiData?.conversionRate || 0}%`}
              change={`+${kpiData?.conversionRateChange || 0}% from last month`}
              icon={TrendingUp}
              trend="up"
            />
          </div>

          <div className="bg-white border border-slate-200 rounded-xl shadow-sm">
            <div className="p-6 border-b border-slate-200">
              <h2 className="font-semibold text-slate-900">Recent Orders</h2>
            </div>

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
                  {recentOrders.map((order) => (
                    <tr key={order.id} className="hover:bg-slate-50 transition-colors">
                      <td className="px-6 py-4 text-sm font-semibold text-slate-900">
                        #{order.id.slice(0, 8)}
                      </td>
                      <td className="px-6 py-4 text-sm text-slate-600">
                        {order.shippingAddress}
                      </td>
                      <td className="px-6 py-4 text-sm font-semibold text-slate-900">
                        ${order.totalAmount.toFixed(2)}
                      </td>
                      <td className="px-6 py-4 text-sm text-slate-600">
                        {new Date(order.createdDate).toLocaleDateString('en-US', { 
                          month: 'short', 
                          day: 'numeric', 
                          year: 'numeric' 
                        })}
                      </td>
                      <td className="px-6 py-4">
                        <StatusBadge status={getStatusLabel(order.status)} />
                      </td>
                      <td className="px-6 py-4">
                        <button 
                          onClick={() => handleViewOrder(order.id)}
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

            <div className="p-4 border-t border-slate-200 text-center">
              <button 
                onClick={() => navigate('/admin/orders')}
                className="text-sm text-indigo-600 hover:text-indigo-700 font-semibold hover:underline focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded"
              >
                View All Orders →
              </button>
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}
