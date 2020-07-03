using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.DAL;

namespace OnlineShoppingStore.Controllers
{
    public class telefonosController : Controller
    {
        private ConexionVentas db = new ConexionVentas();

        // GET: telefonos
        public ActionResult Index()
        {
            var telefono = db.telefono.Include(t => t.Cliente);
            return View(telefono.ToList());
        }

        // GET: telefonos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            telefono telefono = db.telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // GET: telefonos/Create
        int idClientfk;
        public ActionResult Create( int id)
        {
            idClientfk = id;
            ViewBag.idCliente = id;
            Session["idCliente"] = id;
            // ViewBag.Cliente_idCliente = new SelectList(db.Cliente, "idCliente", "nombre");
            return View();
        }

        // POST: telefonos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        public ActionResult Create([Bind(Include = "idtelefono,Ntelefono,Cliente_idCliente")] telefono telefono )
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        {
            if (ModelState.IsValid)
            {
               //var tel = this.ToTelefono(telefono,idClientfk);
                db.telefono.Add(telefono);
                db.SaveChanges();
                return RedirectToAction("../Clientes/PanelCliente/" + Session["idCliente"]);
            }

            //ViewBag.Cliente_idCliente = new SelectList(db.Cliente, "idCliente", "nombre", telefono.Cliente_idCliente);
            ViewBag.idCliente = telefono.Cliente_idCliente;
            return View(telefono);
        }

#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        private telefono ToTelefono(telefono tel, int idclient)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        {
            return new telefono
            {

                idtelefono = tel.idtelefono,
                Ntelefono = tel.Ntelefono,
                Cliente_idCliente = idclient,
                
              
            };
        }
        public ActionResult TelefonosContacto(int? id)
        {
            var filtro = db.telefono.Where(tipo => tipo.Cliente_idCliente==id);
            return View(filtro);
        }

        // GET: telefonos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            telefono telefono = db.telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cliente_idCliente = telefono.Cliente_idCliente;

            Session["idCliente"] = telefono.Cliente_idCliente;
            return View(telefono);
        }

        // POST: telefonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        public ActionResult Edit([Bind(Include = "idtelefono,Ntelefono,Cliente_idCliente")] telefono telefono)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'telefono' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefono).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Clientes/PanelCliente/" + Session["idCliente"]);
            }
            ViewBag.Cliente_idCliente = telefono.Cliente_idCliente;
            return View(telefono);
        }

        // GET: telefonos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            telefono telefono = db.telefono.Find(id);
            if (telefono == null)
            {
                return HttpNotFound();
            }
            return View(telefono);
        }

        // POST: telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            telefono telefono = db.telefono.Find(id);
            db.telefono.Remove(telefono);
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
