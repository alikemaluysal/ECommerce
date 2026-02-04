import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Toaster } from 'sonner';
import { CartProvider } from './context/CartContext';
import { AuthProvider } from './context/AuthContext';
import { ConfirmDialogProvider } from './context/ConfirmDialogContext';
import ProtectedRoute from './components/ProtectedRoute';
import Home from './components/storefront/Home';
import ProductListing from './components/storefront/ProductListing';
import ProductDetail from './components/storefront/ProductDetail';
import Cart from './components/storefront/Cart';
import Checkout from './components/storefront/Checkout';
import OrderSuccess from './components/storefront/OrderSuccess';
import OrderLookup from './components/storefront/OrderLookup';
import Login from './components/storefront/Login';
import AdminDashboard from './components/admin/AdminDashboard';
import ProductsList from './components/admin/ProductsList';
import ProductForm from './components/admin/ProductForm';
import CategoriesList from './components/admin/CategoriesList';
import OrdersList from './components/admin/OrdersList';
import OrderDetail from './components/admin/OrderDetail';

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <CartProvider>
          <ConfirmDialogProvider>
            <Toaster position="bottom-right" richColors />
            <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<Login />} />
            <Route path="/products" element={<ProductListing />} />
            <Route path="/products/:id" element={<ProductDetail />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/checkout" element={<Checkout />} />
            <Route path="/order-success/:orderNumber" element={<OrderSuccess />} />
            <Route path="/order-lookup" element={<OrderLookup />} />

            <Route path="/admin/login" element={<Navigate to="/login" replace />} />
            <Route path="/admin" element={<ProtectedRoute requireAdmin><AdminDashboard /></ProtectedRoute>} />
            <Route path="/admin/products" element={<ProtectedRoute requireAdmin><ProductsList /></ProtectedRoute>} />
            <Route path="/admin/products/new" element={<ProtectedRoute requireAdmin><ProductForm /></ProtectedRoute>} />
            <Route path="/admin/products/:id/edit" element={<ProtectedRoute requireAdmin><ProductForm /></ProtectedRoute>} />
            <Route path="/admin/categories" element={<ProtectedRoute requireAdmin><CategoriesList /></ProtectedRoute>} />
            <Route path="/admin/orders" element={<ProtectedRoute requireAdmin><OrdersList /></ProtectedRoute>} />
            <Route path="/admin/orders/:id" element={<ProtectedRoute requireAdmin><OrderDetail /></ProtectedRoute>} />

            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
          </ConfirmDialogProvider>
        </CartProvider>
      </AuthProvider>
    </BrowserRouter>
  );
}
