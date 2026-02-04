import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Search, ShoppingCart, User, Heart, LogOut, LogIn, Shield } from 'lucide-react';
import { useCart } from '../../context/CartContext';
import { useAuth } from '../../context/AuthContext';

export default function Header() {
  const navigate = useNavigate();
  const { getItemCount } = useCart();
  const { isAuthenticated, isAdmin, user, logout } = useAuth();
  const itemCount = getItemCount();
  const [searchTerm, setSearchTerm] = useState('');

  const handleAuthClick = () => {
    if (isAuthenticated()) {
      logout();
      navigate('/');
    } else {
      navigate('/login');
    }
  };

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (searchTerm.trim()) {
      navigate(`/products?search=${encodeURIComponent(searchTerm.trim())}`);
    }
  };

  return (
    <header className="sticky top-0 z-40 bg-white border-b border-slate-200/60 shadow-sm">
      <div className="max-w-6xl mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <button 
            onClick={() => navigate('/')}
            className="text-xl font-semibold text-slate-900 hover:text-indigo-600 transition-colors"
          >
            LUXE
          </button>

          <div className="hidden md:flex flex-1 max-w-md mx-8">
            <form onSubmit={handleSearch} className="relative w-full">
              <input
                type="search"
                placeholder="Search products..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="w-full pl-10 pr-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all"
                aria-label="Search products"
              />
              <button type="submit" className="absolute left-3 top-1/2 -translate-y-1/2" aria-label="Search">
                <Search className="size-4 text-slate-400" />
              </button>
            </form>
          </div>

          <div className="flex items-center gap-4">
            {isAuthenticated() && isAdmin() && (
              <button 
                onClick={() => navigate('/admin')}
                className="flex items-center gap-2 px-3 py-2 bg-indigo-600 text-white hover:bg-indigo-700 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded-lg"
                aria-label="Admin Panel"
              >
                <Shield className="size-4" />
                <span className="hidden lg:inline text-sm font-semibold">Admin</span>
              </button>
            )}
            
            <button 
              className="p-2 text-slate-600 hover:text-indigo-600 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded-lg"
              aria-label="Wishlist"
            >
              <Heart className="size-5" />
            </button>
            
            <button 
              onClick={handleAuthClick}
              className="flex items-center gap-2 px-3 py-2 text-slate-600 hover:text-indigo-600 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded-lg"
              aria-label={isAuthenticated() ? 'Sign out' : 'Sign in'}
            >
              {isAuthenticated() ? (
                <>
                  <User className="size-5" />
                  <span className="hidden lg:inline text-sm font-semibold">
                    {user?.email?.split('@')[0] || 'Account'}
                  </span>
                  <LogOut className="size-4" />
                </>
              ) : (
                <>
                  <LogIn className="size-5" />
                  <span className="hidden lg:inline text-sm font-semibold">Sign In</span>
                </>
              )}
            </button>
            
            <button 
              onClick={() => navigate('/cart')}
              className="relative p-2 text-slate-600 hover:text-indigo-600 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500 rounded-lg"
              aria-label="Shopping cart"
            >
              <ShoppingCart className="size-5" />
              {itemCount > 0 && (
                <span className="absolute -top-1 -right-1 bg-indigo-600 text-white text-xs rounded-full size-5 flex items-center justify-center font-semibold">
                  {itemCount}
                </span>
              )}
            </button>
          </div>
        </div>

        {/* Mobile search */}
        <div className="md:hidden pb-3">
          <form onSubmit={handleSearch} className="relative">
            <input
              type="search"
              placeholder="Search products..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full pl-10 pr-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all"
              aria-label="Search products"
            />
            <button type="submit" className="absolute left-3 top-1/2 -translate-y-1/2" aria-label="Search">
              <Search className="size-4 text-slate-400" />
            </button>
          </form>
        </div>
      </div>
    </header>
  );
}
