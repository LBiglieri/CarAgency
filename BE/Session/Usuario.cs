using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public class Usuario
    {
        public Usuario()
        {
        }
        [TableColumnAttribute]
        public Guid Usuario_Id { get; set; }
        [TableColumnAttribute]
        public int DNI { get; set; }

        [TableColumnAttribute]
        public string NombreUsuario { get; set; }

        [TableColumnAttribute]
        public string Clave { get; set; }

        [TableColumnAttribute]
        public string Nombre { get; set; }

        [TableColumnAttribute]
        public string Apellido { get; set; }

        [TableColumnAttribute]
        public Guid Rol_Id { get; set; }

        [TableColumnAttribute]
        public Boolean Bloqueado { get; set; }

        [TableColumnAttribute]
        public Boolean Activo { get; set; }

        [TableColumnAttribute]
        public int IntentosDisponibles { get; set; }

        [TableColumnAttribute]
        public string CodigoIdioma { get; set; }

        public ComposedPermiso Rol { get; set; }
    }
}
