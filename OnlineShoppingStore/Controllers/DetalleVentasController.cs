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
    public class DetalleVentasController : Controller
    {
        private ConexionVentas db = new ConexionVentas();

        // GET: DetalleVentas
        public ActionResult Index()
        {
            var detalleVenta = db.DetalleVenta.Include(d => d.DetalleProducto).Include(d => d.Venta);
            return View(detalleVenta.ToList());
        }

        // GET: DetalleVentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return HttpNotFound();
            }
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Create
        public ActionResult Create()
        {
            ViewBag.DetalleProducto_idDetalleProducto = new SelectList(db.DetalleProducto, "idDetalleProducto", "DatosCombo");
            ViewBag.Venta_idVenta = new SelectList(db.Venta, "idVenta", "DatosComboVenta");
            return View();
        }

        // POST: DetalleVentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalleVenta,cantidadProducto,totalVentaProducto,Venta_idVenta,DetalleProducto_idDetalleProducto")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                db.DetalleVenta.Add(detalleVenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DetalleProducto_idDetalleProducto = new SelectList(db.DetalleProducto, "idDetalleProducto", "DatosCombo", detalleVenta.DetalleProducto_idDetalleProducto);
            ViewBag.Venta_idVenta = new SelectList(db.Venta, "idVenta", "DatosComboVenta", detalleVenta.Venta_idVenta);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.DetalleProducto_idDetalleProducto = new SelectList(db.DetalleProducto, "idDetalleProducto", "DatosCombo", detalleVenta.DetalleProducto_idDetalleProducto);
            ViewBag.Venta_idVenta = new SelectList(db.Venta, "idVenta", "DatosComboVenta", detalleVenta.Venta_idVenta);
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalleVenta,cantidadProducto,totalVentaProducto,Venta_idVenta,DetalleProducto_idDetalleProducto")] DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleVenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DetalleProducto_idDetalleProducto = new SelectList(db.DetalleProducto, "idDetalleProducto", "DatosCombo", detalleVenta.DetalleProducto_idDetalleProducto);
            ViewBag.Venta_idVenta = new SelectList(db.Venta, "idVenta", "DatosComboVenta", detalleVenta.Venta_idVenta);
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            if (detalleVenta == null)
            {
                return HttpNotFound();
            }
            return View(detalleVenta);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleVenta detalleVenta = db.DetalleVenta.Find(id);
            db.DetalleVenta.Remove(detalleVenta);
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
