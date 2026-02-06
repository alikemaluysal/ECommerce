import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import Breadcrumbs from './Breadcrumbs';
import QuantitySelector from './QuantitySelector';
import { ShoppingCart, Heart, Truck, Shield, Package } from 'lucide-react';
import { productsApi, categoriesApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import { useCart } from '../../context/CartContext';
import type { ProductDetailResponse, CategoryDetailResponse } from '../../types/api';
import type { Product } from '../../types';

export default function ProductDetail() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [product, setProduct] = useState<ProductDetailResponse | null>(null);
  const [category, setCategory] = useState<CategoryDetailResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [quantity, setQuantity] = useState(1);
  const [selectedImage, setSelectedImage] = useState(0);
  const { addToCart } = useCart();

  useEffect(() => {
    if (id) {
      loadProduct();
    }
  }, [id]);

  const loadProduct = async () => {
    if (!id) return;
    
    setLoading(true);
    try {
      const prod = await productsApi.getProductById(id);
      setProduct(prod);
      const cat = await categoriesApi.getCategoryById(prod.categoryId);
      setCategory(cat);
    } catch (error) {
      handleApiError(error, 'Ürün yüklenirken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const handleAddToCart = async () => {
    if (product) {
      const productForCart: Product = {
        id: product.id,
        name: product.name,
        slug: product.name.toLowerCase().replace(/\s+/g, '-'),
        description: product.description,
        price: product.price,
        stock: product.stock,
        categoryId: product.categoryId,
        categoryName: category?.name || '',
        primaryImageUrl: product.primaryImageUrl,
        image: product.primaryImageUrl,
        images: product.images,
        specifications: product.specifications,
        rating: 0,
        reviewCount: 0,
        isNew: false,
      };
      
      await addToCart(productForCart, quantity);
    }
  };

  if (loading || !product) {
    return (
      <div>
        <Header />
        <div className="max-w-6xl mx-auto px-4 py-16 text-center">
          <div className="text-slate-600">Loading...</div>
        </div>
        <Footer />
      </div>
    );
  }

  const breadcrumbItems = [
    { label: 'Home', onClick: () => navigate('/products') },
    ...(category ? [{ label: category.name, onClick: () => navigate(`/products?category=${category.id}`) }] : []),
    { label: product.name },
  ];

  return (
    <div>
      <Header />
      
      <main className="bg-white min-h-screen">
        <div className="max-w-6xl mx-auto px-4 py-8">
          <div className="mb-6">
            <Breadcrumbs items={breadcrumbItems} />
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-12 mb-16">
            <div>
              <div className="bg-slate-100 rounded-xl aspect-square mb-4 overflow-hidden">
                <img
                  src={product.images?.[selectedImage]?.imageUrl || product.primaryImageUrl || 'https://placehold.co/600x600/e2e8f0/64748b?text=Product'}
                  alt={product.name}
                  className="w-full h-full object-cover"
                  onError={(e) => {
                    (e.target as HTMLImageElement).src = 'https://placehold.co/600x600/e2e8f0/64748b?text=Product';
                  }}
                />
              </div>
              
              {product.images && product.images.length > 1 && (
                <div className="grid grid-cols-4 gap-4">
                  {product.images.map((img, idx) => (
                    <button
                      key={idx}
                      onClick={() => setSelectedImage(idx)}
                      className={`bg-slate-100 rounded-lg aspect-square overflow-hidden border-2 transition-all ${
                        selectedImage === idx ? 'border-indigo-600' : 'border-transparent'
                      }`}
                    >
                      <img src={img.imageUrl} alt={`${product.name} ${idx + 1}`} className="w-full h-full object-cover" />
                    </button>
                  ))}
                </div>
              )}
            </div>

            <div>
              <h1 className="text-3xl font-bold text-slate-900 mb-4">{product.name}</h1>

              <div className="flex items-baseline gap-3 mb-6">
                <span className="text-3xl font-bold text-slate-900">${product.price.toFixed(2)}</span>
              </div>

              <p className="text-slate-600 mb-6">{product.description}</p>

              {product.stock > 0 ? (
                <>
                  <div className="mb-6">
                    <label className="text-sm font-semibold text-slate-900 mb-2 block">Quantity</label>
                    <QuantitySelector 
                      quantity={quantity}
                      onIncrease={() => setQuantity(q => q + 1)}
                      onDecrease={() => setQuantity(q => Math.max(1, q - 1))}
                    />
                    {product.stock < 10 && (
                      <p className="text-xs text-amber-600 mt-2">Only {product.stock} left in stock!</p>
                    )}
                  </div>

                  <div className="flex gap-3 mb-8">
                    <button
                      onClick={handleAddToCart}
                      className="flex-1 bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors flex items-center justify-center gap-2"
                    >
                      <ShoppingCart className="size-5" />
                      Add to Cart
                    </button>
                    <button className="p-3 border border-slate-200 rounded-lg hover:bg-slate-50 transition-colors">
                      <Heart className="size-6 text-slate-600" />
                    </button>
                  </div>
                </>
              ) : (
                <div className="mb-8 p-4 bg-red-50 border border-red-200 rounded-lg">
                  <p className="text-red-700 font-semibold">Out of Stock</p>
                </div>
              )}

              <div className="border-t border-slate-200 pt-6 space-y-3">
                <div className="flex items-center gap-3">
                  <Truck className="size-5 text-indigo-600" />
                  <span className="text-sm text-slate-700">Free shipping on orders over $100</span>
                </div>
                <div className="flex items-center gap-3">
                  <Package className="size-5 text-indigo-600" />
                  <span className="text-sm text-slate-700">Ships within 2-3 business days</span>
                </div>
                <div className="flex items-center gap-3">
                  <Shield className="size-5 text-indigo-600" />
                  <span className="text-sm text-slate-700">30-day return policy</span>
                </div>
              </div>

              {product.specifications && product.specifications.length > 0 && (
                <div className="border-t border-slate-200 mt-6 pt-6">
                  <h3 className="font-semibold text-slate-900 mb-3">Specifications</h3>
                  <dl className="space-y-2">
                    {product.specifications.map((spec) => (
                      <div key={spec.id} className="flex">
                        <dt className="text-sm text-slate-600 w-1/3">{spec.key}</dt>
                        <dd className="text-sm text-slate-900 w-2/3">{spec.value}</dd>
                      </div>
                    ))}
                  </dl>
                </div>
              )}
            </div>
          </div>
        </div>
      </main>

      <Footer />
    </div>
  );
}
