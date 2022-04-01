using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePersonalEntrega
    {
        IEnumerable<PersonalEntrega> GetPersonalEntrega();
        PersonalEntrega GetPersonalEntregaByID(int id);
        void DeletePesonalEntrega(int id);
        PersonalEntrega Save(PersonalEntrega personalEntrega);
    }
}
