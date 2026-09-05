using CarAgency.Repository.Persistence;
using System;
using System.Data.SqlClient;

namespace CarAgency.Repository
{
    public class SecurityRepository : BaseRepository
    {
        public void RealizarBackup(string path)
        {
            string nombreArchivo = $"CarAgencyBCK_{DateTime.Now:ddMMyy_HHmm}.bak";
            string rutaCompleta = System.IO.Path.Combine(path, nombreArchivo);
            string comandoBackup = $"BACKUP DATABASE CarAgency TO DISK='{rutaCompleta}'";

            using (SqlConnection conn = new SqlConnection(base.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(comandoBackup, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void RealizarRestore(string path)
        {
            using (SqlConnection conn = new SqlConnection(base.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand setMaster = new SqlCommand("USE master;", conn))
                {
                    setMaster.ExecuteNonQuery();
                }

                using (SqlCommand setSingleUser = new SqlCommand("ALTER DATABASE CarAgency SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", conn))
                {
                    setSingleUser.ExecuteNonQuery();
                }

                string query = $"RESTORE DATABASE CarAgency FROM DISK = '{path}' WITH REPLACE;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand setMultiUser = new SqlCommand("ALTER DATABASE CarAgency SET MULTI_USER;", conn))
                {
                    setMultiUser.ExecuteNonQuery();
                }
            }
        }
    }
}
