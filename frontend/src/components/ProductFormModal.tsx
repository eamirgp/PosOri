import { useEffect, useState } from "react"
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select"
import type { Product, CreateProductDto, UpdateProductDto, Category, UnitOfMeasure, IGVType } from "@/types/product"
import { catalogService } from "@/services/productService"

interface ProductFormModalProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onSubmit: (product: CreateProductDto | UpdateProductDto) => Promise<void>
  product?: Product | null
  mode: "create" | "edit"
}

export function ProductFormModal({
  open,
  onOpenChange,
  onSubmit,
  product,
  mode,
}: ProductFormModalProps) {
  const [formData, setFormData] = useState<CreateProductDto>({
    code: "",
    name: "",
    description: "",
    categoryId: "",
    unitOfMeasureId: "",
    igvTypeId: "",
    purchasePrice: 0,
    salePrice: 0,
  })

  const [categories, setCategories] = useState<Category[]>([])
  const [unitOfMeasures, setUnitOfMeasures] = useState<UnitOfMeasure[]>([])
  const [igvTypes, setIGVTypes] = useState<IGVType[]>([])
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    if (open) {
      loadCatalogs()
      if (mode === "edit" && product) {
        setFormData({
          code: product.code,
          name: product.name,
          description: product.description,
          categoryId: product.categoryId,
          unitOfMeasureId: product.unitOfMeasureId,
          igvTypeId: product.igvTypeId,
          purchasePrice: product.purchasePrice,
          salePrice: product.salePrice,
        })
      } else {
        setFormData({
          code: "",
          name: "",
          description: "",
          categoryId: "",
          unitOfMeasureId: "",
          igvTypeId: "",
          purchasePrice: 0,
          salePrice: 0,
        })
      }
    }
  }, [open, mode, product])

  const loadCatalogs = async () => {
    try {
      const [categoriesRes, unitOfMeasuresRes, igvTypesRes] = await Promise.all([
        catalogService.getCategories(),
        catalogService.getUnitOfMeasures(),
        catalogService.getIGVTypes(),
      ])

      if (categoriesRes.isSuccess && categoriesRes.value) {
        setCategories(categoriesRes.value)
      }
      if (unitOfMeasuresRes.isSuccess && unitOfMeasuresRes.value) {
        setUnitOfMeasures(unitOfMeasuresRes.value)
      }
      if (igvTypesRes.isSuccess && igvTypesRes.value) {
        setIGVTypes(igvTypesRes.value)
      }
    } catch (error) {
      console.error("Error loading catalogs:", error)
    }
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setLoading(true)

    try {
      if (mode === "edit" && product) {
        await onSubmit({ ...formData, id: product.id } as UpdateProductDto)
      } else {
        await onSubmit(formData)
      }
      onOpenChange(false)
    } catch (error) {
      console.error("Error submitting form:", error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-[525px]">
        <form onSubmit={handleSubmit}>
          <DialogHeader>
            <DialogTitle>
              {mode === "create" ? "Crear Producto" : "Editar Producto"}
            </DialogTitle>
            <DialogDescription>
              {mode === "create"
                ? "Ingresa los datos del nuevo producto"
                : "Modifica los datos del producto"}
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="code" className="text-right">
                Código
              </Label>
              <Input
                id="code"
                value={formData.code}
                onChange={(e) =>
                  setFormData({ ...formData, code: e.target.value })
                }
                className="col-span-3"
                required
              />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="name" className="text-right">
                Nombre
              </Label>
              <Input
                id="name"
                value={formData.name}
                onChange={(e) =>
                  setFormData({ ...formData, name: e.target.value })
                }
                className="col-span-3"
                required
              />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="description" className="text-right">
                Descripción
              </Label>
              <Input
                id="description"
                value={formData.description}
                onChange={(e) =>
                  setFormData({ ...formData, description: e.target.value })
                }
                className="col-span-3"
                required
              />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="category" className="text-right">
                Categoría
              </Label>
              <Select
                value={formData.categoryId}
                onValueChange={(value) =>
                  setFormData({ ...formData, categoryId: value })
                }
                required
              >
                <SelectTrigger className="col-span-3">
                  <SelectValue placeholder="Selecciona una categoría" />
                </SelectTrigger>
                <SelectContent>
                  {categories.map((category) => (
                    <SelectItem key={category.id} value={category.id}>
                      {category.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="unitOfMeasure" className="text-right">
                Unidad
              </Label>
              <Select
                value={formData.unitOfMeasureId}
                onValueChange={(value) =>
                  setFormData({ ...formData, unitOfMeasureId: value })
                }
                required
              >
                <SelectTrigger className="col-span-3">
                  <SelectValue placeholder="Selecciona una unidad" />
                </SelectTrigger>
                <SelectContent>
                  {unitOfMeasures.map((unit) => (
                    <SelectItem key={unit.id} value={unit.id}>
                      {unit.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="igvType" className="text-right">
                Tipo IGV
              </Label>
              <Select
                value={formData.igvTypeId}
                onValueChange={(value) =>
                  setFormData({ ...formData, igvTypeId: value })
                }
                required
              >
                <SelectTrigger className="col-span-3">
                  <SelectValue placeholder="Selecciona un tipo de IGV" />
                </SelectTrigger>
                <SelectContent>
                  {igvTypes.map((igv) => (
                    <SelectItem key={igv.id} value={igv.id}>
                      {igv.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="purchasePrice" className="text-right">
                Precio Compra
              </Label>
              <Input
                id="purchasePrice"
                type="number"
                step="0.01"
                value={formData.purchasePrice}
                onChange={(e) =>
                  setFormData({
                    ...formData,
                    purchasePrice: parseFloat(e.target.value),
                  })
                }
                className="col-span-3"
                required
              />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="salePrice" className="text-right">
                Precio Venta
              </Label>
              <Input
                id="salePrice"
                type="number"
                step="0.01"
                value={formData.salePrice}
                onChange={(e) =>
                  setFormData({
                    ...formData,
                    salePrice: parseFloat(e.target.value),
                  })
                }
                className="col-span-3"
                required
              />
            </div>
          </div>
          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onOpenChange(false)}
            >
              Cancelar
            </Button>
            <Button type="submit" disabled={loading}>
              {loading ? "Guardando..." : mode === "create" ? "Crear" : "Actualizar"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}
