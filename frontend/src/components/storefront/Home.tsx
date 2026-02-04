import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import ProductCard from './ProductCard';
import { Package, RotateCcw, Headphones } from 'lucide-react';
import { productsApi, categoriesApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import type { Product } from '../../types';
import type { TopCategoryResponse } from '../../types/api';

export default function Home() {
  const navigate = useNavigate();
  const [categories, setCategories] = useState<TopCategoryResponse[]>([]);
  const [featuredProducts, setFeaturedProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadHomeData();
  }, []);

  const loadHomeData = async () => {
    setLoading(true);
    try {
      const [catsResponse, productsResponse] = await Promise.all([
        categoriesApi.getTopCategories(),
        productsApi.getProducts({ page: 0, pageSize: 8 }),
      ]);
      setCategories(catsResponse);
      setFeaturedProducts(productsResponse.items);
    } catch (error) {
      handleApiError(error, 'Anasayfa verileri yüklenirken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const handleCategoryClick = (categoryId: string | null) => {
    if (categoryId) {
      navigate(`/products?category=${categoryId}`);
    } else {
      navigate('/products');
    }
  };

  const handleProductClick = (productId: string) => {
    navigate(`/products/${productId}`);
  };

  return (
    <div>
      <Header />
      
      <main>
        {/* Hero Section */}
        <section className="bg-gradient-to-br from-slate-50 to-slate-100 border-b border-slate-200/60">
          <div className="max-w-6xl mx-auto px-4 py-20 md:py-32">
            <div className="max-w-2xl">
              <h1 className="text-slate-900 mb-4 text-4xl md:text-5xl font-bold">
                Discover Premium Products for Modern Living
              </h1>
              <p className="text-slate-600 mb-8 text-lg">
                Curated collection of high-quality essentials designed to elevate your everyday experience.
              </p>
              <button 
                onClick={() => handleCategoryClick(null)}
                className="bg-indigo-600 text-white px-6 py-3 rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md"
              >
                Shop Now
              </button>
            </div>
          </div>
        </section>

        {/* Category Pills */}
        <section className="border-b border-slate-200/60 bg-white">
          <div className="max-w-6xl mx-auto px-4 py-6">
            {loading ? (
              <div className="text-center text-slate-600">Loading categories...</div>
            ) : (
              <div className="flex gap-2 overflow-x-auto pb-2 scrollbar-hide">
                <button
                  onClick={() => handleCategoryClick(null)}
                  className="px-4 py-2 rounded-lg border border-slate-200 text-sm font-semibold text-slate-700 hover:border-indigo-600 hover:text-indigo-600 hover:bg-indigo-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-all whitespace-nowrap"
                >
                  All Products
                </button>
                {categories.map((category) => (
                  <button
                    key={category.id}
                    onClick={() => navigate(`/products?category=${category.id}`)}
                    className="px-4 py-2 rounded-lg border border-slate-200 text-sm font-semibold text-slate-700 hover:border-indigo-600 hover:text-indigo-600 hover:bg-indigo-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-all whitespace-nowrap"
                  >
                    {category.name} ({category.productCount})
                  </button>
                ))}
              </div>
            )}
          </div>
        </section>

        {/* Featured Products */}
        <section className="bg-white">
          <div className="max-w-6xl mx-auto px-4 py-16">
            <div className="mb-8">
              <h2 className="text-slate-900 mb-2 text-3xl font-bold">Featured Products</h2>
              <p className="text-slate-600">Hand-picked items just for you</p>
            </div>
            
            {loading ? (
              <div className="text-center text-slate-600 py-12">Loading products...</div>
            ) : (
              <div className="grid grid-cols-2 md:grid-cols-4 gap-6">
                {featuredProducts.map((product) => (
                  <ProductCard 
                    key={product.id} 
                    product={product}
                    onClick={() => handleProductClick(product.id)}
                  />
                ))}
              </div>
            )}
          </div>
        </section>

        {/* Features */}
        <section className="bg-slate-50 border-y border-slate-200/60">
          <div className="max-w-6xl mx-auto px-4 py-16">
            <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
              <div className="bg-white border border-slate-200/60 rounded-xl p-6 shadow-sm hover:shadow-md transition-shadow">
                <div className="bg-indigo-100 size-12 rounded-lg flex items-center justify-center mb-4">
                  <Package className="size-6 text-indigo-600" />
                </div>
                <h3 className="font-semibold text-slate-900 mb-2">Free Shipping</h3>
                <p className="text-sm text-slate-600">On orders over $100. Fast and reliable delivery to your door.</p>
              </div>

              <div className="bg-white border border-slate-200/60 rounded-xl p-6 shadow-sm hover:shadow-md transition-shadow">
                <div className="bg-emerald-100 size-12 rounded-lg flex items-center justify-center mb-4">
                  <RotateCcw className="size-6 text-emerald-600" />
                </div>
                <h3 className="font-semibold text-slate-900 mb-2">Easy Returns</h3>
                <p className="text-sm text-slate-600">30-day return policy. Shop with confidence and peace of mind.</p>
              </div>

              <div className="bg-white border border-slate-200/60 rounded-xl p-6 shadow-sm hover:shadow-md transition-shadow">
                <div className="bg-amber-100 size-12 rounded-lg flex items-center justify-center mb-4">
                  <Headphones className="size-6 text-amber-600" />
                </div>
                <h3 className="font-semibold text-slate-900 mb-2">24/7 Support</h3>
                <p className="text-sm text-slate-600">Dedicated customer service team ready to help you anytime.</p>
              </div>
            </div>
          </div>
        </section>
      </main>

      <Footer />
    </div>
  );
}
