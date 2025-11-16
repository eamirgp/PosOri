"use client";

import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { Separator } from "@/components/ui/separator";
import { Skeleton } from "@/components/ui/skeleton";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Loader2 } from "lucide-react";
import { Product, Category, UnitOfMeasure, IGVType } from "@/types/api";
import { productsApi } from "@/lib/api/products";

const productSchema = z.object({
  code: z.string().min(1, "Código es requerido").max(20, "Máximo 20 caracteres"),
  name: z.string().min(1, "Nombre es requerido").max(100, "Máximo 100 caracteres"),
  description: z.string().optional(),
  purchasePrice: z.number().min(0, "Debe ser mayor o igual a 0"),
  salePrice: z.number().min(0, "Debe ser mayor o igual a 0"),
  unitOfMeasureId: z.string().min(1, "Unidad de medida es requerida"),
  igvTypeId: z.string().min(1, "Tipo de IGV es requerido"),
  categoryId: z.string().min(1, "Categoría es requerida"),
});

type ProductFormData = z.infer<typeof productSchema>;

interface ProductDialogProps {
  open: boolean;
  product?: Product;
  onClose: (refresh?: boolean) => void;
}

export function ProductDialog({ open, product, onClose }: ProductDialogProps) {
  const [loading, setLoading] = useState(false);
  const [loadingData, setLoadingData] = useState(false);
  const [categories, setCategories] = useState<Category[]>([]);
  const [unitsOfMeasure, setUnitsOfMeasure] = useState<UnitOfMeasure[]>([]);
  const [igvTypes, setIGVTypes] = useState<IGVType[]>([]);

  const {
    register,
    handleSubmit,
    reset,
    setValue,
    watch,
    formState: { errors },
  } = useForm<ProductFormData>({
    resolver: zodResolver(productSchema),
    defaultValues: {
      code: "",
      name: "",
      description: "",
      purchasePrice: 0,
      salePrice: 0,
      unitOfMeasureId: "",
      igvTypeId: "",
      categoryId: "",
    },
  });

  const selectedCategoryId = watch("categoryId");
  const selectedUnitId = watch("unitOfMeasureId");
  const selectedIGVId = watch("igvTypeId");

  useEffect(() => {
    if (open) {
      loadReferenceData();
      if (product) {
        reset({
          code: product.code,
          name: product.name,
          description: product.description || "",
          purchasePrice: product.purchasePrice,
          salePrice: product.salePrice,
          unitOfMeasureId: product.unitOfMeasureId,
          igvTypeId: product.igvTypeId,
          categoryId: product.categoryId,
        });
      } else {
        reset({
          code: "",
          name: "",
          description: "",
          purchasePrice: 0,
          salePrice: 0,
          unitOfMeasureId: "",
          igvTypeId: "",
          categoryId: "",
        });
      }
    }
  }, [open, product, reset]);

  const loadReferenceData = async () => {
    try {
      setLoadingData(true);
      const [categoriesData, unitsData, igvTypesData] = await Promise.all([
        productsApi.getCategories(),
        productsApi.getUnitsOfMeasure(),
        productsApi.getIGVTypes(),
      ]);
      setCategories(categoriesData);
      setUnitsOfMeasure(unitsData);
      setIGVTypes(igvTypesData);
    } catch (error) {
      console.error("Error loading reference data:", error);
    } finally {
      setLoadingData(false);
    }
  };

  const onSubmit = async (data: ProductFormData) => {
    try {
      setLoading(true);
      if (product) {
        await productsApi.update(product.id, { ...data, id: product.id });
      } else {
        await productsApi.create(data);
      }
      onClose(true);
    } catch (error) {
      console.error("Error saving product:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onOpenChange={() => onClose()}>
      <DialogContent className="max-w-3xl max-h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle className="text-2xl">
            {product ? "Editar Producto" : "Nuevo Producto"}
          </DialogTitle>
          <DialogDescription>
            {product
              ? "Actualiza la información del producto"
              : "Completa los datos para crear un nuevo producto"}
          </DialogDescription>
        </DialogHeader>

        {loadingData ? (
          <div className="space-y-6 py-4">
            <div className="space-y-3">
              <Skeleton className="h-4 w-32" />
              <div className="grid grid-cols-2 gap-4">
                <Skeleton className="h-10 w-full" />
                <Skeleton className="h-10 w-full" />
              </div>
            </div>
            <div className="space-y-3">
              <Skeleton className="h-4 w-24" />
              <Skeleton className="h-20 w-full" />
            </div>
          </div>
        ) : (
          <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
            {/* Información Básica */}
            <div className="space-y-4">
              <div>
                <h3 className="text-sm font-semibold text-foreground">
                  Información Básica
                </h3>
                <p className="text-xs text-muted-foreground">
                  Datos principales del producto
                </p>
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="code" className="text-sm font-medium">
                    Código *
                  </Label>
                  <Input
                    id="code"
                    {...register("code")}
                    placeholder="P001"
                    disabled={loading}
                    className="uppercase"
                  />
                  {errors.code && (
                    <p className="text-xs text-destructive">{errors.code.message}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="name" className="text-sm font-medium">
                    Nombre *
                  </Label>
                  <Input
                    id="name"
                    {...register("name")}
                    placeholder="Nombre del producto"
                    disabled={loading}
                  />
                  {errors.name && (
                    <p className="text-xs text-destructive">{errors.name.message}</p>
                  )}
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="description" className="text-sm font-medium">
                  Descripción
                </Label>
                <Textarea
                  id="description"
                  {...register("description")}
                  placeholder="Descripción detallada del producto (opcional)"
                  disabled={loading}
                  rows={3}
                />
              </div>
            </div>

            <Separator />

            {/* Precios */}
            <div className="space-y-4">
              <div>
                <h3 className="text-sm font-semibold text-foreground">
                  Precios
                </h3>
                <p className="text-xs text-muted-foreground">
                  Define los precios de compra y venta
                </p>
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="purchasePrice" className="text-sm font-medium">
                    Precio de Compra *
                  </Label>
                  <div className="relative">
                    <span className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground">
                      S/
                    </span>
                    <Input
                      id="purchasePrice"
                      type="number"
                      step="0.01"
                      {...register("purchasePrice", { valueAsNumber: true })}
                      placeholder="0.00"
                      disabled={loading}
                      className="pl-10"
                    />
                  </div>
                  {errors.purchasePrice && (
                    <p className="text-xs text-destructive">
                      {errors.purchasePrice.message}
                    </p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="salePrice" className="text-sm font-medium">
                    Precio de Venta *
                  </Label>
                  <div className="relative">
                    <span className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground">
                      S/
                    </span>
                    <Input
                      id="salePrice"
                      type="number"
                      step="0.01"
                      {...register("salePrice", { valueAsNumber: true })}
                      placeholder="0.00"
                      disabled={loading}
                      className="pl-10"
                    />
                  </div>
                  {errors.salePrice && (
                    <p className="text-xs text-destructive">
                      {errors.salePrice.message}
                    </p>
                  )}
                </div>
              </div>
            </div>

            <Separator />

            {/* Clasificación */}
            <div className="space-y-4">
              <div>
                <h3 className="text-sm font-semibold text-foreground">
                  Clasificación y Configuración
                </h3>
                <p className="text-xs text-muted-foreground">
                  Categorización y configuración del producto
                </p>
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="categoryId" className="text-sm font-medium">
                    Categoría *
                  </Label>
                  <Select
                    value={selectedCategoryId}
                    onValueChange={(value) => setValue("categoryId", value)}
                    disabled={loading}
                  >
                    <SelectTrigger>
                      <SelectValue placeholder="Selecciona una categoría" />
                    </SelectTrigger>
                    <SelectContent>
                      {categories.length === 0 ? (
                        <div className="py-6 text-center text-sm text-muted-foreground">
                          No hay categorías disponibles
                        </div>
                      ) : (
                        categories.map((category) => (
                          <SelectItem key={category.id} value={category.id}>
                            {category.name}
                          </SelectItem>
                        ))
                      )}
                    </SelectContent>
                  </Select>
                  {errors.categoryId && (
                    <p className="text-xs text-destructive">
                      {errors.categoryId.message}
                    </p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="unitOfMeasureId" className="text-sm font-medium">
                    Unidad de Medida *
                  </Label>
                  <Select
                    value={selectedUnitId}
                    onValueChange={(value) => setValue("unitOfMeasureId", value)}
                    disabled={loading}
                  >
                    <SelectTrigger>
                      <SelectValue placeholder="Selecciona una unidad" />
                    </SelectTrigger>
                    <SelectContent>
                      {unitsOfMeasure.length === 0 ? (
                        <div className="py-6 text-center text-sm text-muted-foreground">
                          No hay unidades disponibles
                        </div>
                      ) : (
                        unitsOfMeasure.map((unit) => (
                          <SelectItem key={unit.id} value={unit.id}>
                            {unit.name} ({unit.code})
                          </SelectItem>
                        ))
                      )}
                    </SelectContent>
                  </Select>
                  {errors.unitOfMeasureId && (
                    <p className="text-xs text-destructive">
                      {errors.unitOfMeasureId.message}
                    </p>
                  )}
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="igvTypeId" className="text-sm font-medium">
                  Tipo de IGV *
                </Label>
                <Select
                  value={selectedIGVId}
                  onValueChange={(value) => setValue("igvTypeId", value)}
                  disabled={loading}
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecciona un tipo de IGV" />
                  </SelectTrigger>
                  <SelectContent>
                    {igvTypes.length === 0 ? (
                      <div className="py-6 text-center text-sm text-muted-foreground">
                        No hay tipos de IGV disponibles
                      </div>
                    ) : (
                      igvTypes.map((igvType) => (
                        <SelectItem key={igvType.id} value={igvType.id}>
                          {igvType.name} ({igvType.percentage}%)
                        </SelectItem>
                      ))
                    )}
                  </SelectContent>
                </Select>
                {errors.igvTypeId && (
                  <p className="text-xs text-destructive">
                    {errors.igvTypeId.message}
                  </p>
                )}
              </div>
            </div>

            <Separator />

            <DialogFooter className="gap-2">
              <Button
                type="button"
                variant="outline"
                onClick={() => onClose()}
                disabled={loading}
              >
                Cancelar
              </Button>
              <Button type="submit" disabled={loading} className="gap-2">
                {loading && <Loader2 className="h-4 w-4 animate-spin" />}
                {loading ? "Guardando..." : product ? "Actualizar Producto" : "Crear Producto"}
              </Button>
            </DialogFooter>
          </form>
        )}
      </DialogContent>
    </Dialog>
  );
}
