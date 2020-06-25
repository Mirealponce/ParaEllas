using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using OnlineShoppingStore.Models.DAL;

namespace OnlineShoppingStore.Controllers
{
    public class DireccionController : Controller
    {
        private ConexionVentas db = new ConexionVentas();

        // GET: Direccion
        public ActionResult Index()
        {
            var direccion = db.Direccion.Include(d => d.Cliente);
            return View(direccion.ToList());
        }

        // GET: Direccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        public ActionResult MisDireccions(int? id)
        {
            var filtro = db.Direccion.Where(d => d.Cliente_idCliente == id);
            return View(filtro);
        }

        // GET: Direccion/Create
        public ActionResult Create(int id)
        {
            ViewBag.Cliente_idCliente = id;
            return View();
        }

        // POST: Direccion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Direccion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        public ActionResult Create([Bind(Include = "idDireccion,region,comuna,calle,numero,Cliente_idCliente")] Direccion direccion)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Direccion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        {
            if (ModelState.IsValid)
            {
                db.Direccion.Add(direccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cliente_idCliente = direccion.Cliente_idCliente;
            return View(direccion);
        }

        // GET: Direccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_idCliente = new SelectList(db.Cliente, "idCliente", "nombre", direccion.Cliente_idCliente);
            return View(direccion);
        }

        // POST: Direccion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Direccion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        public ActionResult Edit([Bind(Include = "idDireccion,region,comuna,calle,numero,Cliente_idCliente")] Direccion direccion)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Direccion' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cliente_idCliente = new SelectList(db.Cliente, "idCliente", "nombre", direccion.Cliente_idCliente);
            return View(direccion);
        }

        // GET: Direccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccion direccion = db.Direccion.Find(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return View(direccion);
        }

        // POST: Direccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direccion direccion = db.Direccion.Find(id);
            db.Direccion.Remove(direccion);
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
