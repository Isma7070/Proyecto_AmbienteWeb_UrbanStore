using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryPedido : IRepositoryPedido
    {
        public void DeletePedido(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> GetEstadoPedido(int estado)
        {
            IEnumerable<Pedido> listaPedido = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    listaPedido = ctx.Pedido.
                               Where(p => p.estado == estado).ToList<Pedido>();

                }
                return listaPedido;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public Pedido GetPedidoByID(int id)
        {
            Pedido pedido = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pedido = ctx.Pedido.
                               Include("PersonalEntrega").
                               Include("DetallePedido").
                               Include("DetallePedido.Producto").
                               Where(p => p.idPedido == id).
                               FirstOrDefault<Pedido>();

                }
                return pedido;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public IEnumerable<Pedido> GetPedidos()
        {
            List<Pedido> pedidos = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pedidos = ctx.Pedido.
                               Include("PersonalEntrega").
                                Include("Usuario").
                               ToList<Pedido>();

                }
                return pedidos;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public Pedido SavePedido(Pedido ped)
        {
            int resultado = 0;
            Pedido pedido = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Pedido
                // 2- DetallaPedido 
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.Pedido.Add(ped);
                        resultado = ctx.SaveChanges();
                        foreach (var detalle in ped.DetallePedido)
                        {
                            detalle.idPedido = ped.idPedido;
                        }
                        foreach (var item in ped.DetallePedido)
                        {
                            // Busco el producto que está en el detalle por IdLibro
                            Producto oProducto = ctx.Producto.Find(item.idProducto);

                            // Se indica que se alteró
                            ctx.Entry(oProducto).State = EntityState.Modified;
                            // Guardar                         
                            resultado = ctx.SaveChanges();
                        }

                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    pedido = GetPedidoByID(ped.idPedido);


                return pedido;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }
    }
}
