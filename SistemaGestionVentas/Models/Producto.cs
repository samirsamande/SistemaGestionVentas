using SistemaGestionVentas.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestionVentas.Models;

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, 100000, ErrorMessage = "El precio debe estar entre {1} y {2}.")]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Precio (USD)")]
    public decimal? Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, 10000, ErrorMessage = "El stock debe estar entre {1} y {2}.")]
    [Display(Name = "Stock")]
    public int? Stock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría.")]
    [Display(Name = "Categoría")]
    public int CategoriaId { get; set; }

    [Display(Name = "Categoría")]
    public Categoria? Categoria { get; set; }
}