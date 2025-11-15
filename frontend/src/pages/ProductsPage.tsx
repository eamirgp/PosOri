import { useEffect, useState } from "react"
import { Plus, Pencil } from "lucide-react"
import { Button } from "@/components/ui/button"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import { Pagination } from "@/components/ui/pagination"
import { ProductFormModal } from "@/components/ProductFormModal"
import { productService } from "@/services/productService"
import type { Product, CreateProductDto, UpdateProductDto } from "@/types/product"

export function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([])
  const [loading, setLoading] = useState(true)
  const [currentPage, setCurrentPage] = useState(1)
  const [totalPages, setTotalPages] = useState(1)
  const [hasNextPage, setHasNextPage] = useState(false)
  const [hasPreviousPage, setHasPreviousPage] = useState(false)
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false)
  const [isEditModalOpen, setIsEditModalOpen] = useState(false)
  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null)
  const pageSize = 10

  useEffect(() => {
    loadProducts()
  }, [currentPage])

  const loadProducts = async () => {
    setLoading(true)
    try {
      const response = await productService.getAll(currentPage, pageSize)
      if (response.isSuccess && response.value) {
        setProducts(response.value.items)
        setTotalPages(response.value.totalPages)
        setHasNextPage(response.value.hasNextPage)
        setHasPreviousPage(response.value.hasPreviousPage)
      }
    } catch (error) {
      console.error("Error loading products:", error)
    } finally {
      setLoading(false)
    }
  }

  const handleCreateProduct = async (product: CreateProductDto | UpdateProductDto) => {
    try {
      const response = await productService.create(product as CreateProductDto)
      if (response.isSuccess) {
        await loadProducts()
      }
    } catch (error) {
      console.error("Error creating product:", error)
    }
  }

  const handleUpdateProduct = async (product: CreateProductDto | UpdateProductDto) => {
    try {
      const response = await productService.update(product as UpdateProductDto)
      if (response.isSuccess) {
        await loadProducts()
      }
    } catch (error) {
      console.error("Error updating product:", error)
    }
  }

  const handleEditClick = (product: Product) => {
    setSelectedProduct(product)
    setIsEditModalOpen(true)
  }

  return (
    <div className="container mx-auto py-10">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-3xl font-bold">Productos</h1>
        <Button onClick={() => setIsCreateModalOpen(true)}>
          <Plus className="mr-2 h-4 w-4" />
          Nuevo Producto
        </Button>
      </div>

      {loading ? (
        <div className="text-center py-10">Cargando productos...</div>
      ) : (
        <>
          <div className="rounded-md border">
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>Código</TableHead>
                  <TableHead>Nombre</TableHead>
                  <TableHead>Descripción</TableHead>
                  <TableHead>Categoría</TableHead>
                  <TableHead>Unidad</TableHead>
                  <TableHead>Tipo IGV</TableHead>
                  <TableHead className="text-right">Precio Compra</TableHead>
                  <TableHead className="text-right">Precio Venta</TableHead>
                  <TableHead className="text-right">Acciones</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {products.length === 0 ? (
                  <TableRow>
                    <TableCell colSpan={9} className="text-center">
                      No hay productos registrados
                    </TableCell>
                  </TableRow>
                ) : (
                  products.map((product) => (
                    <TableRow key={product.id}>
                      <TableCell className="font-medium">{product.code}</TableCell>
                      <TableCell>{product.name}</TableCell>
                      <TableCell>{product.description}</TableCell>
                      <TableCell>{product.category}</TableCell>
                      <TableCell>{product.unitOfMeasure}</TableCell>
                      <TableCell>{product.igvType}</TableCell>
                      <TableCell className="text-right">
                        S/ {product.purchasePrice.toFixed(2)}
                      </TableCell>
                      <TableCell className="text-right">
                        S/ {product.salePrice.toFixed(2)}
                      </TableCell>
                      <TableCell className="text-right">
                        <Button
                          variant="ghost"
                          size="sm"
                          onClick={() => handleEditClick(product)}
                        >
                          <Pencil className="h-4 w-4" />
                        </Button>
                      </TableCell>
                    </TableRow>
                  ))
                )}
              </TableBody>
            </Table>
          </div>

          <div className="mt-4">
            <Pagination
              currentPage={currentPage}
              totalPages={totalPages}
              onPageChange={setCurrentPage}
              hasNextPage={hasNextPage}
              hasPreviousPage={hasPreviousPage}
            />
          </div>
        </>
      )}

      <ProductFormModal
        open={isCreateModalOpen}
        onOpenChange={setIsCreateModalOpen}
        onSubmit={handleCreateProduct}
        mode="create"
      />

      <ProductFormModal
        open={isEditModalOpen}
        onOpenChange={setIsEditModalOpen}
        onSubmit={handleUpdateProduct}
        product={selectedProduct}
        mode="edit"
      />
    </div>
  )
}
