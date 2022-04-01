using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public void DeleteAutor(int id)
        {
            throw new NotImplementedException();
        }

        public Producto GetProductoByID(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }

        public IEnumerable<Producto> GetProductos()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductos();
        }

        public Producto SaveProducto(Producto prod, string[] selectedCategorias)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.SaveProducto(prod, selectedCategorias);
        }
    }
}
