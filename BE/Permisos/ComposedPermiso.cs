using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAgency.BE
{
    public abstract class ComposedPermiso
    {
        [TableColumnAttribute]
        public Guid Permiso_Id { get; set; }
        public string Nombre { get; set; }

        public PermisoType Tipo { get; set; }

        public abstract void AddPermission(ComposedPermiso p);
        public abstract void DeletePermission(ComposedPermiso p);
        public abstract IList<ComposedPermiso> Children { get; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
