namespace CarAgency.BE
{
    // Resultado que devuelven los SP de escritura (columnas SQLResultType y message).
    public class SQLUpdateResult
    {
        public SQLResultType sqlResult;
        public string message;

        public SQLUpdateResult(SQLResultType sqlResult, string message)
        {
            this.sqlResult = sqlResult;
            this.message = message;
        }
    }

    public enum SQLResultType
    {
        success,
        database_error,
        validation_error,
        registry_already_exists
    }
}
