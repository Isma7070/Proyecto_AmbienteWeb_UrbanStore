using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult IndexProd()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProduct = new ServiceProducto();
                lista = _ServiceProduct.GetProductos();
                return View(lista);
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _ServiceProduct = new ServiceProducto();
                lista = _ServiceProduct.GetProductos();
                return View(lista);
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                producto = _ServiceProducto.GetProductoByID(id.Value);
                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        private MultiSelectList listaCategorias(ICollection<Categoria> categorias)
        {
            //Lista de Categorias
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> listaCategorias = _ServiceCategoria.GetCategoria();
            int[] listaCategoriasSelect = null;

            if (categorias != null)
            {

                listaCategoriasSelect = categorias.Select(c => c.CategoriaID).ToArray();
            }

            return new MultiSelectList(listaCategorias, "CategoriaID", "Descripcion", listaCategoriasSelect);

        }
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = listaCategorias(null);
            return View();
        }


        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                producto = _ServiceProducto.GetProductoByID(id.Value);
                if (producto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.CategoriaID = listaCategorias(producto.Categoria);
                return View(producto);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador)]
        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Save(Producto prod, HttpPostedFileBase ImageFile, string[] selectedCategorias)
        {
            MemoryStream target = new MemoryStream();
            ServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                // TODO: Add update logic here
                // Cuando es Insert Image viene en null porque se pasa diferente
                if (prod.Imagen == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        prod.Imagen = target.ToArray();
                        ModelState.Remove("Imagen");
                    }

                }

                if (ModelState.IsValid)
                {
                    Producto oProductoI = _ServiceProducto.SaveProducto(prod, selectedCategorias);
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.CategoriaID = listaCategorias(prod.Categoria);
                    return View("Create", prod);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
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
