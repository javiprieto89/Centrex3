using Centrex.Models;

public static class DbHelper
{
    /// <summary>
    /// Devuelve una nueva instancia del contexto de base de datos CentrexDbContext.
    /// Se usa para mantener compatibilidad con el código existente en VB/C#.
    /// </summary>
    public static CentrexDbContext GetDbContext()
    {
        return new CentrexDbContext();
    }
}

