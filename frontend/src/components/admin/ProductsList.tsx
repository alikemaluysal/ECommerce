import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import { Search, Plus, Edit, Trash2 } from 'lucide-react';
import { productsApi } from '../../api';
import { handleApiError, showSuccess } from '../../utils/errorHandler';
import type { ProductListItemResponse } from '../../types/api';

export default function ProductsList() {
  const navigate = useNavigate();
  const [products, setProducts] = useState<ProductListItemResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    loadProducts();
  }, [currentPage, searchTerm]);

  const loadProducts = async () => {
    setLoading(true);
    try {
      const response = await productsApi.searchProducts({
        SearchTerm: searchTerm || undefined,
        PageRequest: {
          PageIndex: currentPage,
          PageSize: 10,
        },
      });
      setProducts(response.items);
      setTotalPages(response.pages);
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (confirm('Are you sure you want to delete this product?')) {
      try {
        await productsApi.deleteProduct(id);
        showSuccess('Product deleted successfully');
        await loadProducts();
      } catch (error) {
        handleApiError(error);
      }
    }
  };

  return (
    <div className="flex min-h-screen bg-slate-50">
      <AdminSidebar />
      
      <div className="flex-1 flex flex-col min-w-0">
        <AdminTopbar title="Products" />
        
        <main className="flex-1 p-6">
          <div className="bg-white border border-slate-200 rounded-xl p-4 shadow-sm mb-6">
            <div className="flex flex-col sm:flex-row gap-4 items-start sm:items-center justify-between">
              <div className="flex-1 flex gap-3 w-full sm:w-auto">
                <div className="relative flex-1 sm:flex-initial sm:w-80">
                  <input
                    type="search"
                    placeholder="Search products..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="w-full pl-10 pr-4 py-2 border border-slate-200 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                  />
                  <Search className="absolute left-3 top-1/2 -translate-y-1/2 size-4 text-slate-400" />
                </div>
              </div>

              <button 
                onClick={() => navigate('/admin/products/new')}
                className="bg-indigo-600 text-white px-4 py-2 rounded-lg font-semibold text-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md flex items-center gap-2 w-full sm:w-auto justify-center"
              >
                <Plus className="size-4" />
                New Product
              </button>
            </div>
          </div>

          {loading ? (
            <div className="text-center py-16 text-slate-600">Loading...</div>
          ) : (
          <div className="bg-white border border-slate-200 rounded-xl shadow-sm overflow-hidden">
            <div className="overflow-x-auto">
              <table className="w-full">
                <thead>
                  <tr className="border-b border-slate-200">
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Product
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Category
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Price
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Stock
                    </th>
                    <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                      Actions
                    </th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-200">
                  {products.map((product) => (
                    <tr key={product.id} className="hover:bg-slate-50 transition-colors">
                      <td className="px-6 py-4">
                        <div className="flex items-center gap-3">
                          <div className="bg-slate-100 size-12 rounded-lg flex-shrink-0">
                            {product.primaryImageUrl ? (
                              <img 
                                src={product.primaryImageUrl} 
                                alt={product.name}
                                className="w-full h-full object-cover rounded-lg"
                              />
                            ) : (
                              <div className="w-full h-full bg-gradient-to-br from-slate-200 to-slate-100 rounded-lg"></div>
                            )}
                          </div>
                          <div className="text-sm font-semibold text-slate-900 min-w-0">
                            {product.name}
                          </div>
                        </div>
                      </td>
                      <td className="px-6 py-4 text-sm text-slate-600">
                        {product.categoryName}
                      </td>
                      <td className="px-6 py-4 text-sm font-semibold text-slate-900">
                        ${product.price.toFixed(2)}
                      </td>
                      <td className="px-6 py-4">
                        <span className={`inline-flex items-center px-2.5 py-1 rounded-lg text-xs font-semibold ${
                          product.stock === 0 
                            ? 'bg-red-100 text-red-700' 
                            : product.stock < 50 
                            ? 'bg-amber-100 text-amber-700' 
                            : 'bg-emerald-100 text-emerald-700'
                        }`}>
                          {product.stock === 0 ? 'Out of stock' : `${product.stock} units`}
                        </span>
                      </td>
                      <td className="px-6 py-4">
                        <div className="flex items-center gap-2">
                          <button 
                            onClick={() => navigate(`/admin/products/${product.id}/edit`)}
                            className="p-2 text-slate-600 hover:text-indigo-600 hover:bg-indigo-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500"
                            aria-label="Edit product"
                          >
                            <Edit className="size-4" />
                          </button>
                          <button 
                            onClick={() => handleDelete(product.id)}
                            className="p-2 text-slate-600 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-red-500"
                            aria-label="Delete product"
                          >
                            <Trash2 className="size-4" />
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            
            {totalPages > 1 && (
              <div className="flex items-center justify-between px-6 py-4 border-t border-slate-200">
                <button
                  onClick={() => setCurrentPage(p => Math.max(0, p - 1))}
                  disabled={currentPage === 0}
                  className="px-4 py-2 text-sm font-medium text-slate-700 bg-white border border-slate-300 rounded-lg hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  Previous
                </button>
                <span className="text-sm text-slate-700">
                  Page {currentPage + 1} of {totalPages}
                </span>
                <button
                  onClick={() => setCurrentPage(p => Math.min(totalPages - 1, p + 1))}
                  disabled={currentPage >= totalPages - 1}
                  className="px-4 py-2 text-sm font-medium text-slate-700 bg-white border border-slate-300 rounded-lg hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  Next
                </button>
              </div>
            )}
          </div>
          )}
        </main>
      </div>
    </div>
  );
}
