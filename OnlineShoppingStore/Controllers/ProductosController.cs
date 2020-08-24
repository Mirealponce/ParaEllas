using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Helpers;
using System.Data.Entity.Validation;
using OnlineShoppingStore.Models.DAL;

namespace OnlineShoppingStore.Controllers
{
    public class ProductosController : Controller
    {
        private ConexionVentas db = new ConexionVentas();

        // GET: Productos
        public ActionResult Index()
        {
            if (Session["sesion"] == null)
            {
                return Redirect("~/Clientes/IniciarSesion?mensaje=1");
            }
            var producto = db.Producto.Include(p => p.Categoria);
            

            return View(producto.ToList());
        }
        //ORDENAR PRECIO
        public async Task<ActionResult> OrdenarPorCategoria()
        {
            var productos = from p in db.Producto
                            orderby p.precio ascending
                            select p;
            return View(productos);
       
        }
        
        
        public async Task<ActionResult> filtrar(string nombre)
        {
            //FILTRAR POR CATEGORÍA
            var filtro = db.Producto.Where(tipo => tipo.Categoria.tipoCategoria.Equals(nombre));
           
            return View(filtro);
        }

     
        public async Task<ActionResult> AddToCart(int? id, string cant)
        {
            if (Session["cart"] == null)
            {
                List<Carrito> cart = new List<Carrito>();
                var product = db.Producto.Find(id);
                cart.Add(new Carrito()
                {
                    Productos = product,
                    Cantidad = int.Parse(cant)
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Carrito> cart = (List<Carrito>)Session["cart"];
                var product = db.Producto.Find(id);
                cart.Add(new Carrito()
                {
                    Productos = product,
                    Cantidad = int.Parse(cant)
                });
                Session["cart"] = cart;
            }
            return Redirect("../../Productos/IndexClient");
        }

        public async Task<ActionResult> DetalleCarrito()
        {

            return View();
        }

        //Lista productos para usuario
        public async Task<ActionResult> IndexClient()
        {
            var producto = db.Producto.Include(p => p.Categoria);


            return View(producto.ToList());
        }

        // GET: Productos/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.Categoria_idCategoria = new SelectList(db.Categoria, "idCategoria", "tipoCategoria");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductoVista vistaProducto)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/ImagenesProductos";

                if (vistaProducto.fotoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(vistaProducto.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }
                //almaceno los datos en la variable producto
                var producto = this.ToProducto(vistaProducto, pic);
                //agrego los datos almacenados en la variable producto a la base de datos
                db.Producto.Add(producto);
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

            }

            ViewBag.Categoria_idCategoria = new SelectList(db.Categoria, "idCategoria", "tipoCategoria", vistaProducto.Categoria_idCategoria);
            return View(vistaProducto);
        }

        private Producto ToProducto(ProductoVista vistaProducto, string pic)
        {
            return new Producto
            {

                idProducto = vistaProducto.idProducto,
                Foto = pic,
                nombreProducto = vistaProducto.nombreProducto,
                precio = vistaProducto.precio,
                Descripcion = vistaProducto.Descripcion,
                
                Categoria_idCategoria = vistaProducto.Categoria_idCategoria
            };
        }

        private ProductoVista ToView(Producto producto)
        {
            return new ProductoVista
            {

                idProducto = producto.idProducto,
                Foto = producto.Foto,
                nombreProducto = producto.nombreProducto,
                precio = producto.precio,
                Descripcion = producto.Descripcion,

                Categoria_idCategoria = producto.Categoria_idCategoria
            };
        }
        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Producto producto = db.Producto.Find(id);
           
            if (producto == null)
            {
                return HttpNotFound();
          
            }

            ViewBag.Categoria_idCategoria = new SelectList(db.Categoria, "idCategoria", "tipoCategoria", producto.Categoria_idCategoria);
            var view = this.ToView(producto);
            return View(view);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( ProductoVista producto)
        {
            if (ModelState.IsValid)
            {

                var pic = producto.Foto;
                var folder = "~/Content/ImagenesProductos";

                if (producto.fotoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(producto.fotoFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var productos = this.ToProducto(producto,pic);

                db.Entry(productos).State = EntityState.Modified;
               await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Categoria_idCategoria = new SelectList(db.Categoria, "idCategoria", "tipoCategoria", producto.Categoria_idCategoria);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
