using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryPersonalEntrega
    {
        IEnumerable<PersonalEntrega> GetPersonalEntrega();
        PersonalEntrega GetPersonalEntregaByID(int id);
        void DeletePesonalEntrega(int id);
        PersonalEntrega Save(PersonalEntrega personalEntrega);
    }
}
