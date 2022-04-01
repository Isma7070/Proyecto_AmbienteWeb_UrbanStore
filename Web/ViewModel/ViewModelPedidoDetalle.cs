using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelPedidoDetalle
    {
        public int idPedido { get; set; }
        public int idProducto { get; set; }
        public int idDetalle { get; set; }
        public int cantidad { get; set; }
        public decimal Precio 
        {
            get { return Producto.Precio; } 
        }
        public virtual Producto Producto { get; set; }
        public decimal SubTotal
        {
            get
            {
                return calculoSubtotal();
            }
        }
        private decimal calculoSubtotal()
        {
            return this.Precio * this.cantidad;
        }
        public ViewModelPedidoDetalle(int IdProducto)
        {
            IServiceProducto _ServiceLibro = new ServiceProducto();
            this.idProducto = IdProducto;
            this.Producto = _ServiceLibro.GetProductoByID(IdProducto);
        }
    }
}