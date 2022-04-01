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
    public class RepositoryPersonalEntrega : IRepositoryPersonalEntrega
    {
        public void DeletePesonalEntrega(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonalEntrega> GetPersonalEntrega()
        {
            try
            {
                IEnumerable<PersonalEntrega> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.PersonalEntrega.Include(x => x.Vehiculo).ToList();
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

        public PersonalEntrega GetPersonalEntregaByID(int id)
        {
            PersonalEntrega oPersonalEntrega = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPersonalEntrega = ctx.PersonalEntrega.
                    Where(p => p.PersonalID == id).
                    Include(x => x.Vehiculo).
                    FirstOrDefault();
            }
            return oPersonalEntrega;
        }

        public PersonalEntrega Save(PersonalEntrega personalEntrega)
        {
            int retorno = 0;
            PersonalEntrega oPersonalEntrega = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPersonalEntrega = GetPersonalEntregaByID((int)personalEntrega.PersonalID);
                IRepositoryVehiculo _RepositoryVehiculo = new RespositoryVehiculo();

                if (oPersonalEntrega == null)
                {

                    //Insertar
                    //Insertar
                    ctx.PersonalEntrega.Add(personalEntrega);
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
                    ctx.PersonalEntrega.Add(personalEntrega);
                    ctx.Entry(personalEntrega).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oPersonalEntrega = GetPersonalEntregaByID((int)personalEntrega.PersonalID);

            return oPersonalEntrega;
        }
    }
}
