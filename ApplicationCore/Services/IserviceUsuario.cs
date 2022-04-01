using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IserviceUsuario
    {
        Usuario GetUsuarioByID(int id);
        Usuario Save(Usuario usuario);
        Usuario GetUsuario(int ID, string password);
    }
}
