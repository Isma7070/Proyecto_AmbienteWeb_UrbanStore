using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Utils;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelPedidoDetalle> Items { get; private set; }

        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Carrito Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelPedidoDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(int prodId)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelPedidoDetalle nuevoItem = new ViewModelPedidoDetalle(prodId);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.idProducto == prodId))
                {
                    ViewModelPedidoDetalle item = Items.Find(x => x.idProducto == prodId);
                    item.cantidad++;
                }
                else
                {
                    nuevoItem.cantidad = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Producto agregado al pedido", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "El producto solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }


        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public String SetItemCantidad(int prodId, int Cantidad)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (Cantidad == 0)
            {
                EliminarItem(prodId);
                mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Producto eliminado", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelPedidoDetalle actualizarItem = new ViewModelPedidoDetalle(prodId);
                if (Items.Exists(x => x.idProducto == prodId))
                {
                    ViewModelPedidoDetalle item = Items.Find(x => x.idProducto == prodId);
                    item.cantidad = Cantidad;
                    mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Cantidad actualizada", SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }

        /**
         * EliminarItem (): elimina un artículo del carrito de compras
         */
        public String EliminarItem(int prodId)
        {
            String mensaje = "El producto no existe";
            if (Items.Exists(x => x.idProducto == prodId))
            {
                var itemEliminar = Items.Single(x => x.idProducto == prodId);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Pedido Producto", "Producto eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        /**
         * GetTotal() - Devuelve el precio total de todos los libros.
         */
        public decimal CacularImpuesto()
        {
            decimal subTotal = 0;
            subTotal = Items.Sum(x => x.SubTotal) * (decimal)0.13;

            return subTotal;
        }
        public decimal GetTotal()
        {
            decimal total = 0;
            total = Items.Sum(x => x.SubTotal) + CacularImpuesto();

            return total;
        }
        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.cantidad);

            return total;
        }
        /**
         * GetTotalPeso() - Devuelve el total de peso de todos los libros.
         */

        public void eliminarCarrito()
        {
            Items.Clear();

        }
    }
}