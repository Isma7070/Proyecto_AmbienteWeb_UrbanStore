using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
using Web.ViewModel;

namespace Web.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Vendedor)]
        public ActionResult Index()
        {
            IEnumerable<Pedido> lista = null;

            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                lista = _ServicePedido.GetPedidos();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult PedidoCarrito()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            //ViewBag.idCliente = listaClientes();

            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            return View();
        }
        //Actualizar cantidad de libros en el carrito
        public ActionResult actualizarCantidad(int idProducto, int cantidad)
        {
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idProducto, cantidad);
            TempData.Keep();
            return PartialView("_DetallePedido", Carrito.Instancia.Items);

        }
        //Actualizar solo la cantidad de productos que se muestra en el menú
        public ActionResult actualizarPedidoCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("_PedidoCantidad");

        }
        //Eliminar producto del pedido
        public ActionResult eliminarProducto(int? idProducto)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem((int)idProducto);
            return PartialView("_DetallePedido", Carrito.Instancia.Items);
        }
        public ActionResult ordenarProducto(int? idProducto)
        {
            int cantidad = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idProducto);
            return PartialView("_PedidoCantidad");

        }
        public ActionResult AjaxDetails(int estado)
        {

            IServicePedido _ServicePedido = new ServicePedido();
            IEnumerable<Pedido> listaPedido = _ServicePedido.GetEstadoPedido(estado);
            return PartialView("_PartialViewPedidoFitro", listaPedido);
        }
        public PartialViewResult FiltroEstado(int? id)
        {
            IEnumerable<Pedido> lista = null;
            IServicePedido _ServicePedido = new ServicePedido();
            if (id != null)
            {
                lista = _ServicePedido.GetEstadoPedido((int)id);
            }
            return PartialView("_PartialViewPedidoFitro", lista);
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {
            ServicePedido _ServicePedido = new ServicePedido();
            Pedido pedido = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                pedido = _ServicePedido.GetPedidoByID(id.Value);
                if (pedido == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(pedido);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Pedido/Save
        public ActionResult Save(Pedido pedido)
        {
            try
            {

                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Pedido", "Seleccione los productos a pedir", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {

                    var listaDetalle = Carrito.Instancia.Items;

                    foreach (var item in listaDetalle)
                    {
                        DetallePedido pedidoDetalle = new DetallePedido();
                        pedidoDetalle.idProducto = item.idProducto;
                        pedidoDetalle.Precio = item.Precio;
                        pedidoDetalle.cantidad = item.cantidad;
                        pedido.DetallePedido.Add(pedidoDetalle);
                    }
                }

                IServicePedido _ServicePedido = new ServicePedido();
                Pedido pedidoSave = _ServicePedido.SavePedido(pedido);

                // Limpia el Carrito de compras
                Carrito.Instancia.eliminarCarrito();
                TempData["NotificationMessage"] = Utils.SweetAlertHelper.Mensaje("Pedido", "Pedido guardado satisfactoriamente!", SweetAlertMessageType.success);
                // Reporte orden
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Pedido";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Pedido/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedido/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pedido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
