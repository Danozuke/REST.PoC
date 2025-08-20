using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoC.Models;
namespace PoC.DAL.Services
{
    /// <summary>
    /// Esta clase de servicio instancia una lista de usuarios con valores predeterminados, la cual podrá ser accedida 
    /// mediante los diferentes métodos del API para simular un CRUD.
    /// </summary>
    public class UsuarioService
    {
        private readonly List<Usuario> _usuarios;


        public UsuarioService() 
        {
            _usuarios = new List<Usuario>
            {
                new Usuario{usuarioId = 1, identificacion = "123456", nombres = "Namuel", apellidos = "Solorzano", email = "namuel.solorzano@fakemail.com"},
                new Usuario{usuarioId = 2, identificacion = "789012", nombres = "Jhon", apellidos = "Mendez", email = "jhon.mendez@fakemail.com"},
                new Usuario{usuarioId = 3, identificacion = "987654", nombres = "Carlos", apellidos = "Cortez", email = "carlos.cortez@fakemail.com"},
                new Usuario{usuarioId = 4, identificacion = "321789", nombres = "Guillermo", apellidos = "Mendoza", email = "guillermo.mendoza@fakemail.com"}
            };
        }

        public List<Usuario> GetAll() => _usuarios;

        public Usuario? GetById(int usuarioId) => _usuarios.FirstOrDefault(u => u.usuarioId == usuarioId);

        public void Add(Usuario usuario)
        {
            usuario.usuarioId = _usuarios.Any() ? _usuarios.Max(u => u.usuarioId) + 1 : 1;
            _usuarios.Add(usuario);
        }

        public bool Update(Usuario usuario)
        {
            var existing = GetById(usuario.usuarioId);
            if (existing == null) return false;

            existing.identificacion = usuario.identificacion;
            existing.nombres = usuario.nombres;
            existing.apellidos = usuario.apellidos;
            existing.email = usuario.email;
            return true;
        }

        public bool Delete(int usuarioId) 
        { 
            var usuario = GetById(usuarioId);
            if(usuario == null) return false;

            _usuarios.Remove(usuario);
            return true;
        }



    }
}
