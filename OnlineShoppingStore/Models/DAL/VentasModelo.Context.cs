﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShoppingStore.Models.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ConexionVentas : DbContext
    {
        public ConexionVentas()
            : base("name=ConexionVentas")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleProducto> DetalleProducto { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<Direccion> Direccion { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<telefono> telefono { get; set; }
        public virtual DbSet<UsuarioAdmin> UsuarioAdmin { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
    }
}
