using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.DAL;

namespace OnlineShoppingStore.Controllers
{
    public class ClientesController : Controller
    {
        private ConexionVentas db = new ConexionVentas();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Cliente.ToList());
        }
        /// <summary>
        /// Método que a través del id muestra el perfil del usuario o sus datos datos personales
        /// </summary>
        /// <param name="id">id del usuario Cliente en la base de datos</param>
        /// <returns></returns>
        public ActionResult Perfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }
        public ActionResult IniciarSesion()
        {
            return View();
        }

        public ActionResult ModalRegistro()
        {
            return View();
        }

        public ActionResult PanelCliente(int? id)
        {
            ViewBag.idCliente = id;
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(string usuario, string pass)
        {
            var resultclient= db.Cliente.Where(c => c.correo.Equals(usuario)&& c.pass.Equals(pass)).FirstOrDefault();
            var resultadmin = db.UsuarioAdmin.Where(a => a.nombreAdmin.Equals(usuario)&& a.clave.Equals(pass)).FirstOrDefault();

            if (resultclient==null&&resultadmin==null)
            {
                return View();
            }
            else if (resultclient != null)
            {

                //var cliente = this.ToCliente(resultclient.);
               // var cliente = this.Details(resultclient.idCliente);
                // this.Perfil(cliente);
                return Redirect("/Clientes/PanelCliente/"+resultclient.idCliente);

            }
            else if (resultadmin != null)
            {
                return Redirect("/Productos/Index");
            }

             return View("IniciarSesion");

        }

        

        // GET: Clientes/Details/5
       private Cliente ToCliente(Cliente cliente)
        {
            return new Cliente
            {

                idCliente = cliente.idCliente,
               
                nombre = cliente.nombre,
                apellido = cliente.apellido,
                correo = cliente.correo,
                pass= cliente.pass
                
               
            };
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "idCliente,nombre,apellido,correo,pass")] Cliente cliente)

        {
            if (ModelState.IsValid)
            {
                var result = db.Cliente.Where(c=>c.correo.Equals(cliente.correo)).FirstOrDefault();
                if (result==null)
                {
                    db.Cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("../Clientes/IniciarSesion");
                }else if (result!=null)
                {
                    return RedirectToAction("../Clientes/ModalRegistro");
                }
               
            }

            return View(cliente);
        }

        

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Cliente' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        public ActionResult Edit([Bind(Include = "idCliente,nombre,apellido,correo,pass")] Cliente cliente)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Cliente' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
