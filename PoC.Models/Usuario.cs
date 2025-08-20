using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Models
{
    /// <summary>
    /// Clase que representa un registro de usuario en la base de datos.
    /// El campo usuarioId es la clave primaria.
    /// El campo identificación es el número de identificación del usuario, que puede ser alfanumérico.
    /// </summary>
    public class Usuario
    {
        public int usuarioId { get; set; }
        public string identificacion { get; set; } = string.Empty;
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;

        [EmailAddress]
        public string email { get; set; } = string.Empty;
    }
}
