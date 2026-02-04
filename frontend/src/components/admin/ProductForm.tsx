import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import AdminSidebar from './AdminSidebar';
import AdminTopbar from './AdminTopbar';
import { Upload, X, Star, Plus, Edit2 } from 'lucide-react';
import { productsApi, categoriesApi } from '../../api';
import { handleApiError, showSuccess } from '../../utils/errorHandler';
import { useConfirm } from '../../context/ConfirmDialogContext';
import type { CategoryResponse, ProductImage, ProductSpecification } from '../../types/api';

export default function ProductForm() {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const confirm = useConfirm();
  const isEditMode = !!id;

  const [formData, setFormData] = useState({
    name: '',
    description: '',
    price: 0,
    stock: 0,
    categoryId: '',
  });
  const [categories, setCategories] = useState<CategoryResponse[]>([]);
  const [images, setImages] = useState<ProductImage[]>([]);
  const [specifications, setSpecifications] = useState<ProductSpecification[]>([]);
  const [pendingImages, setPendingImages] = useState<File[]>([]);
  const [pendingSpecs, setPendingSpecs] = useState<Array<{ key: string; value: string }>>([]);
  const [specFormData, setSpecFormData] = useState({ key: '', value: '' });
  const [editingSpecId, setEditingSpecId] = useState<string | null>(null);
  const [editingSpecIndex, setEditingSpecIndex] = useState<number | null>(null);
  const [uploadingImage, setUploadingImage] = useState(false);
  const [savingSpec, setSavingSpec] = useState(false);
  const [loading, setLoading] = useState(false);
  const [loadingData, setLoadingData] = useState(isEditMode);

  useEffect(() => {
    loadCategories();
    if (isEditMode && id) {
      loadProduct(id);
    }
  }, [id]);

  const loadCategories = async () => {
    try {
      const response = await categoriesApi.getCategories(0, 100);
      setCategories(response.items);
    } catch (error) {
      handleApiError(error);
    }
  };

  const loadProduct = async (productId: string) => {
    setLoadingData(true);
    try {
      const product = await productsApi.getProductById(productId);
      setFormData({
        name: product.name,
        description: product.description,
        price: product.price,
        stock: product.stock,
        categoryId: product.categoryId,
      });
      setImages(product.images || []);
      setSpecifications(product.specifications || []);
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoadingData(false);
    }
  };

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files || !e.target.files[0] || !id) return;
    
    const file = e.target.files[0];
    setUploadingImage(true);
    
    try {
      const displayOrder = images.length;
      await productsApi.uploadProductImage(id, file, displayOrder);
      showSuccess('Image uploaded successfully');
      await loadProduct(id);
    } catch (error) {
      handleApiError(error);
    } finally {
      setUploadingImage(false);
    }
  };

  const handleDeleteImage = async (imageId: string) => {
    if (!id) return;

    const confirmed = await confirm({
      title: 'Delete Image',
      message: 'Are you sure you want to delete this image? This action cannot be undone.',
      type: 'danger',
      confirmText: 'Delete',
      cancelText: 'Cancel'
    });

    if (!confirmed) return;
    
    try {
      await productsApi.deleteProductImage(id, imageId);
      showSuccess('Image deleted successfully');
      await loadProduct(id);
    } catch (error) {
      handleApiError(error);
    }
  };

  const handleSetPrimaryImage = async (imageId: string) => {
    if (!id) return;
    
    try {
      await productsApi.setPrimaryImage(id, imageId);
      showSuccess('Primary image updated successfully');
      await loadProduct(id);
    } catch (error) {
      handleApiError(error);
    }
  };

  const handleSpecSubmit = async () => {
    if (!specFormData.key.trim() || !specFormData.value.trim()) return;

    if (isEditMode && id) {
      setSavingSpec(true);
      try {
        if (editingSpecId) {
          await productsApi.updateSpecification(id, editingSpecId, specFormData);
          showSuccess('Specification updated successfully');
        } else {
          await productsApi.createSpecification(id, specFormData);
          showSuccess('Specification added successfully');
        }
        setSpecFormData({ key: '', value: '' });
        setEditingSpecId(null);
        await loadProduct(id);
      } catch (error) {
        handleApiError(error);
      } finally {
        setSavingSpec(false);
      }
    } else {
      if (editingSpecIndex !== null) {
        const updated = [...pendingSpecs];
        updated[editingSpecIndex] = specFormData;
        setPendingSpecs(updated);
        setEditingSpecIndex(null);
      } else {
        const isDuplicate = pendingSpecs.some(
          spec => spec.key.toLowerCase() === specFormData.key.toLowerCase() && 
                  spec.value.toLowerCase() === specFormData.value.toLowerCase()
        );
        
        if (isDuplicate) {
          handleApiError({ message: 'This specification already exists' });
          return;
        }
        
        setPendingSpecs([...pendingSpecs, specFormData]);
      }
      setSpecFormData({ key: '', value: '' });
    }
  };

  const handleEditSpec = (spec: ProductSpecification) => {
    setSpecFormData({ key: spec.key, value: spec.value });
    setEditingSpecId(spec.id);
  };

  const handleEditPendingSpec = (index: number) => {
    setSpecFormData(pendingSpecs[index]);
    setEditingSpecIndex(index);
  };

  const handleCancelSpecEdit = () => {
    setSpecFormData({ key: '', value: '' });
    setEditingSpecId(null);
    setEditingSpecIndex(null);
  };

  const handleDeleteSpec = async (specId: string) => {
    if (!id) return;

    const confirmed = await confirm({
      title: 'Delete Specification',
      message: 'Are you sure you want to delete this specification?',
      type: 'danger',
      confirmText: 'Delete',
      cancelText: 'Cancel'
    });

    if (!confirmed) return;

    try {
      await productsApi.deleteSpecification(id, specId);
      showSuccess('Specification deleted successfully');
      await loadProduct(id);
    } catch (error) {
      handleApiError(error);
    }
  };

  const handleDeletePendingSpec = async (index: number) => {
    const confirmed = await confirm({
      title: 'Delete Specification',
      message: 'Are you sure you want to delete this specification?',
      type: 'danger',
      confirmText: 'Delete',
      cancelText: 'Cancel'
    });

    if (!confirmed) return;

    setPendingSpecs(pendingSpecs.filter((_, i) => i !== index));
  };

  const handlePendingImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files || !e.target.files[0]) return;
    
    const files = Array.from(e.target.files);
    setPendingImages([...pendingImages, ...files]);
  };

  const handleDeletePendingImage = async (index: number) => {
    const confirmed = await confirm({
      title: 'Delete Image',
      message: 'Are you sure you want to delete this image?',
      type: 'danger',
      confirmText: 'Delete',
      cancelText: 'Cancel'
    });

    if (!confirmed) return;

    setPendingImages(pendingImages.filter((_, i) => i !== index));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      if (isEditMode && id) {
        await productsApi.updateProduct(id, formData);
        showSuccess('Product updated successfully');
        navigate('/admin/products');
      } else {
        const createdProduct = await productsApi.createProduct(formData);
        const productId = createdProduct.id;

        for (const spec of pendingSpecs) {
          await productsApi.createSpecification(productId, spec);
        }

        for (let i = 0; i < pendingImages.length; i++) {
          const file = pendingImages[i];
          await productsApi.uploadProductImage(productId, file, i);
        }

        if (pendingImages.length > 0) {
          const firstImageResponse = await productsApi.getProductById(productId);
          if (firstImageResponse.images && firstImageResponse.images.length > 0) {
            await productsApi.setPrimaryImage(productId, firstImageResponse.images[0].id);
          }
        }

        showSuccess('Product created successfully');
        navigate('/admin/products');
      }
    } catch (error) {
      handleApiError(error);
    } finally {
      setLoading(false);
    }
  };

  if (loadingData) {
    return (
      <div className="flex min-h-screen bg-slate-50">
        <AdminSidebar />
        <div className="flex-1 flex items-center justify-center">
          <div className="text-slate-600">Loading...</div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex min-h-screen bg-slate-50">
      <AdminSidebar />
      
      <div className="flex-1 flex flex-col min-w-0">
        <AdminTopbar title={isEditMode ? 'Edit Product' : 'Create Product'} />
        
        <main className="flex-1 p-6">
          <form onSubmit={handleSubmit} className="max-w-4xl">
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
                        required
                        placeholder="Premium Wireless Headphones"
                        value={formData.name}
                        onChange={(e) => setFormData({ ...formData, name: e.target.value })}
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
                          required
                          value={formData.categoryId}
                          onChange={(e) => setFormData({ ...formData, categoryId: e.target.value })}
                          className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                        >
                          <option value="">Select category</option>
                          {categories.map((category) => (
                            <option key={category.id} value={category.id}>
                              {category.name}
                            </option>
                          ))}
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
                            required
                            min="0"
                            step="0.01"
                            placeholder="299"
                            value={formData.price || ''}
                            onChange={(e) => setFormData({ ...formData, price: parseFloat(e.target.value) || 0 })}
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
                        required
                        min="0"
                        placeholder="100"
                        value={formData.stock || ''}
                        onChange={(e) => setFormData({ ...formData, stock: parseInt(e.target.value) || 0 })}
                        className="w-full px-4 py-2 border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                      />
                    </div>

                    <div>
                      <label htmlFor="description" className="block text-sm font-semibold text-slate-900 mb-2">
                        Description
                      </label>
                      <textarea
                        id="description"
                        required
                        rows={6}
                        placeholder="Enter product description..."
                        value={formData.description}
                        onChange={(e) => setFormData({ ...formData, description: e.target.value })}
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
                  
                  <div className="space-y-4">
                    <div className="border-2 border-dashed border-slate-200 rounded-lg p-6 text-center hover:border-indigo-300 hover:bg-indigo-50/50 transition-colors cursor-pointer">
                      <input
                        type="file"
                        accept="image/*"
                        multiple={!isEditMode}
                        onChange={isEditMode ? handleImageUpload : handlePendingImageUpload}
                        disabled={uploadingImage}
                        className="hidden"
                        id="imageUpload"
                      />
                      <label htmlFor="imageUpload" className="cursor-pointer">
                        <div className="bg-slate-100 size-12 rounded-full flex items-center justify-center mx-auto mb-3">
                          <Upload className="size-6 text-slate-400" />
                        </div>
                        <div className="text-sm font-semibold text-slate-900 mb-1">
                          {uploadingImage ? 'Uploading...' : 'Upload Image'}
                        </div>
                        <div className="text-xs text-slate-500">
                          {isEditMode ? 'Click to select an image' : 'Click to select images'}
                        </div>
                      </label>
                    </div>

                    {isEditMode && images.length > 0 && (
                      <div className="space-y-2">
                        {images.map((image) => (
                          <div key={image.id} className="relative group border border-slate-200 rounded-lg p-2">
                            <div className="flex items-center gap-2">
                              <img
                                src={image.imageUrl}
                                alt="Product"
                                className="w-16 h-16 object-cover rounded"
                              />
                              <div className="flex-1 min-w-0">
                                <div className="text-xs text-slate-600 truncate">Image {image.displayOrder + 1}</div>
                                {image.isPrimary && (
                                  <div className="flex items-center gap-1 text-amber-600 text-xs">
                                    <Star className="size-3 fill-current" />
                                    Primary
                                  </div>
                                )}
                              </div>
                              <div className="flex gap-1">
                                {!image.isPrimary && (
                                  <button
                                    type="button"
                                    onClick={() => handleSetPrimaryImage(image.id)}
                                    className="p-1 text-slate-400 hover:text-amber-600 transition-colors"
                                    title="Set as primary"
                                  >
                                    <Star className="size-4" />
                                  </button>
                                )}
                                <button
                                  type="button"
                                  onClick={() => handleDeleteImage(image.id)}
                                  className="p-1 text-slate-400 hover:text-red-600 transition-colors"
                                  title="Delete image"
                                >
                                  <X className="size-4" />
                                </button>
                              </div>
                            </div>
                          </div>
                        ))}
                      </div>
                    )}

                    {!isEditMode && pendingImages.length > 0 && (
                      <div className="space-y-2">
                        {pendingImages.map((file, index) => (
                          <div key={index} className="relative group border border-slate-200 rounded-lg p-2">
                            <div className="flex items-center gap-2">
                              <img
                                src={URL.createObjectURL(file)}
                                alt="Preview"
                                className="w-16 h-16 object-cover rounded"
                              />
                              <div className="flex-1 min-w-0">
                                <div className="text-xs text-slate-600 truncate">{file.name}</div>
                                {index === 0 && (
                                  <div className="flex items-center gap-1 text-amber-600 text-xs">
                                    <Star className="size-3 fill-current" />
                                    Will be primary
                                  </div>
                                )}
                              </div>
                              <button
                                type="button"
                                onClick={() => handleDeletePendingImage(index)}
                                className="p-1 text-slate-400 hover:text-red-600 transition-colors"
                                title="Delete image"
                              >
                                <X className="size-4" />
                              </button>
                            </div>
                          </div>
                        ))}
                      </div>
                    )}
                  </div>
                </div>

                <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm sticky top-24 mt-6">
                  <h2 className="font-semibold text-slate-900 mb-4">Specifications</h2>
                  
                  <div className="space-y-3 mb-4">
                    <div>
                      <input
                        type="text"
                        placeholder="Key (e.g., Brand)"
                        value={specFormData.key}
                        onChange={(e) => setSpecFormData({ ...specFormData, key: e.target.value })}
                        className="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                      />
                    </div>
                    <div>
                      <input
                        type="text"
                        placeholder="Value (e.g., Sony)"
                        value={specFormData.value}
                        onChange={(e) => setSpecFormData({ ...specFormData, value: e.target.value })}
                        onKeyDown={(e) => {
                          if (e.key === 'Enter') {
                            e.preventDefault();
                            if (specFormData.key.trim() && specFormData.value.trim()) {
                              handleSpecSubmit();
                            }
                          }
                        }}
                        className="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500"
                      />
                    </div>
                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={handleSpecSubmit}
                        disabled={savingSpec || !specFormData.key.trim() || !specFormData.value.trim()}
                        className="flex-1 px-3 py-2 text-sm bg-indigo-600 text-white rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-colors disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-1"
                      >
                        {(editingSpecId || editingSpecIndex !== null) ? <Edit2 className="size-3" /> : <Plus className="size-3" />}
                        {savingSpec ? 'Saving...' : (editingSpecId || editingSpecIndex !== null) ? 'Update' : 'Add'}
                      </button>
                      {(editingSpecId || editingSpecIndex !== null) && (
                        <button
                          type="button"
                          onClick={handleCancelSpecEdit}
                          className="px-3 py-2 text-sm border border-slate-200 text-slate-700 rounded-lg font-semibold hover:bg-slate-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 transition-colors"
                        >
                          Cancel
                        </button>
                      )}
                    </div>
                  </div>

                  {isEditMode && specifications.length > 0 && (
                    <div className="space-y-2">
                      {specifications.map((spec) => (
                        <div key={spec.id} className="flex items-start gap-2 p-2 border border-slate-200 rounded-lg bg-slate-50">
                          <div className="flex-1 min-w-0">
                            <div className="text-xs font-semibold text-slate-900">{spec.key}</div>
                            <div className="text-xs text-slate-600 truncate">{spec.value}</div>
                          </div>
                          <div className="flex gap-1">
                            <button
                              type="button"
                              onClick={() => handleEditSpec(spec)}
                              className="p-1 text-slate-400 hover:text-indigo-600 transition-colors"
                              title="Edit specification"
                            >
                              <Edit2 className="size-3" />
                            </button>
                            <button
                              type="button"
                              onClick={() => handleDeleteSpec(spec.id)}
                              className="p-1 text-slate-400 hover:text-red-600 transition-colors"
                              title="Delete specification"
                            >
                              <X className="size-3" />
                            </button>
                          </div>
                        </div>
                      ))}
                    </div>
                  )}

                  {!isEditMode && pendingSpecs.length > 0 && (
                    <div className="space-y-2">
                      {pendingSpecs.map((spec, index) => (
                        <div key={index} className="flex items-start gap-2 p-2 border border-slate-200 rounded-lg bg-slate-50">
                          <div className="flex-1 min-w-0">
                            <div className="text-xs font-semibold text-slate-900">{spec.key}</div>
                            <div className="text-xs text-slate-600 truncate">{spec.value}</div>
                          </div>
                          <div className="flex gap-1">
                            <button
                              type="button"
                              onClick={() => handleEditPendingSpec(index)}
                              className="p-1 text-slate-400 hover:text-indigo-600 transition-colors"
                              title="Edit specification"
                            >
                              <Edit2 className="size-3" />
                            </button>
                            <button
                              type="button"
                              onClick={() => handleDeletePendingSpec(index)}
                              className="p-1 text-slate-400 hover:text-red-600 transition-colors"
                              title="Delete specification"
                            >
                              <X className="size-3" />
                            </button>
                          </div>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
              </div>
            </div>

            <div className="flex gap-3 mt-6">
              <button 
                type="submit"
                disabled={loading}
                className="px-6 py-3 bg-indigo-600 text-white rounded-lg font-semibold hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 transition-colors shadow-sm hover:shadow-md disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {loading ? 'Saving...' : isEditMode ? 'Update Product' : 'Save Product'}
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
