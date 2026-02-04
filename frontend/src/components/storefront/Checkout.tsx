import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import { CreditCard } from 'lucide-react';
import { useCart } from '../../context/CartContext';
import { useAuth } from '../../context/AuthContext';
import { ordersApi } from '../../api';
import { handleApiError, showSuccess } from '../../utils/errorHandler';
import type { CheckoutFormData } from '../../types';

export default function Checkout() {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();
  const { items, getSubtotal, getShipping, getTax, getTotal, clearCart } = useCart();
  const [loading, setLoading] = useState(false);
  const [formData, setFormData] = useState<CheckoutFormData>({
    email: '',
    shippingAddress: {
      fullName: '',
      phone: '',
      street: '',
      city: '',
      state: '',
      zipCode: '',
      country: 'USA',
    },
    billingAddress: {
      fullName: '',
      phone: '',
      street: '',
      city: '',
      state: '',
      zipCode: '',
      country: 'USA',
    },
    paymentMethod: 'credit_card',
    sameAsShipping: true,
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
      const response = await ordersApi.createOrder({
        shippingAddress: formData.shippingAddress.street,
        shippingCity: formData.shippingAddress.city,
        shippingCountry: formData.shippingAddress.country,
        shippingPostalCode: formData.shippingAddress.zipCode,
      });
      
      await clearCart();
      showSuccess('Siparişiniz başarıyla oluşturuldu!');
      navigate(`/order-success/${response.id}`);
    } catch (error) {
      handleApiError(error, 'Sipariş oluşturulurken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const updateShippingAddress = (field: string, value: string) => {
    setFormData(prev => ({
      ...prev,
      shippingAddress: {
        ...prev.shippingAddress,
        [field]: value,
      },
    }));
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
                  <h2 className="font-semibold text-slate-900 mb-4">Contact Information</h2>
                  <div>
                    <label htmlFor="email" className="block text-sm font-semibold text-slate-700 mb-2">
                      Email Address
                    </label>
                    <input
                      type="email"
                      id="email"
                      required
                      value={formData.email}
                      onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                      className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                    />
                  </div>
                </div>

                <div className="bg-white border border-slate-200 rounded-xl p-6">
                  <h2 className="font-semibold text-slate-900 mb-4">Shipping Address</h2>
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="md:col-span-2">
                      <label htmlFor="fullName" className="block text-sm font-semibold text-slate-700 mb-2">
                        Full Name
                      </label>
                      <input
                        type="text"
                        id="fullName"
                        required
                        value={formData.shippingAddress.fullName}
                        onChange={(e) => updateShippingAddress('fullName', e.target.value)}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                      />
                    </div>

                    <div className="md:col-span-2">
                      <label htmlFor="phone" className="block text-sm font-semibold text-slate-700 mb-2">
                        Phone Number
                      </label>
                      <input
                        type="tel"
                        id="phone"
                        required
                        value={formData.shippingAddress.phone}
                        onChange={(e) => updateShippingAddress('phone', e.target.value)}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                      />
                    </div>

                    <div className="md:col-span-2">
                      <label htmlFor="street" className="block text-sm font-semibold text-slate-700 mb-2">
                        Street Address
                      </label>
                      <input
                        type="text"
                        id="street"
                        required
                        value={formData.shippingAddress.street}
                        onChange={(e) => updateShippingAddress('street', e.target.value)}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                      />
                    </div>

                    <div>
                      <label htmlFor="city" className="block text-sm font-semibold text-slate-700 mb-2">
                        City
                      </label>
                      <input
                        type="text"
                        id="city"
                        required
                        value={formData.shippingAddress.city}
                        onChange={(e) => updateShippingAddress('city', e.target.value)}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                      />
                    </div>

                    <div>
                      <label htmlFor="state" className="block text-sm font-semibold text-slate-700 mb-2">
                        State
                      </label>
                      <input
                        type="text"
                        id="state"
                        required
                        value={formData.shippingAddress.state}
                        onChange={(e) => updateShippingAddress('state', e.target.value)}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                      />
                    </div>

                    <div>
                      <label htmlFor="zipCode" className="block text-sm font-semibold text-slate-700 mb-2">
                        Zip Code
                      </label>
                      <input
                        type="text"
                        id="zipCode"
                        required
                        value={formData.shippingAddress.zipCode}
                        onChange={(e) => updateShippingAddress('zipCode', e.target.value)}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500"
                      />
                    </div>
                  </div>
                </div>

                <div className="bg-white border border-slate-200 rounded-xl p-6">
                  <h2 className="font-semibold text-slate-900 mb-4">Payment Method</h2>
                  <div className="space-y-3">
                    <label className="flex items-center gap-3 p-4 border-2 border-indigo-600 rounded-lg cursor-pointer">
                      <input
                        type="radio"
                        name="payment"
                        value="credit_card"
                        checked={formData.paymentMethod === 'credit_card'}
                        onChange={(e) => setFormData({ ...formData, paymentMethod: e.target.value as any })}
                        className="size-4"
                      />
                      <CreditCard className="size-5 text-slate-600" />
                      <span className="font-semibold text-slate-900">Credit Card</span>
                    </label>

                    <label className="flex items-center gap-3 p-4 border-2 border-slate-200 rounded-lg cursor-pointer">
                      <input
                        type="radio"
                        name="payment"
                        value="paypal"
                        onChange={(e) => setFormData({ ...formData, paymentMethod: e.target.value as any })}
                        className="size-4"
                      />
                      <span className="font-semibold text-slate-900">PayPal</span>
                    </label>

                    <label className="flex items-center gap-3 p-4 border-2 border-slate-200 rounded-lg cursor-pointer">
                      <input
                        type="radio"
                        name="payment"
                        value="cash_on_delivery"
                        onChange={(e) => setFormData({ ...formData, paymentMethod: e.target.value as any })}
                        className="size-4"
                      />
                      <span className="font-semibold text-slate-900">Cash on Delivery</span>
                    </label>
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
