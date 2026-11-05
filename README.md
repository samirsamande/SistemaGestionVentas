# Sistema de Gestión de Ventas

Aplicación web **ASP.NET Core MVC (.NET 10)** con **Entity Framework Core** y **SQL Server Express** que implementa una **relación uno a muchos** entre dos entidades: **Categoría** (entidad principal) y **Producto** (entidad dependiente). Cada categoría tiene muchos productos y cada producto pertenece a una sola categoría.

**Asignatura:** Programación Integrativa para la Integración de Tecnologías

**Universidad:** Universidad de las Fuerzas Armadas ESPE, Departamento de Ciencias de la Computación

**Actividad:** Relaciones entre entidades, Unidad 1

**Grupo:** 1

## Tema asignado

| Entidad principal | Entidad dependiente | Relación |
|---|---|---|
| Categoría (`Categoria`) | Producto (`Producto`) | Uno a muchos |

## Integrantes y aportes

| Integrante | Aporte |
|---|---|
| David Iván Granada Pachacama | Fase 3 (parte 2): regeneración del CRUD de `Productos` con scaffolding tras agregar la relación |
| John Steven Guanotoa Díaz | Fase 2: DataAnnotations de `Producto` con mensajes en español y su migración |
| Dylan Joel López Morales | Fase 3: relación uno a muchos entre `Categoria` y `Producto` y CRUD completo de ambas entidades |
| Shaffik Samir Samandé Mujica | Creación del repositorio y del proyecto; Fase 1: CRUD de `Producto` y migración `InitialCreate`; sincronización final de migraciones y de la tabla `Categoria` |
| Ronny Adrián Sánchez García | Componente autónomo: corrección del formulario de `Producto` (lista desplegable de Categoría y eliminación del campo duplicado) |

## Tecnologías

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core (Code First con migraciones)
- SQL Server Express (`localhost\\SQLEXPRESS`)
- DataAnnotations para las validaciones
- Bootstrap (plantilla por defecto de MVC)

## Modelo de datos

**Categoria**

| Campo | Tipo | Restricciones |
|---|---|---|
| Id | int | Clave primaria |
| Nombre | nvarchar(100) | Obligatorio, de 3 a 100 caracteres |
| Descripcion | nvarchar(250) | Obligatoria, máximo 250 caracteres |

**Producto**

| Campo | Tipo | Restricciones |
|---|---|---|
| Id | int | Clave primaria |
| Nombre | nvarchar(150) | Obligatorio, de 3 a 150 caracteres |
| Precio | decimal(10,2) | Obligatorio, entre 0.01 y 100000 |
| Stock | int | Obligatorio, entre 0 y 10000 |
| CategoriaId | int | Clave foránea hacia `Categoria` (obligatoria, se elige en una lista desplegable) |

Todos los mensajes de validación están en español.

La clave foránea `FK_Producto_Categoria_CategoriaId` elimina en cascada: al borrar una categoría se borran también sus productos.

## Migraciones

| Migración | Descripción |
|---|---|
| `InitialCreate` | Crea la tabla `Producto` con su CRUD inicial |
| `ValidacionesProducto` | Aplica las validaciones de `Producto` |
| `AjustePrecioDecimal` | Ajusta `Precio` a `decimal(10,2)` |
| `RelacionCategoriaProducto` | Crea la tabla `Categoria`, agrega `CategoriaId` a `Producto` y la clave foránea |
| `AgregarCategoria` | Sincroniza el modelo con la base de datos (sin cambios de esquema) |
| `AjusteFinalModelo` | Deja `Descripcion` de `Categoria` como obligatoria |

## Componente autónomo

Al regenerar el CRUD de `Producto`, el formulario presentaba la clave foránea como un campo numérico y un segundo campo generado desde la propiedad de navegación. Se corrigió así:

- Los formularios de crear y editar usan una **lista desplegable** que muestra el nombre de la categoría (`SelectList` cargado en el controlador y `<select asp-for="CategoriaId">` en la vista).
- Se eliminó el **campo duplicado** de `Categoria`; la propiedad de navegación es nulable (`Categoria?`) y se excluye del `Bind`.
- `Index` y `Details` muestran el **nombre** de la categoría, no su número, con `Include(p => p.Categoria)` (carga ansiosa).
- Si el formulario se envía con errores de validación, la lista desplegable **conserva sus opciones**.

Archivos modificados: `Controllers/ProductosController.cs`, `Models/Producto.cs`, `Views/Productos/Create.cshtml`, `Edit.cshtml`, `Index.cshtml` y `Details.cshtml`.

## Requisitos

- Visual Studio con la carga de trabajo *ASP.NET y desarrollo web*
- SDK de .NET 10
- SQL Server Express con la instancia `localhost\\SQLEXPRESS`
- SQL Server Management Studio (SSMS), opcional, para consultar la base de datos

## Pasos para ejecutar el proyecto

1. Clonar el repositorio:
   ```bash
   git clone <URL-del-repositorio>
   ```
2. Abrir `SistemaGestionVentas.slnx` en Visual Studio.
3. Verificar en `appsettings.json` que la cadena de conexión `SistemaGestionVentasContext` apunte a `localhost\\SQLEXPRESS`.
4. Abrir *Herramientas > Administrador de paquetes NuGet > Consola del administrador de paquetes* y ejecutar:
   ```powershell
   Update-Database
   ```
   Esto crea la base de datos y aplica las seis migraciones.
5. Ejecutar el proyecto con **F5**.
6. Crear primero algunas categorías en **Categorías** y luego los productos en **Productos**. Los decimales se escriben con punto (por ejemplo, `1.50`).

## Video de demostración

[Ver video de demostración](https://youtu.be/8AkVIx4clXQ)

Muestra el CRUD de categorías, la creación de un producto eligiendo la categoría en la lista desplegable, un error de validación en español, la lista de productos con el nombre de la categoría y los datos relacionados en SSMS.
