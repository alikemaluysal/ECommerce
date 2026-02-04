import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import { useCart } from '../../context/CartContext';
import { useAuth } from '../../context/AuthContext';
import { ordersApi } from '../../api';
import { handleApiError, showSuccess } from '../../utils/errorHandler';

export default function Checkout() {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();
  const { items, getSubtotal, getShipping, getTax, getTotal, clearCart } = useCart();
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState({
    shippingAddress: '',
    shippingCity: '',
    shippingCountry: '',
    shippingPostalCode: '',
  });

  useEffect(() => {
    if (!isAuthenticated()) {
      navigate('/login', { state: { from: '/checkout' } });
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      const response = await ordersApi.checkout(formData);
      
      await clearCart();
      showSuccess('Siparişiniz başarıyla oluşturuldu!');
      navigate(`/order-success/${response.id}`);
    } catch (error) {
      handleApiError(error, 'Sipariş oluşturulurken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  if (items.length === 0) {
    return (
      <div>
        <Header />
        <div className="max-w-6xl mx-auto px-4 py-16 text-center">
          <h2 className="text-2xl font-semibold text-slate-900 mb-4">Your cart is empty</h2>
          <button
            onClick={() => navigate('/products')}
            className="bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700"
          >
            Continue Shopping
          </button>
        </div>
        <Footer />
      </div>
    );
  }

  return (
    <div>
      <Header />
      
      <main className="bg-white min-h-screen">
        <div className="max-w-6xl mx-auto px-4 py-8">
          <h1 className="text-3xl font-bold text-slate-900 mb-8">Checkout</h1>

          <form onSubmit={handleSubmit}>
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
              <div className="lg:col-span-2 space-y-6">
                <div className="bg-white border border-slate-200 rounded-xl p-6">
                  <h2 className="font-semibold text-slate-900 mb-4">Shipping Information</h2>
                  <div className="space-y-4">
                    <div>
                      <label htmlFor="shippingAddress" className="block text-sm font-semibold text-slate-700 mb-2">
                        Address
                      </label>
                      <input
                        type="text"
                        id="shippingAddress"
                        required
                        value={formData.shippingAddress}
                        onChange={(e) => setFormData({ ...formData, shippingAddress: e.target.value })}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                        placeholder="123 Main St"
                      />
                    </div>

                    <div>
                      <label htmlFor="shippingCity" className="block text-sm font-semibold text-slate-700 mb-2">
                        City
                      </label>
                      <input
                        type="text"
                        id="shippingCity"
                        required
                        value={formData.shippingCity}
                        onChange={(e) => setFormData({ ...formData, shippingCity: e.target.value })}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                        placeholder="New York"
                      />
                    </div>

                    <div>
                      <label htmlFor="shippingCountry" className="block text-sm font-semibold text-slate-700 mb-2">
                        Country
                      </label>
                      <input
                        type="text"
                        id="shippingCountry"
                        required
                        value={formData.shippingCountry}
                        onChange={(e) => setFormData({ ...formData, shippingCountry: e.target.value })}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                        placeholder="USA"
                      />
                    </div>

                    <div>
                      <label htmlFor="shippingPostalCode" className="block text-sm font-semibold text-slate-700 mb-2">
                        Postal Code
                      </label>
                      <input
                        type="text"
                        id="shippingPostalCode"
                        required
                        value={formData.shippingPostalCode}
                        onChange={(e) => setFormData({ ...formData, shippingPostalCode: e.target.value })}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                        placeholder="10001"
                      />
                    </div>
                  </div>
                </div>
              </div>

              <div className="lg:col-span-1">
                <div className="bg-white border border-slate-200 rounded-xl p-6 sticky top-24">
                  <h2 className="font-semibold text-slate-900 mb-4">Order Summary</h2>
                  
                  <div className="space-y-3 mb-4">
                    {items.map((item) => (
                      <div key={item.id} className="flex gap-3">
                        <img
                          src={item.product?.primaryImageUrl || 'https://placehold.co/64x64/e2e8f0/64748b?text=Product'}
                          alt={item.product?.name || 'Product'}
                          className="size-16 bg-slate-100 rounded-lg object-cover"
                        />
                        <div className="flex-1 min-w-0">
                          <p className="text-sm font-semibold text-slate-900 truncate">{item.product?.name}</p>
                          <p className="text-sm text-slate-600">Qty: {item.quantity}</p>
                        </div>
                      </div>
                    ))}
                  </div>

                  <div className="space-y-3 mb-4 pb-4 border-t border-slate-200 pt-4">
                    <div className="flex justify-between text-sm">
                      <span className="text-slate-600">Subtotal</span>
                      <span className="text-slate-900">${getSubtotal().toFixed(2)}</span>
                    </div>
                    <div className="flex justify-between text-sm">
                      <span className="text-slate-600">Shipping</span>
                      <span className="text-slate-900">
                        {getShipping() === 0 ? (
                          <span className="text-emerald-600 font-semibold">Free</span>
                        ) : (
                          `$${getShipping().toFixed(2)}`
                        )}
                      </span>
                    </div>
                    <div className="flex justify-between text-sm">
                      <span className="text-slate-600">Tax</span>
                      <span className="text-slate-900">${getTax().toFixed(2)}</span>
                    </div>
                  </div>

                  <div className="flex justify-between mb-6 text-lg font-semibold border-t border-slate-200 pt-4">
                    <span className="text-slate-900">Total</span>
                    <span className="text-slate-900">${getTotal().toFixed(2)}</span>
                  </div>

                  <button
                    type="submit"
                    disabled={loading}
                    className="w-full bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    {loading ? 'Placing Order...' : 'Place Order'}
                  </button>
                </div>
              </div>
            </div>
          </form>
        </div>
      </main>

      <Footer />
    </div>
  );
}
