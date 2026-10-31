using SistemaGestionVentas.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionVentas.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(250, ErrorMessage = "La descripción no puede superar los {1} caracteres.")]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; } 

    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}