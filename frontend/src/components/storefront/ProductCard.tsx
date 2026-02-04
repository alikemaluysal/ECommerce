import { ShoppingCart } from 'lucide-react';
import type { Product } from '../../types';
import { useCart } from '../../context/CartContext';

interface ProductCardProps {
  product: Product;
  onClick: () => void;
}

export default function ProductCard({ product, onClick }: ProductCardProps) {
  const { addToCart } = useCart();

  const handleAddToCart = async (e: React.MouseEvent) => {
    e.stopPropagation();
    if (product.stock > 0) {
      await addToCart(product, 1);
    }
  };

  return (
    <div className="group cursor-pointer" onClick={onClick}>
      <div className="relative bg-slate-100 rounded-xl aspect-[3/4] overflow-hidden mb-3 shadow-sm hover:shadow-md transition-shadow">
        <img 
          src={product.primaryImageUrl || '/placeholder.png'} 
          alt={product.name}
          className="w-full h-full object-cover"
          onError={(e) => {
            (e.target as HTMLImageElement).src = 'https://placehold.co/400x500/e2e8f0/64748b?text=Product';
          }}
        />
        
        <button 
          onClick={handleAddToCart}
          disabled={product.stock === 0}
          className={`absolute top-3 right-3 size-9 rounded-full flex items-center justify-center opacity-0 group-hover:opacity-100 transition-all shadow-md hover:shadow-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 ${
            product.stock === 0 
              ? 'bg-slate-300 cursor-not-allowed' 
              : 'bg-indigo-600 hover:bg-indigo-700 hover:scale-110'
          }`}
          aria-label="Add to cart"
        >
          <ShoppingCart className={`size-4 ${product.stock === 0 ? 'text-slate-500' : 'text-white'}`} />
        </button>

        <div 
          className="absolute inset-0 flex items-center justify-center bg-gradient-to-t from-black/60 via-black/30 to-transparent opacity-0 group-hover:opacity-100 transition-opacity pointer-events-none"
        >
          <span className="bg-white text-slate-900 px-6 py-2.5 rounded-full font-semibold text-sm shadow-lg transform transition-transform group-hover:scale-105">
            Quick View
          </span>
        </div>
      </div>

      <div>
        <h3 className="font-semibold text-slate-900 mb-1 line-clamp-2">{product.name}</h3>
        <div className="flex items-center gap-2">
          <span className="text-slate-900 font-semibold">${product.price.toFixed(2)}</span>
        </div>
        {product.stock < 10 && product.stock > 0 && (
          <p className="text-xs text-amber-600 mt-1">Only {product.stock} left!</p>
        )}
        {product.stock === 0 && (
          <p className="text-xs text-red-600 mt-1">Out of stock</p>
        )}
      </div>
    </div>
  );
}
