using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoByID(int id);
        void DeleteAutor(int id);
        Producto SaveProducto(Producto prod, string[] selectedCategorias);
    }
}
