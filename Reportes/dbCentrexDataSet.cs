using System.ComponentModel;
using System.Data;

namespace Centrex
{
    /// <summary>
    /// Implementación mínima del DataSet legacy para mantener compatibilidad
    /// con los formularios de reporte durante la migración.
    /// </summary>
    public class dbCentrexDataSet : DataSet
    {
        public dbCentrexDataSet()
        {
            CaseSensitive = false;
            SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
        }
    }
}

namespace dbCentrexDataSetTableAdapters
{
    /// <summary>
    /// Clase base compacta para los table adapters legacy. Los métodos devuelven
    /// valores neutros para permitir la compilación; la lógica real debe migrarse a EF.
    /// </summary>
    public abstract class BaseTableAdapter : Component
    {
        public bool ClearBeforeFill { get; set; } = true;
    }

    public class factura_cabeceraTableAdapter : BaseTableAdapter
    {
        public virtual int Fill(DataTable dataTable) => 0;
        public virtual DataTable GetData() => _dataTable;
        private readonly DataTable _dataTable = new();
    }

    public class factura_detalleTableAdapter : BaseTableAdapter
    {
        public virtual int Fill(DataTable dataTable) => 0;
        public virtual DataTable GetData() => _dataTable;
        private readonly DataTable _dataTable = new();
    }

    public class datos_empresaTableAdapter : BaseTableAdapter
    {
        public virtual int Fill(DataTable dataTable) => 0;
        public virtual DataTable GetData() => _dataTable;
        private readonly DataTable _dataTable = new();
    }

    public class TableAdapterManager
    {
        public enum UpdateOrderOption
        {
            InsertUpdateDelete
        }

        public bool BackupDataSetBeforeUpdate { get; set; }
        public UpdateOrderOption UpdateOrder { get; set; } = UpdateOrderOption.InsertUpdateDelete;

        public virtual int UpdateAll(Centrex.dbCentrexDataSet dataSet) => 0;
    }
}
