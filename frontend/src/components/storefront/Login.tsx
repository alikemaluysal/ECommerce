import { useState } from 'react';
import { useNavigate, useLocation, Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import Header from './Header';
import Footer from './Footer';
import { ShoppingBag, Lock } from 'lucide-react';

export default function Login() {
  const navigate = useNavigate();
  const location = useLocation();
  const { login, register: registerUser, isAdmin } = useAuth();
  const [isLogin, setIsLogin] = useState(true);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const from = (location.state as any)?.from || '/';

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      if (isLogin) {
        const success = await login(email, password);
        if (success) {
          // Admin kullanıcıları admin paneline yönlendir
          if (isAdmin()) {
            navigate('/admin', { replace: true });
          } else {
            navigate(from, { replace: true });
          }
        }
      } else {
        const success = await registerUser(email, password);
        if (success) {
          navigate(from, { replace: true });
        }
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div>
      <Header />
      
      <main className="bg-gradient-to-br from-slate-50 via-white to-slate-50 min-h-screen py-12">
        {/* Subtle background pattern */}
        <div className="absolute inset-0 bg-[radial-gradient(#e5e7eb_1px,transparent_1px)] [background-size:16px_16px] opacity-30"></div>
        
        <div className="relative max-w-md mx-auto px-4">
          <div className="bg-white border border-slate-200 rounded-xl shadow-sm p-8">
            {/* Icon & Title */}
            <div className="text-center mb-8">
              <div className="inline-flex items-center justify-center w-16 h-16 bg-indigo-100 rounded-full mb-4">
                <ShoppingBag className="size-8 text-indigo-600" />
              </div>
              <h1 className="text-2xl font-bold text-slate-900 mb-2">
                {isLogin ? 'Welcome Back' : 'Create Account'}
              </h1>
              <p className="text-sm text-slate-600">
                {isLogin 
                  ? 'Sign in to access your cart and orders' 
                  : 'Join us to start shopping'}
              </p>
            </div>

            {/* Tabs */}
            <div className="flex gap-2 mb-6 p-1 bg-slate-100 rounded-lg">
              <button
                onClick={() => setIsLogin(true)}
                className={`flex-1 py-2 px-4 rounded-md font-semibold text-sm transition-all ${
                  isLogin 
                    ? 'bg-white text-slate-900 shadow-sm' 
                    : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                Sign In
              </button>
              <button
                onClick={() => setIsLogin(false)}
                className={`flex-1 py-2 px-4 rounded-md font-semibold text-sm transition-all ${
                  !isLogin 
                    ? 'bg-white text-slate-900 shadow-sm' 
                    : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                Sign Up
              </button>
            </div>

            {/* Form */}
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label htmlFor="email" className="block text-sm font-semibold text-slate-900 mb-2">
                  Email Address
                </label>
                <input
                  id="email"
                  type="email"
                  placeholder="you@example.com"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                  disabled={isLoading}
                  className="w-full px-4 py-3 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all disabled:opacity-50"
                />
              </div>

              <div>
                <label htmlFor="password" className="block text-sm font-semibold text-slate-900 mb-2">
                  Password
                </label>
                <input
                  id="password"
                  type="password"
                  placeholder="••••••••"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                  disabled={isLoading}
                  className="w-full px-4 py-3 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all disabled:opacity-50"
                />
              </div>

              {isLogin && (
                <div className="flex items-center justify-between text-sm">
                  <label className="flex items-center gap-2 cursor-pointer">
                    <input type="checkbox" className="rounded border-slate-300 text-indigo-600" />
                    <span className="text-slate-600">Remember me</span>
                  </label>
                  <button type="button" className="text-indigo-600 hover:text-indigo-700 font-semibold">
                    Forgot password?
                  </button>
                </div>
              )}

              <button 
                type="submit"
                disabled={isLoading}
                className="w-full bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {isLoading 
                  ? (isLogin ? 'Signing in...' : 'Creating account...') 
                  : (isLogin ? 'Sign In' : 'Create Account')}
              </button>
            </form>

            {/* Footer */}
            <div className="mt-6 pt-6 border-t border-slate-200">
              <div className="flex items-center justify-center gap-2 text-sm text-slate-500">
                <Lock className="size-4" />
                <span>Secure connection with SSL encryption</span>
              </div>
            </div>

            {/* Admin Link */}
            <div className="mt-4 text-center">
              <Link 
                to="/admin/login" 
                className="text-xs text-slate-500 hover:text-slate-700 transition-colors"
              >
                Admin Panel →
              </Link>
            </div>
          </div>

          {/* Return to shopping */}
          <div className="text-center mt-6">
            <Link 
              to="/" 
              className="text-sm text-slate-600 hover:text-slate-900 font-semibold transition-colors"
            >
              ← Continue Shopping
            </Link>
          </div>
        </div>
      </main>

      <Footer />
    </div>
  );
}
