using Microsoft.EntityFrameworkCore;

public class SistemaGestionVentasContext(DbContextOptions<SistemaGestionVentasContext> options) : DbContext(options)
{
    public DbSet<SistemaGestionVentas.Models.Categoria> Categoria { get; set; } = default!;
    public DbSet<SistemaGestionVentas.Models.Producto> Producto { get; set; } = default!;
}
