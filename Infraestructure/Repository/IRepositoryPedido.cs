using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryPedido
    {
        IEnumerable<Pedido> GetPedidos();
        IEnumerable<Pedido> GetEstadoPedido(int estado);
        Pedido GetPedidoByID(int id);
        void DeletePedido(int id);
        Pedido SavePedido(Pedido ped);
    }
}
