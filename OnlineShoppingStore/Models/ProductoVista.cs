using OnlineShoppingStore.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'Producto' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    public class ProductoVista:Producto
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'Producto' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    {

        public HttpPostedFileBase fotoFile { get; set; }
    }
}