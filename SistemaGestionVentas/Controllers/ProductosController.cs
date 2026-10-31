using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionVentas.Models;

public class ProductosController : Controller
{
    private readonly SistemaGestionVentasContext _context;

    public ProductosController(SistemaGestionVentasContext context)
    {
        _context = context;
    }

    // GET: PRODUCTOS
    public async Task<IActionResult> Index()    
    {
        var sistemaContext = _context.Producto.Include(p => p.Categoria);
        return View(await sistemaContext.ToListAsync());
    }

    // GET: PRODUCTOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _context.Producto
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // GET: PRODUCTOS/Create
    public IActionResult Create()
    {
        CargarCategorias();
        return View();
    }

    // POST: PRODUCTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Stock,CategoriaId")] Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        CargarCategorias(producto.CategoriaId);
        return View(producto);
    }

    // GET: PRODUCTOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _context.Producto.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        CargarCategorias(producto.CategoriaId);
        return View(producto);
    }

    // POST: PRODUCTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Precio,Stock,CategoriaId")] Producto producto)
    {
        if (id != producto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(producto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        CargarCategorias(producto.CategoriaId);
        return View(producto);
    }

    // GET: PRODUCTOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var producto = await _context.Producto
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (producto == null)
        {
            return NotFound();
        }

        return View(producto);
    }

    // POST: PRODUCTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var producto = await _context.Producto.FindAsync(id);
        if (producto != null)
        {
            _context.Producto.Remove(producto);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductoExists(int? id)
    {
        return _context.Producto.Any(e => e.Id == id);
    }

    private void CargarCategorias(object? seleccionada = null)
    {
        ViewData["CategoriaId"] = new SelectList(
            _context.Categoria.OrderBy(c => c.Nombre), "Id", "Nombre", seleccionada);
    }
}

