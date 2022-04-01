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
    public class PersonalEntregaController : Controller
    {
        // GET: PersonalEntrega
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<PersonalEntrega> lista = null;
            try
            {
                IServicePersonalEntrega _ServicePersonalEntrega = new ServicePersonalEntrega();
                lista = _ServicePersonalEntrega.GetPersonalEntrega();
                //lista vehiculs
                IServiceVehiculo _serviceVehiculo = new ServiceVehiculo();
                ViewBag.listaVehiculos1 = _serviceVehiculo.GetVehiculo();
                return View(lista);
            }
            catch (Exception ex)
            {


                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaVehiculo(int idVehiculo = 0)
        {
            //Lista de autores
            IServiceVehiculo _ServiceVehiculo = new ServiceVehiculo();
            IEnumerable<Vehiculo> listaVehiculos = _ServiceVehiculo.GetVehiculo();
            //Autor SelectAutor = listaAutores.Where(c => c.IdAutor == idAutor).FirstOrDefault();
            return new SelectList(listaVehiculos, "VehiculoID", "VehiculoID", idVehiculo);
        }

        // GET: PersonalEntrega/Details/5
        public ActionResult Details(int id)
        {
            ServicePersonalEntrega _ServicePersonalEntrega = new ServicePersonalEntrega();
            PersonalEntrega perso = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                perso = _ServicePersonalEntrega.GetPersonalEntregaByID(id);
                if (perso == null)
                {
                    TempData["Message"] = "No existe el personal solicitado";
                    TempData["Redirect"] = "PersonalEntrega";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(perso);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PersonalEntrega";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: PersonalEntrega/Create
        public ActionResult Create()
        {
            ViewBag.VehiculoID = listaVehiculo();
            return View();
        }

        // POST: PersonalEntrega/Create
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

        // GET: PersonalEntrega/Edit/5
        public ActionResult Edit(int? id)
        {
            ServicePersonalEntrega _ServicePersonalEntrega = new ServicePersonalEntrega();
            PersonalEntrega oPersonal = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oPersonal = _ServicePersonalEntrega.GetPersonalEntregaByID(id.Value);
                if (oPersonal == null)
                {
                    TempData["Message"] = "No existe el personal solicitado";
                    TempData["Redirect"] = "PersonalEntrega";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Lista de Vehiculos
                ViewBag.VehiculoID = listaVehiculo(oPersonal.vehiculoID);
                return View(oPersonal);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PersonalEntrega";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: PersonalEntrega/Edit/5
        [HttpPost]
        public ActionResult Save(PersonalEntrega perso)
        {
            MemoryStream target = new MemoryStream();
            IServicePersonalEntrega _ServicePersonalEntrega = new ServicePersonalEntrega();
            try
            {
                if (ModelState.IsValid)
                {
                    PersonalEntrega oPersonal1 = _ServicePersonalEntrega.Save(perso);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    ViewBag.VehiculoID = listaVehiculo(perso.vehiculoID);
                    return View("Create", perso);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PersonalEntrega";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: PersonalEntrega/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonalEntrega/Delete/5
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
