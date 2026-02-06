import { useState, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import Breadcrumbs from './Breadcrumbs';
import ProductCard from './ProductCard';
import { ChevronLeft, ChevronRight, X } from 'lucide-react';
import { productsApi, categoriesApi } from '../../api';
import { handleApiError } from '../../utils/errorHandler';
import { ProductSortBy } from '../../types/api';
import type { Product } from '../../types';
import type { ProductSearchItemResponse } from '../../types/api';

const mapProductSearchItemToProduct = (item: ProductSearchItemResponse): Product => ({
  ...item,
  slug: item.name.toLowerCase().replace(/\s+/g, '-'),
  image: item.primaryImageUrl,
  rating: 0,
  reviewCount: 0,
  isNew: false,
});
import type { CategoryDetailResponse, CategoryResponse } from '../../types/api';

export default function ProductListing() {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<CategoryResponse[]>([]);
  const [category, setCategory] = useState<CategoryDetailResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [totalPages, setTotalPages] = useState(0);
  const [currentPage, setCurrentPage] = useState(0);
  
  const searchTerm = searchParams.get('search') || '';
  const categoryId = searchParams.get('category') || '';
  const minPrice = searchParams.get('minPrice') || '';
  const maxPrice = searchParams.get('maxPrice') || '';
  const inStockOnly = searchParams.get('inStock') === 'true';
  const sortBy = searchParams.get('sort') || 'default';
  const page = parseInt(searchParams.get('page') || '0');

  useEffect(() => {
    loadCategories();
  }, []);

  useEffect(() => {
    loadProducts();
  }, [searchParams]);

  const loadCategories = async () => {
    try {
      const response = await categoriesApi.getCategories(0, 100);
      setCategories(response.items);
    } catch (error) {
      handleApiError(error, 'Kategoriler yüklenirken hata oluştu');
    }
  };

  const loadProducts = async () => {
    setLoading(true);
    try {
      const getSortByValue = (sort: string): ProductSortBy | undefined => {
        switch (sort) {
          case 'price-asc': return ProductSortBy.PriceLowToHigh;
          case 'price-desc': return ProductSortBy.PriceHighToLow;
          case 'newest': return ProductSortBy.Newest;
          default: return ProductSortBy.Default;
        }
      };

      const productsResponse = await productsApi.searchProducts({
        SearchTerm: searchTerm || undefined,
        CategoryId: categoryId || undefined,
        MinPrice: minPrice ? parseFloat(minPrice) : undefined,
        MaxPrice: maxPrice ? parseFloat(maxPrice) : undefined,
        InStock: inStockOnly || undefined,
        SortBy: getSortByValue(sortBy),
        PageRequest: { PageIndex: page, PageSize: 12 },
      });

      setProducts(productsResponse.items.map(mapProductSearchItemToProduct));
      setTotalPages(productsResponse.pages);
      setCurrentPage(page);

      if (categoryId) {
        const cat = await categoriesApi.getCategoryById(categoryId);
        setCategory(cat);
      } else {
        setCategory(null);
      }
    } catch (error) {
      handleApiError(error, 'Ürünler yüklenirken hata oluştu');
    } finally {
      setLoading(false);
    }
  };

  const updateFilters = (updates: Record<string, string | null>) => {
    const newParams = new URLSearchParams(searchParams);
    
    Object.entries(updates).forEach(([key, value]) => {
      if (value === null || value === '') {
        newParams.delete(key);
      } else {
        newParams.set(key, value);
      }
    });
    
    if (!updates.page) {
      newParams.delete('page');
    }
    
    setSearchParams(newParams);
  };

  const handleProductClick = (productId: string) => {
    navigate(`/products/${productId}`);
  };

  const handleClearFilters = () => {
    setSearchParams(new URLSearchParams());
  };

  const handleRemoveFilter = (filterKey: string) => {
    updateFilters({ [filterKey]: null });
  };

  const breadcrumbItems = [
    { label: 'Home', onClick: () => navigate('/products') },
    ...(category ? [{ label: category.name }] : [{ label: 'All Products' }]),
  ];

  const activeFiltersCount = [
    searchTerm, 
    categoryId, 
    minPrice, 
    maxPrice, 
    inStockOnly ? 'stock' : '', 
    sortBy !== 'default' ? sortBy : ''
  ].filter(Boolean).length;

  return (
    <div>
      <Header />
      
      <main className="bg-white min-h-screen">
        <div className="max-w-6xl mx-auto px-4 py-8">
          <div className="mb-6">
            <Breadcrumbs items={breadcrumbItems} />
          </div>

          <div className="flex items-start justify-between mb-6">
            <div>
              <h1 className="text-3xl font-bold text-slate-900 mb-2">
                {category ? category.name : 'All Products'}
              </h1>
              <p className="text-slate-600">
                {category?.description || 'Browse our complete collection'}
              </p>
            </div>
            {activeFiltersCount > 0 && (
              <button
                onClick={handleClearFilters}
                className="text-sm text-indigo-600 hover:text-indigo-700 font-semibold flex items-center gap-1"
              >
                <X className="size-4" />
                Clear all filters
              </button>
            )}
          </div>

          {activeFiltersCount > 0 && (
            <div className="flex flex-wrap gap-2 mb-6">
              {searchTerm && (
                <span className="inline-flex items-center gap-1 px-3 py-1 bg-indigo-100 text-indigo-700 text-sm rounded-full">
                  Search: {searchTerm}
                  <button onClick={() => handleRemoveFilter('search')} className="hover:bg-indigo-200 rounded-full p-0.5">
                    <X className="size-3" />
                  </button>
                </span>
              )}
              {category && (
                <span className="inline-flex items-center gap-1 px-3 py-1 bg-indigo-100 text-indigo-700 text-sm rounded-full">
                  Category: {category.name}
                  <button onClick={() => handleRemoveFilter('category')} className="hover:bg-indigo-200 rounded-full p-0.5">
                    <X className="size-3" />
                  </button>
                </span>
              )}
              {minPrice && (
                <span className="inline-flex items-center gap-1 px-3 py-1 bg-indigo-100 text-indigo-700 text-sm rounded-full">
                  Min: ${minPrice}
                  <button onClick={() => handleRemoveFilter('minPrice')} className="hover:bg-indigo-200 rounded-full p-0.5">
                    <X className="size-3" />
                  </button>
                </span>
              )}
              {maxPrice && (
                <span className="inline-flex items-center gap-1 px-3 py-1 bg-indigo-100 text-indigo-700 text-sm rounded-full">
                  Max: ${maxPrice}
                  <button onClick={() => handleRemoveFilter('maxPrice')} className="hover:bg-indigo-200 rounded-full p-0.5">
                    <X className="size-3" />
                  </button>
                </span>
              )}
              {inStockOnly && (
                <span className="inline-flex items-center gap-1 px-3 py-1 bg-indigo-100 text-indigo-700 text-sm rounded-full">
                  In Stock Only
                  <button onClick={() => handleRemoveFilter('inStock')} className="hover:bg-indigo-200 rounded-full p-0.5">
                    <X className="size-3" />
                  </button>
                </span>
              )}
            </div>
          )}

          <div className="flex flex-col md:flex-row gap-4 mb-8 pb-6 border-b border-slate-200">
            <div className="flex-1">
              <input
                type="text"
                placeholder="Search products..."
                value={searchTerm}
                onChange={(e) => updateFilters({ search: e.target.value })}
                className="w-full px-4 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
              />
            </div>

            <div className="flex flex-wrap gap-3">
              <select
                value={categoryId}
                onChange={(e) => updateFilters({ category: e.target.value })}
                className="px-3 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
              >
                <option value="">All Categories</option>
                {categories.map((cat) => (
                  <option key={cat.id} value={cat.id}>
                    {cat.name}
                  </option>
                ))}
              </select>
              <input
                type="number"
                placeholder="Min Price"
                value={minPrice}
                onChange={(e) => updateFilters({ minPrice: e.target.value })}
                className="w-32 px-3 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
              />
              <input
                type="number"
                placeholder="Max Price"
                value={maxPrice}
                onChange={(e) => updateFilters({ maxPrice: e.target.value })}
                className="w-32 px-3 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
              />
              <label className="flex items-center gap-2 px-3 py-2 border border-slate-200 rounded-lg text-sm cursor-pointer hover:bg-slate-50">
                <input
                  type="checkbox"
                  checked={inStockOnly}
                  onChange={(e) => updateFilters({ inStock: e.target.checked ? 'true' : null })}
                  className="rounded border-slate-300 text-indigo-600 focus:ring-indigo-500"
                />
                <span className="text-slate-700">In Stock Only</span>
              </label>
              <select
                value={sortBy}
                onChange={(e) => updateFilters({ sort: e.target.value })}
                className="px-3 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500"
              >
                <option value="default">Default</option>
                <option value="price-asc">Price: Low to High</option>
                <option value="price-desc">Price: High to Low</option>
                <option value="newest">Newest</option>
              </select>
            </div>
          </div>

          {loading ? (
            <div className="text-center py-16 text-slate-600">Loading products...</div>
          ) : products.length === 0 ? (
            <div className="text-center py-16">
              <p className="text-slate-600 mb-4">No products found matching your criteria.</p>
              {activeFiltersCount > 0 && (
                <button
                  onClick={handleClearFilters}
                  className="text-indigo-600 hover:text-indigo-700 font-semibold"
                >
                  Clear all filters
                </button>
              )}
            </div>
          ) : (
            <>
              <div className="grid grid-cols-2 md:grid-cols-4 gap-6 mb-8">
                {products.map((product) => (
                  <ProductCard 
                    key={product.id} 
                    product={product}
                    onClick={() => handleProductClick(product.id)}
                  />
                ))}
              </div>

              {totalPages > 1 && (
                <div className="flex justify-center gap-2">
                  <button 
                    onClick={() => updateFilters({ page: String(Math.max(0, currentPage - 1)) })}
                    disabled={currentPage === 0}
                    className="p-2 rounded-lg border border-slate-200 hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    <ChevronLeft className="size-5" />
                  </button>
                  
                  {Array.from({ length: Math.min(5, totalPages) }, (_, i) => {
                    let pageNum: number;
                    if (totalPages <= 5) {
                      pageNum = i;
                    } else if (currentPage < 3) {
                      pageNum = i;
                    } else if (currentPage > totalPages - 3) {
                      pageNum = totalPages - 5 + i;
                    } else {
                      pageNum = currentPage - 2 + i;
                    }
                    
                    return (
                      <button
                        key={pageNum}
                        onClick={() => updateFilters({ page: String(pageNum) })}
                        className={`px-4 py-2 rounded-lg font-semibold ${
                          currentPage === pageNum
                            ? 'bg-indigo-600 text-white'
                            : 'border border-slate-200 hover:bg-slate-50'
                        }`}
                      >
                        {pageNum + 1}
                      </button>
                    );
                  })}
                  
                  <button 
                    onClick={() => updateFilters({ page: String(Math.min(totalPages - 1, currentPage + 1)) })}
                    disabled={currentPage >= totalPages - 1}
                    className="p-2 rounded-lg border border-slate-200 hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    <ChevronRight className="size-5" />
                  </button>
                </div>
              )}
            </>
          )}
        </div>
      </main>

      <Footer />
    </div>
  );
}
