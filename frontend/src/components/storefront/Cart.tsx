import { useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import QuantitySelector from './QuantitySelector';
import { X, ShoppingBag, Trash2 } from 'lucide-react';
import { useCart } from '../../context/CartContext';
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from '../ui/alert-dialog';

export default function Cart() {
  const navigate = useNavigate();
  const { items, removeFromCart, updateQuantity, clearCart, getSubtotal, getShipping, getTax, getTotal } = useCart();

  const subtotal = getSubtotal();
  const shipping = getShipping();
  const tax = getTax();
  const total = getTotal();

  return (
    <div>
      <Header />
      
      <main className="bg-white min-h-screen">
        <div className="max-w-6xl mx-auto px-4 py-8">
          <div className="mb-8 flex items-center justify-between">
            <div>
              <h1 className="text-slate-900 mb-2 text-3xl font-bold">Shopping Cart</h1>
              <p className="text-slate-600">{items.length} items in your cart</p>
            </div>
            {items.length > 0 && (
              <AlertDialog>
                <AlertDialogTrigger asChild>
                  <button className="flex items-center gap-2 px-4 py-2 text-red-600 hover:text-red-700 hover:bg-red-50 rounded-lg font-semibold transition-colors focus:outline-none focus:ring-2 focus:ring-red-500">
                    <Trash2 className="size-4" />
                    Clear Cart
                  </button>
                </AlertDialogTrigger>
                <AlertDialogContent>
                  <AlertDialogHeader>
                    <AlertDialogTitle>Clear Shopping Cart</AlertDialogTitle>
                    <AlertDialogDescription>
                      Are you sure you want to remove all items from your cart? This action cannot be undone.
                    </AlertDialogDescription>
                  </AlertDialogHeader>
                  <AlertDialogFooter>
                    <AlertDialogCancel>Cancel</AlertDialogCancel>
                    <AlertDialogAction
                      onClick={clearCart}
                      className="bg-red-600 hover:bg-red-700 focus:ring-red-500"
                    >
                      Clear Cart
                    </AlertDialogAction>
                  </AlertDialogFooter>
                </AlertDialogContent>
              </AlertDialog>
            )}
          </div>

          {items.length === 0 ? (
            <div className="text-center py-16">
              <ShoppingBag className="size-16 text-slate-300 mx-auto mb-4" />
              <h2 className="text-2xl font-semibold text-slate-900 mb-2">Your cart is empty</h2>
              <p className="text-slate-600 mb-6">Add some products to get started!</p>
              <button
                onClick={() => navigate('/products')}
                className="bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 transition-colors"
              >
                Continue Shopping
              </button>
            </div>
          ) : (
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
              <div className="lg:col-span-2 space-y-4">
                {items.map((item) => {
                  if (!item.product) return null;

                  return (
                    <div key={item.id} className="border border-slate-200 rounded-xl p-4 shadow-sm hover:shadow-md transition-shadow">
                      <div className="flex gap-4">
                        <div className="bg-slate-100 rounded-lg size-24 flex-shrink-0 overflow-hidden">
                          <img
                            src={item.product.primaryImageUrl}
                            alt={item.product.name}
                            className="w-full h-full object-cover"
                            onError={(e) => {
                              (e.target as HTMLImageElement).src = 'https://placehold.co/96x96/e2e8f0/64748b?text=Product';
                            }}
                          />
                        </div>

                        <div className="flex-1 min-w-0">
                          <div className="flex justify-between gap-4 mb-2">
                            <h3 className="font-semibold text-slate-900">{item.product.name}</h3>
                            <button 
                              onClick={() => removeFromCart(item.id)}
                              className="text-slate-400 hover:text-red-600 focus:outline-none focus:ring-2 focus:ring-red-500 rounded transition-colors"
                              aria-label="Remove item"
                            >
                              <X className="size-5" />
                            </button>
                          </div>
                          
                          <div className="text-sm text-slate-600 mb-3">
                            ${item.product.price.toFixed(2)} each
                          </div>
                          
                          <div className="flex items-center justify-between">
                            <QuantitySelector 
                              quantity={item.quantity}
                              onIncrease={() => updateQuantity(item.id, item.quantity + 1)}
                              onDecrease={() => updateQuantity(item.id, item.quantity - 1)}
                            />
                            <div className="font-semibold text-slate-900">
                              ${(item.product.price * item.quantity).toFixed(2)}
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  );
                })}

                <button 
                  onClick={() => navigate('/products')}
                  className="text-indigo-600 hover:text-indigo-700 font-semibold text-sm hover:underline focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded"
                >
                  ← Continue Shopping
                </button>
              </div>

              <div className="lg:col-span-1">
                <div className="border border-slate-200 rounded-xl p-6 shadow-sm sticky top-24">
                  <h2 className="font-semibold text-slate-900 mb-4">Order Summary</h2>
                  
                  <div className="space-y-3 mb-4 pb-4 border-b border-slate-200">
                    <div className="flex justify-between text-sm">
                      <span className="text-slate-600">Subtotal</span>
                      <span className="text-slate-900">${subtotal.toFixed(2)}</span>
                    </div>
                    <div className="flex justify-between text-sm">
                      <span className="text-slate-600">Shipping</span>
                      <span className="text-slate-900">
                        {shipping === 0 ? (
                          <span className="text-emerald-600 font-semibold">Free</span>
                        ) : (
                          `$${shipping.toFixed(2)}`
                        )}
                      </span>
                    </div>
                    <div className="flex justify-between text-sm">
                      <span className="text-slate-600">Tax (10%)</span>
                      <span className="text-slate-900">${tax.toFixed(2)}</span>
                    </div>
                  </div>

                  {shipping > 0 && (
                    <div className="mb-4 p-3 bg-indigo-50 border border-indigo-200 rounded-lg">
                      <p className="text-xs text-indigo-700">
                        Add <strong>${(100 - subtotal).toFixed(2)}</strong> more to get free shipping!
                      </p>
                    </div>
                  )}

                  <div className="flex justify-between mb-6 text-lg font-semibold">
                    <span className="text-slate-900">Total</span>
                    <span className="text-slate-900">${total.toFixed(2)}</span>
                  </div>

                  <button
                    onClick={() => navigate('/checkout')}
                    className="w-full bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md"
                  >
                    Proceed to Checkout
                  </button>

                  <div className="mt-4 text-center">
                    <button className="text-sm text-slate-600 hover:text-slate-900 hover:underline">
                      Apply Coupon Code
                    </button>
                  </div>
                </div>
              </div>
            </div>
          )}
        </div>
      </main>

      <Footer />
    </div>
  );
}
