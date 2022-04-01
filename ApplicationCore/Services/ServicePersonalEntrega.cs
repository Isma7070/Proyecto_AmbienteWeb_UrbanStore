using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePersonalEntrega : IServicePersonalEntrega
    {
        public void DeletePesonalEntrega(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonalEntrega> GetPersonalEntrega()
        {
            IRepositoryPersonalEntrega repository = new RepositoryPersonalEntrega();
            return repository.GetPersonalEntrega();
        }

        public PersonalEntrega GetPersonalEntregaByID(int id)
        {
            IRepositoryPersonalEntrega repository = new RepositoryPersonalEntrega();
            return repository.GetPersonalEntregaByID(id);
        }

        public PersonalEntrega Save(PersonalEntrega personalEntrega)
        {
            IRepositoryPersonalEntrega repository = new RepositoryPersonalEntrega();
            return repository.Save(personalEntrega);
        }
    }
}
