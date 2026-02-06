import { createContext, useContext, useState, useEffect, useCallback } from 'react';
import type { ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import type { CartItem, Product } from '../types';
import { cartApi, productsApi } from '../api';
import { handleApiError, showSuccess, showError } from '../utils/errorHandler';
import { useAuth } from './AuthContext';

interface CartContextType {
  items: CartItem[];
  isLoading: boolean;
  addToCart: (product: Product, quantity?: number) => Promise<void>;
  removeFromCart: (itemId: string) => Promise<void>;
  updateQuantity: (itemId: string, quantity: number) => Promise<void>;
  clearCart: () => Promise<void>;
  refreshCart: () => Promise<void>;
  getItemCount: () => number;
  getSubtotal: () => number;
  getShipping: () => number;
  getTax: () => number;
  getTotal: () => number;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

export function CartProvider({ children }: { children: ReactNode }) {
  const { isAuthenticated, user } = useAuth();
  const navigate = useNavigate();
  const [items, setItems] = useState<CartItem[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const loadCartFromBackend = useCallback(async () => {
    if (!isAuthenticated()) return;

    try {
      setIsLoading(true);
      const response = await cartApi.getCart();
      
      const itemsWithProducts = await Promise.all(
        response.items.map(async (item) => {
          try {
            const product = await productsApi.getProductById(item.productId);
            const cartItem: CartItem = {
              id: item.id,
              productId: item.productId,
              quantity: item.quantity,
              product: {
                id: product.id,
                name: product.name,
                slug: product.name.toLowerCase().replace(/\s+/g, '-'),
                description: product.description,
                price: product.price,
                stock: product.stock,
                categoryId: product.categoryId,
                categoryName: '',
                primaryImageUrl: product.primaryImageUrl,
                image: product.primaryImageUrl,
                images: product.images,
                specifications: product.specifications,
                rating: 0,
                reviewCount: 0,
                isNew: false,
              },
            };
            return cartItem;
          } catch (error) {
            console.error(`Failed to load product ${item.productId}`, error);
            return null;
          }
        })
      );

      setItems(itemsWithProducts.filter((item): item is CartItem => item !== null));
    } catch (error) {
      handleApiError(error);
    } finally {
      setIsLoading(false);
    }
  }, [isAuthenticated]);

  useEffect(() => {
    if (isAuthenticated() && user) {
      loadCartFromBackend();
    } else {
      setItems([]);
    }
  }, [isAuthenticated, user, loadCartFromBackend]);

  const addToCart = async (product: Product, quantity: number = 1) => {
    if (!isAuthenticated()) {
      showError('Sepete ürün eklemek için giriş yapmalısınız');
      navigate('/login', { state: { from: window.location.pathname } });
      return;
    }

    try {
      setIsLoading(true);
      const response = await cartApi.addItem({
        productId: product.id,
        quantity,
      });
      
      setItems((prevItems) => {
        const existingItem = prevItems.find((item) => item.id === response.id);
        
        if (existingItem) {
          return prevItems.map((item) =>
            item.id === response.id
              ? { ...item, quantity: response.quantity, product }
              : item
          );
        } else {
          return [...prevItems, { ...response, product }];
        }
      });
      
      showSuccess('Ürün sepete eklendi');
    } catch (error) {
      handleApiError(error);
    } finally {
      setIsLoading(false);
    }
  };

  const removeFromCart = async (itemId: string) => {
    if (!isAuthenticated()) {
      showError('Bu işlem için giriş yapmalısınız');
      return;
    }

    try {
      setIsLoading(true);
      await cartApi.removeItem(itemId);
      setItems((prevItems) => prevItems.filter((item) => item.id !== itemId));
      showSuccess('Ürün sepetten çıkarıldı');
    } catch (error) {
      handleApiError(error);
    } finally {
      setIsLoading(false);
    }
  };

  const updateQuantity = async (itemId: string, quantity: number) => {
    if (!isAuthenticated()) {
      showError('Bu işlem için giriş yapmalısınız');
      return;
    }

    if (quantity <= 0) {
      await removeFromCart(itemId);
      return;
    }

    try {
      setIsLoading(true);
      
      const response = await cartApi.updateItem(itemId, { quantity });
      
      setItems((prevItems) =>
        prevItems.map((i) =>
          i.id === itemId 
            ? { ...i, quantity: response.quantity } 
            : i
        )
      );
    } catch (error) {
      handleApiError(error);
      await loadCartFromBackend();
    } finally {
      setIsLoading(false);
    }
  };

  const clearCart = async () => {
    if (!isAuthenticated()) {
      showError('Bu işlem için giriş yapmalısınız');
      return;
    }

    try {
      setIsLoading(true);
      await cartApi.clearCart();
      setItems([]);
      showSuccess('Sepet temizlendi');
    } catch (error) {
      handleApiError(error);
    } finally {
      setIsLoading(false);
    }
  };

  const refreshCart = async () => {
    await loadCartFromBackend();
  };

  const getItemCount = () => {
    return items.reduce((count, item) => count + item.quantity, 0);
  };

  const getSubtotal = () => {
    return items.reduce((total, item) => {
      if (!item.product) return total;
      return total + item.product.price * item.quantity;
    }, 0);
  };

  const getShipping = () => {
    const subtotal = getSubtotal();
    return subtotal > 100 ? 0 : 15;
  };

  const getTax = () => {
    return getSubtotal() * 0.1;
  };

  const getTotal = () => {
    return getSubtotal() + getShipping() + getTax();
  };

  return (
    <CartContext.Provider
      value={{
        items,
        isLoading,
        addToCart,
        removeFromCart,
        updateQuantity,
        clearCart,
        refreshCart,
        getItemCount,
        getSubtotal,
        getShipping,
        getTax,
        getTotal,
      }}
    >
      {children}
    </CartContext.Provider>
  );
}

export function useCart() {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within CartProvider');
  }
  return context;
}
