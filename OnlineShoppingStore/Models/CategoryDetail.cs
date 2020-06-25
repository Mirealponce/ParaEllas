using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Models
{
    public class CategoryDetail
    {
        public int idCategoria { get; set; }
        [Required(ErrorMessage = "Category Name Requird")]
        [StringLength(100, ErrorMessage = "Minimun 3 and minimun 5 and maximun 100 characters are allwed", MinimumLength = 3)]
        public string tipoCategoria { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public class ProductoDetail
        {
            public int idProducto { get; set; }
            [Required(ErrorMessage = "Producto Name is Requerid")]
            [StringLength(100, ErrorMessage = "Minimun 3 and minimun 5 and maximun 100 characters are allwed", MinimumLength = 3)]
            public string nombreProducto { get; set; }
            [Required]
            [Range (1,50)]
            public Nullable<double> precio { get; set; }
            [Required]
            [Range(typeof(decimal), "1", "200000", ErrorMessage = "Precio Invalido")]
            public Nullable<int> cantidadStock { get; set; }
            public int Categoria_idCategoria { get; set; }
            public SelectList Categorias { get; set; }
        }
    }
}