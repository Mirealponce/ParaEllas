using Newtonsoft.Json;

using OnlineShoppingStore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //public UnidadGenericaDeTrabajo _UnidadDeTrabajo = new UnidadGenericaDeTrabajo();
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Categories()
        {
            //List<Categoria> allcategories = _UnidadDeTrabajo.GetRepositoryInstance<Categoria>().GetAllRecords().ToList();
            return View(/*allcategories*/);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory( int idCategoria)
        {
          /*  CategoryDetail cd;
#pragma warning disable CS0472 // El resultado de la expresión siempre es 'true' porque un valor del tipo 'int' nunca es igual a 'NULL' de tipo 'int?'
                if (idCategoria != null)
#pragma warning restore CS0472 // El resultado de la expresión siempre es 'true' porque un valor del tipo 'int' nunca es igual a 'NULL' de tipo 'int?'
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_UnidadDeTrabajo.GetRepositoryInstance<Categoria>().GetFirstorDefault(idCategoria)));
            }
            else
            {
                cd = new CategoryDetail();
            }
          */
            return View(/*"UpdateCategory",cd*/);
        }
        
        public ActionResult Producto()
        {
            return View();
        }
            
        

    }
}