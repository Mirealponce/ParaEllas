using OnlineShoppingStore.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models
{
    public class Carrito
    {
        
#pragma warning disable CS0169 // El campo 'Carrito.cantidad' nunca se usa
        private int cantidad;
#pragma warning restore CS0169 // El campo 'Carrito.cantidad' nunca se usa
#pragma warning disable CS0169 // El campo 'Carrito.productos' nunca se usa
        private Producto productos ;
#pragma warning restore CS0169 // El campo 'Carrito.productos' nunca se usa
        public Carrito()
        {

        }
       
        public int Cantidad { get; set; }
        public Producto Productos { get; set; }

    }
}