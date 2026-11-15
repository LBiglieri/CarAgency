using System;
using System.Data;
using System.Data.SqlClient;

namespace CarAgency.DAL.Persistence
{
    // Usuario de la aplicacion que viaja con cada conexion. La base se conecta siempre con el mismo
    // login, asi que los triggers de la bitacora de cambios leen el usuario de SESSION_CONTEXT.
    // La DAL no conoce la sesion: el valor lo carga la capa de seguridad al iniciar y cerrar sesion.
    public static class DatabaseSessionContext
    {
        public const string UsuarioIdKey = "Usuario_Id";

        // Se guarda como object para que la asignacion sea atomica.
        private static volatile object usuarioId;

        public static Guid? UsuarioId
        {
            get { return (Guid?)usuarioId; }
            set { usuarioId = value; }
        }

        // Se ejecuta en cada conexion recien abierta y antes del SP: una conexion reciclada del pool
        // nunca conserva el usuario de una llamada anterior.
        internal static void Apply(SqlConnection connection)
        {
            using (var command = new SqlCommand("sys.sp_set_session_context", connection) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add("@key", SqlDbType.NVarChar, 128).Value = UsuarioIdKey;
                command.Parameters.Add("@value", SqlDbType.Variant).Value = (object)UsuarioId ?? DBNull.Value;
                command.ExecuteNonQuery();
            }
        }
    }
}
