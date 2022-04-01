using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePedido : IServicePedido
    {
        public void DeletePedido(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> GetEstadoPedido(int estado)
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.GetEstadoPedido(estado);
        }

        public Pedido GetPedidoByID(int id)
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.GetPedidoByID(id);
        }

        public IEnumerable<Pedido> GetPedidos()
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.GetPedidos();
        }

        public Pedido SavePedido(Pedido ped)
        {
            IRepositoryPedido repository = new RepositoryPedido();
            return repository.SavePedido(ped);
        }
    }
}
