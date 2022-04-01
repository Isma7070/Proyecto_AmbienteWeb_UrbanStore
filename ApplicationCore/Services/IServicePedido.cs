using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePedido
    {
        IEnumerable<Pedido> GetPedidos();
        Pedido GetPedidoByID(int id);
        void DeletePedido(int id);
        Pedido SavePedido(Pedido ped);
        IEnumerable<Pedido> GetEstadoPedido(int estado);
    }
}
