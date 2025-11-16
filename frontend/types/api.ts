// API Response types
export interface ApiResponse<T> {
  isSuccess: boolean;
  value: T;
  errors: string[] | null;
  statusCode: number;
}

export interface PaginatedResponse<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

// Entity types
export interface Product {
  id: string;
  code: string;
  name: string;
  description?: string;
  purchasePrice: number;
  salePrice: number;
  unitOfMeasureId: string;
  igvTypeId: string;
  categoryId: string;
  createdAt: string;
  updatedAt?: string;
}

export interface Category {
  id: string;
  name: string;
  description?: string;
}

export interface UnitOfMeasure {
  id: string;
  code: string;
  name: string;
  description?: string;
}

export interface IGVType {
  id: string;
  code: string;
  name: string;
  percentage: number;
}

// Request types
export interface CreateProductRequest {
  code: string;
  name: string;
  description?: string;
  purchasePrice: number;
  salePrice: number;
  unitOfMeasureId: string;
  igvTypeId: string;
  categoryId: string;
}

export interface UpdateProductRequest extends CreateProductRequest {
  id: string;
}

export interface GetProductsRequest {
  pageNumber?: number;
  pageSize?: number;
}

export interface SearchProductsRequest {
  term: string;
  warehouseId: string;
  pageNumber?: number;
  pageSize?: number;
}
