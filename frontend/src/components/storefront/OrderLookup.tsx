import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import { Search, Package } from 'lucide-react';
import { ordersApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';

export default function OrderLookup() {
  const navigate = useNavigate();
  const [orderId, setOrderId] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!orderId.trim()) return;

    setLoading(true);
    try {
      await ordersApi.getOrderById(orderId.trim());
      navigate(`/order-success/${orderId.trim()}`);
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };
  return (
    <div>
      <Header />
      
      <main className="bg-white">
        <div className="max-w-2xl mx-auto px-4 py-16">
          <div className="text-center mb-8">
            <h1 className="text-slate-900 mb-3">Track Your Order</h1>
            <p className="text-slate-600">
              Enter your order details to check the status
            </p>
          </div>

          {/* Order Lookup Form */}
          <div className="border border-slate-200 rounded-xl p-8 shadow-sm mb-8">
            <form onSubmit={handleSearch} className="space-y-4">
              <div>
                <label htmlFor="orderNumber" className="block text-sm font-semibold text-slate-900 mb-2">
                  Order ID
                </label>
                <input
                  id="orderNumber"
                  type="text"
                  value={orderId}
                  onChange={(e) => setOrderId(e.target.value)}
                  placeholder="Enter your order ID"
                  required
                  className="w-full px-4 py-3 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all"
                />
                <p className="mt-1 text-xs text-slate-500">
                  You can find this in your order confirmation email
                </p>
              </div>

              <button 
                type="submit"
                disabled={loading || !orderId.trim()}
                className="w-full bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md flex items-center justify-center gap-2 disabled:opacity-50 disabled:cursor-not-allowed"
              >
                <Search className="size-5" />
                {loading ? 'Searching...' : 'Find My Order'}
              </button>
            </form>
          </div>

          <div className="border border-slate-200 rounded-xl p-12 shadow-sm text-center bg-slate-50">
            <div className="bg-slate-200 size-16 rounded-full flex items-center justify-center mx-auto mb-4">
              <Package className="size-8 text-slate-400" />
            </div>
            <h3 className="font-semibold text-slate-900 mb-2">No Order Found</h3>
            <p className="text-sm text-slate-600 mb-6">
              Enter your order details above to track your shipment
            </p>
            <button 
              onClick={() => navigate('/')}
              className="text-indigo-600 hover:text-indigo-700 font-semibold text-sm hover:underline focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded"
            >
              Continue Shopping
            </button>
          </div>
        </div>
      </main>

      <Footer />
    </div>
  );
}
