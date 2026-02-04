import { useNavigate } from 'react-router-dom';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import { Upload, X } from 'lucide-react';

export default function ProductForm() {
  const navigate = useNavigate();

  return (
    <div className="flex min-h-screen bg-slate-50">
      <AdminSidebar />
      
      <div className="flex-1 flex flex-col min-w-0">
        <AdminTopbar title="Create Product" />
        
        <main className="flex-1 p-6">
          <form className="max-w-4xl">
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
              <div className="lg:col-span-2 space-y-6">
                <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm">
                  <h2 className="font-semibold text-slate-900 mb-4">Basic Information</h2>
                  
                  <div className="space-y-4">
                    <div>
                      <label htmlFor="productName" className="block text-sm font-semibold text-slate-900 mb-2">
                        Product Name
                      </label>
                      <input
                        id="productName"
                        type="text"
                        placeholder="Premium Wireless Headphones"
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                      />
                      <p className="mt-1 text-xs text-slate-500">
                        Enter a clear and descriptive product name
                      </p>
                    </div>

                    <div className="grid grid-cols-2 gap-4">
                      <div>
                        <label htmlFor="category" className="block text-sm font-semibold text-slate-900 mb-2">
                          Category
                        </label>
                        <select
                          id="category"
                          className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                        >
                          <option>Select category</option>
                          <option>Electronics</option>
                          <option>Fashion</option>
                          <option>Home & Living</option>
                          <option>Sports</option>
                        </select>
                      </div>

                      <div>
                        <label htmlFor="price" className="block text-sm font-semibold text-slate-900 mb-2">
                          Price
                        </label>
                        <div className="relative">
                          <span className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-500">$</span>
                          <input
                            id="price"
                            type="number"
                            placeholder="299"
                            className="w-full pl-8 pr-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                          />
                        </div>
                      </div>
                    </div>

                    <div>
                      <label htmlFor="stock" className="block text-sm font-semibold text-slate-900 mb-2">
                        Stock Quantity
                      </label>
                      <input
                        id="stock"
                        type="number"
                        placeholder="100"
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                      />
                    </div>

                    <div>
                      <label htmlFor="description" className="block text-sm font-semibold text-slate-900 mb-2">
                        Description
                      </label>
                      <textarea
                        id="description"
                        rows={6}
                        placeholder="Enter product description..."
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 resize-none"
                      ></textarea>
                      <p className="mt-1 text-xs text-slate-500">
                        Provide detailed information about the product
                      </p>
                    </div>
                  </div>
                </div>
              </div>

              <div className="lg:col-span-1">
                <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm sticky top-24">
                  <h2 className="font-semibold text-slate-900 mb-4">Product Images</h2>
                  
                  <div className="border-2 border-dashed border-slate-200 rounded-lg p-8 text-center hover:border-indigo-300 hover:bg-indigo-50/50 transition-colors cursor-pointer">
                    <div className="bg-slate-100 size-12 rounded-full flex items-center justify-center mx-auto mb-3">
                      <Upload className="size-6 text-slate-400" />
                    </div>
                    <div className="text-sm font-semibold text-slate-900 mb-1">Upload Images</div>
                    <div className="text-xs text-slate-500 mb-3">
                      PNG, JPG up to 10MB
                    </div>
                    <button 
                      type="button"
                      className="px-4 py-2 bg-slate-100 text-slate-700 rounded-lg text-sm font-semibold hover:bg-slate-200 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-colors"
                    >
                      Choose Files
                    </button>
                  </div>

                  <div className="mt-4 space-y-3">
                    <div className="text-xs font-semibold text-slate-700 uppercase tracking-wider">Preview</div>
                    {[1, 2].map((i) => (
                      <div key={i} className="flex items-center gap-3 p-3 border border-slate-200 rounded-lg">
                        <div className="bg-slate-100 size-12 rounded-lg flex-shrink-0">
                          <div className="w-full h-full bg-gradient-to-br from-slate-200 to-slate-100 rounded-lg"></div>
                        </div>
                        <div className="flex-1 min-w-0">
                          <div className="text-sm text-slate-900 truncate">image-{i}.jpg</div>
                          <div className="text-xs text-slate-500">2.4 MB</div>
                        </div>
                        <button 
                          type="button"
                          className="text-slate-400 hover:text-red-600 focus:outline-none"
                        >
                          <X className="size-4" />
                        </button>
                      </div>
                    ))}
                  </div>
                </div>
              </div>
            </div>

            <div className="flex gap-3 mt-6">
              <button 
                type="submit"
                onClick={(e) => {
                  e.preventDefault();
                  navigate('/admin/products');
                }}
                className="px-6 py-3 bg-indigo-600 text-white rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md"
              >
                Save Product
              </button>
              <button 
                type="button"
                onClick={() => navigate('/admin/products')}
                className="px-6 py-3 border border-slate-200 text-slate-700 rounded-lg font-semibold hover:bg-slate-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-colors"
              >
                Cancel
              </button>
            </div>
          </form>
        </main>
      </div>
    </div>
  );
}
