import { ApiResponse } from "@/types/api";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7190";

export class ApiError extends Error {
  constructor(
    public statusCode: number,
    public errors: string[],
    message?: string
  ) {
    super(message || errors.join(", "));
    this.name = "ApiError";
  }
}

async function handleResponse<T>(response: Response): Promise<T> {
  const data = await response.json();

  if (!response.ok) {
    throw new ApiError(
      response.status,
      data.errors || ["An error occurred"],
      data.message
    );
  }

  // Handle wrapped API responses
  if (data.isSuccess !== undefined) {
    if (!data.isSuccess) {
      throw new ApiError(data.statusCode, data.errors || ["API error"]);
    }
    return data.value;
  }

  return data;
}

export async function apiGet<T>(endpoint: string): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  return handleResponse<T>(response);
}

export async function apiPost<T, D = unknown>(
  endpoint: string,
  data: D
): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  return handleResponse<T>(response);
}

export async function apiPut<T, D = unknown>(
  endpoint: string,
  data: D
): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  return handleResponse<T>(response);
}

export async function apiDelete<T>(endpoint: string): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  });

  return handleResponse<T>(response);
}
