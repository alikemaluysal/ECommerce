import type { Product, Category } from '../types';
import { mockProducts, mockCategories } from '../data/mockData';

class ProductService {
  private products: Product[] = [...mockProducts];

  async getAllProducts(): Promise<Product[]> {
    return new Promise((resolve) => {
      setTimeout(() => resolve([...this.products]), 100);
    });
  }

  async getProductById(id: number): Promise<Product | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const product = this.products.find((p) => p.id === id);
        resolve(product);
      }, 100);
    });
  }

  async getProductBySlug(slug: string): Promise<Product | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const product = this.products.find((p) => p.slug === slug);
        resolve(product);
      }, 100);
    });
  }

  async getProductsByCategory(categoryId: number): Promise<Product[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const filtered = this.products.filter((p) => p.categoryId === categoryId);
        resolve(filtered);
      }, 100);
    });
  }

  async filterProducts(filters: {
    categoryId?: number;
    minPrice?: number;
    maxPrice?: number;
    inStock?: boolean;
    search?: string;
  }): Promise<Product[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        let filtered = [...this.products];

        if (filters.categoryId) {
          filtered = filtered.filter((p) => p.categoryId === filters.categoryId);
        }

        if (filters.minPrice !== undefined) {
          filtered = filtered.filter((p) => p.price >= filters.minPrice!);
        }

        if (filters.maxPrice !== undefined) {
          filtered = filtered.filter((p) => p.price <= filters.maxPrice!);
        }

        if (filters.inStock) {
          filtered = filtered.filter((p) => p.stock > 0);
        }

        if (filters.search) {
          const searchLower = filters.search.toLowerCase();
          filtered = filtered.filter(
            (p) =>
              p.name.toLowerCase().includes(searchLower) ||
              p.description.toLowerCase().includes(searchLower)
          );
        }

        resolve(filtered);
      }, 100);
    });
  }

  sortProducts(products: Product[], sortBy: string): Product[] {
    const sorted = [...products];

    switch (sortBy) {
      case 'price-low-high':
        return sorted.sort((a, b) => a.price - b.price);
      case 'price-high-low':
        return sorted.sort((a, b) => b.price - a.price);
      case 'newest':
        return sorted.sort((a, b) => (a.isNew === b.isNew ? 0 : a.isNew ? -1 : 1));
      case 'rating':
        return sorted.sort((a, b) => b.rating - a.rating);
      default:
        return sorted;
    }
  }

  async getFeaturedProducts(limit: number = 8): Promise<Product[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const featured = this.products.slice(0, limit);
        resolve(featured);
      }, 100);
    });
  }

  async getNewArrivals(limit: number = 8): Promise<Product[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const newProducts = this.products.filter((p) => p.isNew).slice(0, limit);
        resolve(newProducts);
      }, 100);
    });
  }

  async createProduct(product: Omit<Product, 'id'>): Promise<Product> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const newProduct = {
          ...product,
          id: Math.max(...this.products.map((p) => p.id), 0) + 1,
        };
        this.products.push(newProduct);
        resolve(newProduct);
      }, 100);
    });
  }

  async updateProduct(id: number, updates: Partial<Product>): Promise<Product | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const index = this.products.findIndex((p) => p.id === id);
        if (index !== -1) {
          this.products[index] = { ...this.products[index], ...updates };
          resolve(this.products[index]);
        } else {
          resolve(undefined);
        }
      }, 100);
    });
  }

  async deleteProduct(id: number): Promise<boolean> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const index = this.products.findIndex((p) => p.id === id);
        if (index !== -1) {
          this.products.splice(index, 1);
          resolve(true);
        } else {
          resolve(false);
        }
      }, 100);
    });
  }
}

class CategoryService {
  private categories: Category[] = [...mockCategories];

  async getAllCategories(): Promise<Category[]> {
    return new Promise((resolve) => {
      setTimeout(() => resolve([...this.categories]), 100);
    });
  }

  async getCategoryById(id: number): Promise<Category | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const category = this.categories.find((c) => c.id === id);
        resolve(category);
      }, 100);
    });
  }

  async getCategoryBySlug(slug: string): Promise<Category | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const category = this.categories.find((c) => c.slug === slug);
        resolve(category);
      }, 100);
    });
  }

  async createCategory(category: Omit<Category, 'id' | 'productCount'>): Promise<Category> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const newCategory = {
          ...category,
          id: Math.max(...this.categories.map((c) => c.id), 0) + 1,
          productCount: 0,
        };
        this.categories.push(newCategory);
        resolve(newCategory);
      }, 100);
    });
  }

  async updateCategory(id: number, updates: Partial<Category>): Promise<Category | undefined> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const index = this.categories.findIndex((c) => c.id === id);
        if (index !== -1) {
          this.categories[index] = { ...this.categories[index], ...updates };
          resolve(this.categories[index]);
        } else {
          resolve(undefined);
        }
      }, 100);
    });
  }

  async deleteCategory(id: number): Promise<boolean> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const index = this.categories.findIndex((c) => c.id === id);
        if (index !== -1) {
          this.categories.splice(index, 1);
          resolve(true);
        } else {
          resolve(false);
        }
      }, 100);
    });
  }
}

export const productService = new ProductService();
export const categoryService = new CategoryService();
