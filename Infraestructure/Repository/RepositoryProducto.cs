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
    public class RepositoryProducto : IRepositoryProducto
    {
        public void DeleteAutor(int id)
        {
            throw new NotImplementedException();
        }

        public Producto GetProductoByID(int id)
        {
            Producto oProducto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = ctx.Producto.
                    Where(p => p.ProductoID == id).
                    Include(x => x.Categoria).
                    FirstOrDefault();
            }
            return oProducto;
        }

        public IEnumerable<Producto> GetProductos()
        {
            try
            {
                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Producto.ToList<Producto>();
                }
                return lista;
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
                throw;
            }
        }

        public Producto SaveProducto(Producto prod, string[] selectedCategorias)
        {
            int retorno = 0;
            Producto oProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = GetProductoByID((int)prod.ProductoID);
                IRepositoryCategoria _RepositoryCategoria = new RepositoryCategoria();

                if (oProducto == null)
                {

                    //Insertar
                    if (selectedCategorias != null)
                    {

                        prod.Categoria = new List<Categoria>();
                        foreach (var categoria in selectedCategorias)
                        {
                            var categoriaToAdd = _RepositoryCategoria.GetCategoriaByID(int.Parse(categoria));
                            ctx.Categoria.Attach(categoriaToAdd); //sin esto, EF intentará crear una categoría
                            prod.Categoria.Add(categoriaToAdd);// asociar a la categoría existente con el libro


                        }
                    }
                    ctx.Producto.Add(prod);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Producto
                    ctx.Producto.Add(prod);
                    ctx.Entry(prod).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    //Actualizar Categorias
                    var selectedCategoriasID = new HashSet<string>(selectedCategorias);
                    if (selectedCategorias != null)
                    {
                        ctx.Entry(prod).Collection(p => p.Categoria).Load();
                        var newCategoriaForLibro = ctx.Categoria
                         .Where(x => selectedCategoriasID.Contains(x.CategoriaID.ToString())).ToList();
                        prod.Categoria = newCategoriaForLibro;

                        ctx.Entry(prod).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)prod.ProductoID);

            return oProducto;
        }
    }
}
