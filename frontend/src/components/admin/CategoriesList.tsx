import { useState, useEffect } from 'react';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { categoriesApi } from '../../api';
import { handleApiError, showSuccess } from '../../utils/errorHandler';
import type { CategoryResponse } from '../../types/api';

export default function CategoriesList() {
  const [categories, setCategories] = useState<CategoryResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    loadCategories();
  }, [currentPage]);

  const loadCategories = async () => {
    setLoading(true);
    try {
      const response = await categoriesApi.getCategories(currentPage, 10);
      setCategories(response.items);
      setTotalPages(response.pages);
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (confirm('Are you sure you want to delete this category?')) {
      try {
        await categoriesApi.deleteCategory(id);
        showSuccess('Category deleted successfully');
        await loadCategories();
      } catch (error) {
        handleApiError(error);
      }
    }
  };

  const handlePrevPage = () => {
    if (currentPage > 0) {
      setCurrentPage(currentPage - 1);
    }
  };

  const handleNextPage = () => {
    if (currentPage < totalPages - 1) {
      setCurrentPage(currentPage + 1);
    }
  };

  return (
    <div className="flex min-h-screen bg-slate-50">
      <AdminSidebar />
      
      <div className="flex-1 flex flex-col min-w-0">
        <AdminTopbar title="Categories" />
        
        <main className="flex-1 p-6">
          <div className="flex justify-between items-center mb-6">
            <div>
              <h2 className="font-semibold text-slate-900">Product Categories</h2>
              <p className="text-sm text-slate-600 mt-1">Manage your product categories</p>
            </div>
            <button className="bg-indigo-600 text-white px-4 py-2 rounded-lg font-semibold text-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md flex items-center gap-2">
              <Plus className="size-4" />
              New Category
            </button>
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
                        Name
                      </th>
                      <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                        Description
                      </th>
                      <th className="text-left px-6 py-3 text-xs font-semibold text-slate-700 uppercase tracking-wider">
                        Actions
                      </th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-200">
                    {categories.map((category) => (
                      <tr key={category.id} className="hover:bg-slate-50 transition-colors">
                        <td className="px-6 py-4 text-sm font-semibold text-slate-900">
                          {category.name}
                        </td>
                        <td className="px-6 py-4 text-sm text-slate-600">
                          {category.description}
                        </td>
                        <td className="px-6 py-4">
                          <div className="flex items-center gap-2">
                            <button 
                              className="p-2 text-slate-600 hover:text-indigo-600 hover:bg-indigo-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500"
                              aria-label="Edit category"
                            >
                              <Edit className="size-4" />
                            </button>
                            <button 
                              onClick={() => handleDelete(category.id)}
                              className="p-2 text-slate-600 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors focus:outline-none focus:ring-2 focus:ring-red-500"
                              aria-label="Delete category"
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
            </div>
          )}
        </main>
      </div>
    </div>
  );
}
