import {
  Product,
  PaginatedResponse,
  CreateProductRequest,
  UpdateProductRequest,
  GetProductsRequest,
  Category,
  UnitOfMeasure,
  IGVType,
} from "@/types/api";
import { apiGet, apiPost, apiPut } from "./client";

export const productsApi = {
  getAll: async (params?: GetProductsRequest) => {
    const queryParams = new URLSearchParams();
    if (params?.pageNumber) queryParams.append("pageNumber", params.pageNumber.toString());
    if (params?.pageSize) queryParams.append("pageSize", params.pageSize.toString());

    const query = queryParams.toString();
    const endpoint = `/api/product${query ? `?${query}` : ""}`;

    return apiGet<PaginatedResponse<Product>>(endpoint);
  },

  create: async (data: CreateProductRequest) => {
    return apiPost<Product, CreateProductRequest>("/api/product", data);
  },

  update: async (id: string, data: UpdateProductRequest) => {
    return apiPut<Product, UpdateProductRequest>(`/api/product/${id}`, data);
  },

  // Reference data
  getCategories: async () => {
    return apiGet<Category[]>("/api/category");
  },

  getUnitsOfMeasure: async () => {
    return apiGet<UnitOfMeasure[]>("/api/unitofmeasure");
  },

  getIGVTypes: async () => {
    return apiGet<IGVType[]>("/api/igvtype");
  },
};
