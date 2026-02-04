import { NavLink, useNavigate } from 'react-router-dom';
import { LayoutDashboard, Package, FolderOpen, ShoppingBag, LogOut } from 'lucide-react';

export default function AdminSidebar() {
  const navigate = useNavigate();
  
  const navItems = [
    { path: '/admin', label: 'Dashboard', icon: LayoutDashboard, exact: true },
    { path: '/admin/products', label: 'Products', icon: Package },
    { path: '/admin/categories', label: 'Categories', icon: FolderOpen },
    { path: '/admin/orders', label: 'Orders', icon: ShoppingBag },
  ];

  const handleSignOut = () => {
    navigate('/admin-login');
  };

  return (
    <aside className="hidden lg:flex lg:flex-col w-64 bg-white border-r border-slate-200 h-screen sticky top-0">
      <div className="p-6 border-b border-slate-200">
        <div className="text-xl font-semibold text-slate-900">LUXE Admin</div>
      </div>

      <nav className="flex-1 p-4 space-y-1">
        {navItems.map((item) => {
          const Icon = item.icon;
          
          return (
            <NavLink
              key={item.path}
              to={item.path}
              end={item.exact}
              className={({ isActive }) => `w-full flex items-center gap-3 px-4 py-3 rounded-lg font-semibold text-sm transition-all focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
                isActive
                  ? 'bg-indigo-600 text-white shadow-sm'
                  : 'text-slate-700 hover:bg-slate-50'
              }`}
            >
              <Icon className="size-5" />
              {item.label}
            </NavLink>
          );
        })}
      </nav>

      <div className="p-4 border-t border-slate-200">
        <button 
          onClick={handleSignOut}
          className="w-full flex items-center gap-3 px-4 py-3 rounded-lg font-semibold text-sm text-slate-700 hover:bg-slate-50 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500"
        >
          <LogOut className="size-5" />
          Sign Out
        </button>
      </div>
    </aside>
  );
}
