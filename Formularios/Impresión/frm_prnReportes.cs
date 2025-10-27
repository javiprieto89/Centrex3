/*
 * FORMULARIO DE REPORTES - COMENTADO TEMPORALMENTE
 * Este formulario maneja la generación de reportes usando ReportViewer
 * Se comenta temporalmente durante la migración de VB.NET a C# y EF Core
 * 
 * TODO: Migrar completamente a EF Core y actualizar lógica de reportes
 */

/*
using System;
using System.Collections.Generic;
using System.Data;
using Centrex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class frm_prnReportes
    {
        // Generales
        private string archivoRpt;
        private int rptID;
        private string spEmpresa;
        private string spCabecera;
        private string spDetalle;

        private string dsEmpresa;
        private string dsCabecera;
        private string dsDetalle;
        // Generales

        // Para cobros
        private bool esCobro;
        // Para pagos
        private bool esPago;

        private string spCheque;
        private string spTransferencia;
        private string spRetencion;

        private string dsCheque;
        private string dsTransferencia;
        private string dsRetencion;
        // Para cobros

        public frm_prnReportes()
        {
            InitializeComponent();
        }

        public frm_prnReportes(string _archivoRpt, string _spEmpresa, string _spCabecera, string _spDetalle, string _dsEmpresa, string _dsCabecera, string _dsDetalle, int _id)
        {
            InitializeComponent();

            archivoRpt = _archivoRpt;
            spEmpresa = _spEmpresa;
            spCabecera = _spCabecera;
            spDetalle = _spDetalle;
            dsEmpresa = _dsEmpresa;
            dsCabecera = _dsCabecera;
            dsDetalle = _dsDetalle;
            rptID = _id;
        }

        public frm_prnReportes(string _archivoRpt, string _spEmpresa, string _spCabecera, string _spCheque, string _spTransferencia, string _spRetencion, string _dsEmpresa, string _dsCabecera, string _dsCheque, string _dsTransferencia, string _dsRetencion, int _id, bool _esCobro)
        {
            InitializeComponent();

            archivoRpt = _archivoRpt;
            spEmpresa = _spEmpresa;
            spCabecera = _spCabecera;
            spCheque = _spCheque;
            spTransferencia = _spTransferencia;
            spRetencion = _spRetencion;
            dsEmpresa = _dsEmpresa;
            dsCabecera = _dsCabecera;
            dsCheque = _dsCheque;
            dsTransferencia = _dsTransferencia;
            dsRetencion = _dsRetencion;
            rptID = _id;
            esCobro = _esCobro;
        }

        public frm_prnReportes(string _archivoRpt, string _spEmpresa, string _spCabecera, string _spCheque, string _spTransferencia, string _dsEmpresa, string _dsCabecera, string _dsCheque, string _dsTransferencia, int _id, bool _esPago)
        {
            InitializeComponent();

            archivoRpt = _archivoRpt;
            spEmpresa = _spEmpresa;
            spCabecera = _spCabecera;
            spCheque = _spCheque;
            spTransferencia = _spTransferencia;
            dsEmpresa = _dsEmpresa;
            dsCabecera = _dsCabecera;
            dsCheque = _dsCheque;
            dsTransferencia = _dsTransferencia;
            rptID = _id;
            esPago = _esPago;
        }

        private void frm_prnReportes_Load(object sender, EventArgs e)
        {
            var procedimientosEjecutados = new List<string>();

            try
            {
                using var context = new CentrexDbContext();

                EjecutarYRegistrar(context, spEmpresa, procedimientosEjecutados);

                if (esCobro)
                {
                    EjecutarYRegistrar(context, spCabecera, procedimientosEjecutados, true);
                    EjecutarYRegistrar(context, spCheque, procedimientosEjecutados, true);
                    EjecutarYRegistrar(context, spTransferencia, procedimientosEjecutados, true);
                    EjecutarYRegistrar(context, spRetencion, procedimientosEjecutados, true);
                }
                else if (esPago)
                {
                    EjecutarYRegistrar(context, spCabecera, procedimientosEjecutados, true);
                    EjecutarYRegistrar(context, spCheque, procedimientosEjecutados, true);
                    EjecutarYRegistrar(context, spTransferencia, procedimientosEjecutados, true);
                }
                else
                {
                    EjecutarYRegistrar(context, spCabecera, procedimientosEjecutados, true);
                    EjecutarYRegistrar(context, spDetalle, procedimientosEjecutados, true);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al ejecutar los procedimientos del reporte: " + ex.Message, Constants.vbCritical);
            }
            finally
            {
                string detalle = procedimientosEjecutados.Count > 0
                    ? string.Join(", ", procedimientosEjecutados)
                    : "Sin procedimientos ejecutados";

                Interaction.MsgBox(
                    "La visualización de reportes está deshabilitada temporalmente." + Environment.NewLine +
                    detalle,
                    MsgBoxStyle.Information,
                    "Centrex");
            }
        }

        private void EjecutarYRegistrar(CentrexDbContext context, string storedProcedure, List<string> resumen, bool incluirId = false)
        {
            if (string.IsNullOrWhiteSpace(storedProcedure))
                return;

            DataTable table = incluirId
                ? ExecuteStoredProcedure(context, storedProcedure, ("@id", rptID))
                : ExecuteStoredProcedure(context, storedProcedure);

            resumen.Add($"{storedProcedure} ({table.Rows.Count})");
        }

        private static DataTable ExecuteStoredProcedure(CentrexDbContext context, string storedProcedure, params (string Name, object? Value)[] parameters)
        {
            var table = new DataTable();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = storedProcedure;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var (name, value) in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = name;
                parameter.Value = value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            using var reader = command.ExecuteReader();
            table.Load(reader);
            return table;
        }
    }
}
*/
