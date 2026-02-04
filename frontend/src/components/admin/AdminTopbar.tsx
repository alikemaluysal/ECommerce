import { Bell, User, LogOut, Store } from 'lucide-react';
import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router-dom';

interface AdminTopbarProps {
  title: string;
}

export default function AdminTopbar({ title }: AdminTopbarProps) {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = async () => {
    await logout();
    navigate('/');
  };

  return (
    <header className="bg-white border-b border-slate-200 sticky top-0 z-30">
      <div className="flex items-center justify-between h-16 px-6">
        <h1 className="font-semibold text-slate-900">{title}</h1>

        <div className="flex items-center gap-4">
          <button 
            onClick={() => navigate('/')}
            className="flex items-center gap-2 px-3 py-2 text-slate-600 hover:text-indigo-600 hover:bg-indigo-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500"
            aria-label="Storefront"
            title="Go to Storefront"
          >
            <Store className="size-5" />
            <span className="hidden md:inline text-sm font-semibold">Storefront</span>
          </button>

          <button 
            className="p-2 text-slate-600 hover:text-slate-900 hover:bg-slate-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500"
            aria-label="Notifications"
          >
            <Bell className="size-5" />
          </button>

          <div className="flex items-center gap-3 pl-4 border-l border-slate-200">
            <div className="text-right hidden sm:block">
              <div className="text-sm font-semibold text-slate-900">
                {user?.firstName || user?.lastName 
                  ? `${user.firstName} ${user.lastName}`.trim() 
                  : 'Admin'}
              </div>
              <div className="text-xs text-slate-500">{user?.email || 'admin@store.com'}</div>
            </div>
            <div className="bg-slate-100 size-10 rounded-full flex items-center justify-center">
              <User className="size-5 text-slate-600" />
            </div>
            <button
              onClick={handleLogout}
              className="p-2 text-slate-600 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-red-500"
              aria-label="Logout"
              title="Logout"
            >
              <LogOut className="size-5" />
            </button>
          </div>
        </div>
      </div>

      <div className="lg:hidden px-4 pb-3 flex gap-2 overflow-x-auto">
        <button className="px-3 py-1.5 bg-indigo-600 text-white text-xs font-semibold rounded-lg whitespace-nowrap">
          Dashboard
        </button>
        <button className="px-3 py-1.5 border border-slate-200 text-slate-700 text-xs font-semibold rounded-lg hover:bg-slate-50 whitespace-nowrap">
          Products
        </button>
        <button className="px-3 py-1.5 border border-slate-200 text-slate-700 text-xs font-semibold rounded-lg hover:bg-slate-50 whitespace-nowrap">
          Categories
        </button>
        <button className="px-3 py-1.5 border border-slate-200 text-slate-700 text-xs font-semibold rounded-lg hover:bg-slate-50 whitespace-nowrap">
          Orders
        </button>
      </div>
    </header>
  );
}
