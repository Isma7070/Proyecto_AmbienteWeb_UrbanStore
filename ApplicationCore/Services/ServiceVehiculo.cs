using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceVehiculo : IServiceVehiculo
    {
        public IEnumerable<Vehiculo> GetVehiculo()
        {
            IRepositoryVehiculo repository = new RespositoryVehiculo();
            return repository.GetVehiculo();
        }

        public Vehiculo GetVehiculoByID(int id)
        {
            IRepositoryVehiculo repository = new RespositoryVehiculo();
            return repository.GetVehiculoByID(id);
        }
    }
}
