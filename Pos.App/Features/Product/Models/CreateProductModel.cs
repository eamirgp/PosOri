using System.ComponentModel.DataAnnotations;

namespace Pos.App.Features.Product.Models
{
    public class CreateProductModel
    {
        [Required(ErrorMessage = "La unidad de medida es requerida.")]
        public Guid UnitOfMeasureId { get; set; }

        [Required(ErrorMessage = "El tipo de IGV es requerido.")]
        public Guid IGVTypeId { get; set; }

        [Required(ErrorMessage = "La categoría es requerida.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "El código es requerido.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder los 20 caracteres.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El precio de compra es requerido.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor o igual a 0.")]
        public decimal PurchasePrice { get; set; }

        [Required(ErrorMessage = "El precio de venta es requerido.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor o igual a 0.")]
        public decimal SalePrice { get; set; }
    }
}