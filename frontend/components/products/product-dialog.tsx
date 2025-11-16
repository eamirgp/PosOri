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
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
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
      <DialogContent className="max-w-2xl">
        <DialogHeader>
          <DialogTitle>
            {product ? "Editar Producto" : "Nuevo Producto"}
          </DialogTitle>
          <DialogDescription>
            {product
              ? "Actualiza la información del producto"
              : "Completa los datos para crear un nuevo producto"}
          </DialogDescription>
        </DialogHeader>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <Label htmlFor="code">Código *</Label>
              <Input
                id="code"
                {...register("code")}
                placeholder="P001"
                disabled={loading}
              />
              {errors.code && (
                <p className="text-sm text-destructive">{errors.code.message}</p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="name">Nombre *</Label>
              <Input
                id="name"
                {...register("name")}
                placeholder="Nombre del producto"
                disabled={loading}
              />
              {errors.name && (
                <p className="text-sm text-destructive">{errors.name.message}</p>
              )}
            </div>
          </div>

          <div className="space-y-2">
            <Label htmlFor="description">Descripción</Label>
            <Input
              id="description"
              {...register("description")}
              placeholder="Descripción opcional"
              disabled={loading}
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <Label htmlFor="purchasePrice">Precio de Compra *</Label>
              <Input
                id="purchasePrice"
                type="number"
                step="0.01"
                {...register("purchasePrice", { valueAsNumber: true })}
                placeholder="0.00"
                disabled={loading}
              />
              {errors.purchasePrice && (
                <p className="text-sm text-destructive">
                  {errors.purchasePrice.message}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="salePrice">Precio de Venta *</Label>
              <Input
                id="salePrice"
                type="number"
                step="0.01"
                {...register("salePrice", { valueAsNumber: true })}
                placeholder="0.00"
                disabled={loading}
              />
              {errors.salePrice && (
                <p className="text-sm text-destructive">
                  {errors.salePrice.message}
                </p>
              )}
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <Label htmlFor="categoryId">Categoría *</Label>
              <Select
                value={selectedCategoryId}
                onValueChange={(value) => setValue("categoryId", value)}
                disabled={loading}
              >
                <SelectTrigger>
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
              {errors.categoryId && (
                <p className="text-sm text-destructive">
                  {errors.categoryId.message}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="unitOfMeasureId">Unidad de Medida *</Label>
              <Select
                value={selectedUnitId}
                onValueChange={(value) => setValue("unitOfMeasureId", value)}
                disabled={loading}
              >
                <SelectTrigger>
                  <SelectValue placeholder="Selecciona una unidad" />
                </SelectTrigger>
                <SelectContent>
                  {unitsOfMeasure.map((unit) => (
                    <SelectItem key={unit.id} value={unit.id}>
                      {unit.name} ({unit.code})
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              {errors.unitOfMeasureId && (
                <p className="text-sm text-destructive">
                  {errors.unitOfMeasureId.message}
                </p>
              )}
            </div>
          </div>

          <div className="space-y-2">
            <Label htmlFor="igvTypeId">Tipo de IGV *</Label>
            <Select
              value={selectedIGVId}
              onValueChange={(value) => setValue("igvTypeId", value)}
              disabled={loading}
            >
              <SelectTrigger>
                <SelectValue placeholder="Selecciona un tipo de IGV" />
              </SelectTrigger>
              <SelectContent>
                {igvTypes.map((igvType) => (
                  <SelectItem key={igvType.id} value={igvType.id}>
                    {igvType.name} ({igvType.percentage}%)
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
            {errors.igvTypeId && (
              <p className="text-sm text-destructive">
                {errors.igvTypeId.message}
              </p>
            )}
          </div>

          <DialogFooter>
            <Button
              type="button"
              variant="outline"
              onClick={() => onClose()}
              disabled={loading}
            >
              Cancelar
            </Button>
            <Button type="submit" disabled={loading}>
              {loading ? "Guardando..." : product ? "Actualizar" : "Crear"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
