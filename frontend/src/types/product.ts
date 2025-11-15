export interface Product {
  id: string;
  unitOfMeasureId: string;
  unitOfMeasure: string;
  igvTypeId: string;
  igvType: string;
  categoryId: string;
  category: string;
  code: string;
  name: string;
  description: string;
  purchasePrice: number;
  salePrice: number;
}

export interface CreateProductDto {
  unitOfMeasureId: string;
  igvTypeId: string;
  categoryId: string;
  code: string;
  name: string;
  description: string;
  purchasePrice: number;
  salePrice: number;
}

export interface UpdateProductDto {
  id: string;
  unitOfMeasureId: string;
  igvTypeId: string;
  categoryId: string;
  code: string;
  name: string;
  description: string;
  purchasePrice: number;
  salePrice: number;
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

export interface ApiResponse<T> {
  isSuccess: boolean;
  value?: T;
  errors?: string[];
  statusCode: number;
}

// Para los selects
export interface Category {
  id: string;
  code: string;
  name: string;
  description: string;
}

export interface UnitOfMeasure {
  id: string;
  code: string;
  name: string;
  description: string;
}

export interface IGVType {
  id: string;
  code: string;
  name: string;
  description: string;
}
