using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProductoByID(int id);
        void DeleteAutor(int id);
        Producto SaveProducto(Producto prod, string[] selectedCategorias);
    }
}
