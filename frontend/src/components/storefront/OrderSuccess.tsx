import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import { CheckCircle2 } from 'lucide-react';
import { ordersApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import type { OrderDetailResponse } from '../../types/api';

export default function OrderSuccess() {
  const { orderNumber } = useParams<{ orderNumber: string }>();
  const navigate = useNavigate();
  const [order, setOrder] = useState<OrderDetailResponse | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (orderNumber) {
      loadOrder();
    }
  }, [orderNumber]);

  const loadOrder = async () => {
    if (!orderNumber) return;
    setLoading(true);
    try {
      const data = await ordersApi.getOrderById(orderNumber);
      setOrder(data);
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div>
        <Header />
        <main className="bg-white">
          <div className="max-w-2xl mx-auto px-4 py-16">
            <div className="text-center text-slate-600">Loading...</div>
          </div>
        </main>
        <Footer />
      </div>
    );
  }

  if (!order) {
    return (
      <div>
        <Header />
        <main className="bg-white">
          <div className="max-w-2xl mx-auto px-4 py-16">
            <div className="text-center text-slate-600">Order not found</div>
          </div>
        </main>
        <Footer />
      </div>
    );
  }

  return (
    <div>
      <Header />
      
      <main className="bg-white">
        <div className="max-w-2xl mx-auto px-4 py-16">
          <div className="border border-slate-200 rounded-xl p-8 shadow-sm text-center">
            <div className="bg-emerald-100 size-16 rounded-full flex items-center justify-center mx-auto mb-6">
              <CheckCircle2 className="size-8 text-emerald-600" />
            </div>
            
            <h1 className="text-slate-900 mb-3">Order Confirmed!</h1>
            <p className="text-slate-600 mb-6">
              Thank you for your purchase. Your order has been successfully placed.
            </p>

            <div className="bg-slate-50 border border-slate-200 rounded-lg p-6 mb-6">
              <div className="text-sm text-slate-600 mb-2">Order Number</div>
              <div className="text-slate-900 font-mono font-semibold">
                #{order.id.slice(0, 8).toUpperCase()}
              </div>
            </div>

            <div className="space-y-3 text-sm text-slate-600 mb-8">
              <p>A confirmation email has been sent to {order.customerEmail}</p>
              <p>You can track your order status using the order number above.</p>
            </div>

            <div className="flex flex-col sm:flex-row gap-3 justify-center">
              <button 
                onClick={() => navigate('/')}
                className="px-6 py-3 bg-indigo-600 text-white rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md"
              >
                Continue Shopping
              </button>
              <button 
                onClick={() => navigate('/order-lookup')}
                className="px-6 py-3 border border-slate-200 text-slate-700 rounded-lg font-semibold hover:bg-slate-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-colors"
              >
                Track Order
              </button>
            </div>
          </div>
        </div>
      </main>

      <Footer />
    </div>
  );
}
