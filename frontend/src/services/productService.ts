import { apiClient } from './api';
import type {
  Product,
  CreateProductDto,
  UpdateProductDto,
  PaginatedResponse,
  ApiResponse,
  Category,
  UnitOfMeasure,
  IGVType,
} from '@/types/product';

export const productService = {
  // Obtener todos los productos con paginación
  async getAll(pageNumber: number = 1, pageSize: number = 10): Promise<ApiResponse<PaginatedResponse<Product>>> {
    const response = await apiClient.get<ApiResponse<PaginatedResponse<Product>>>(
      `/product?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
    return response.data;
  },

  // Crear un producto
  async create(product: CreateProductDto): Promise<ApiResponse<Product>> {
    const response = await apiClient.post<ApiResponse<Product>>('/product', product);
    return response.data;
  },

  // Actualizar un producto
  async update(product: UpdateProductDto): Promise<ApiResponse<Product>> {
    const response = await apiClient.put<ApiResponse<Product>>(`/product/${product.id}`, product);
    return response.data;
  },

  // Eliminar un producto (si existe en el backend)
  async delete(id: string): Promise<ApiResponse<void>> {
    const response = await apiClient.delete<ApiResponse<void>>(`/product/${id}`);
    return response.data;
  },

  // Buscar productos
  async search(searchTerm: string = '', warehouseId: string = '', pageNumber: number = 1, pageSize: number = 10) {
    const response = await apiClient.get(
      `/product/search?searchTerm=${searchTerm}&warehouseId=${warehouseId}&pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
    return response.data;
  },
};

// Servicios para obtener catálogos
export const catalogService = {
  async getCategories(): Promise<ApiResponse<Category[]>> {
    const response = await apiClient.get<ApiResponse<Category[]>>('/category');
    return response.data;
  },

  async getUnitOfMeasures(): Promise<ApiResponse<UnitOfMeasure[]>> {
    const response = await apiClient.get<ApiResponse<UnitOfMeasure[]>>('/unitofmeasure');
    return response.data;
  },

  async getIGVTypes(): Promise<ApiResponse<IGVType[]>> {
    const response = await apiClient.get<ApiResponse<IGVType[]>>('/igvtype');
    return response.data;
  },
};
