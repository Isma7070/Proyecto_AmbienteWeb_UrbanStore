using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IserviceUsuario
    {
        public Usuario GetUsuario(int ID, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);
            return repository.GetUsuario(ID, password);
        }

        public Usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            oUsuario.Contrasena = Cryptography.DecrypthAES(oUsuario.Contrasena);
            return oUsuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.Contrasena = Cryptography.EncrypthAES(usuario.Contrasena);
            return repository.Save(usuario);
        }
    }
}
