using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class clientes
    {
        // ************************************ FUNCIONES DE CLIENTES ***************************
        public static cliente info_cliente(string id_cliente)
        {
            var tmp = new cliente();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var clienteEntity = context.Clientes.Include(c => c.ProvinciaFiscal).Include(c => c.ProvinciaEntrega).FirstOrDefault(c => c.IdCliente == Conversions.ToInteger(id_cliente));


                    if (clienteEntity is not null)
                    {
                        tmp.IdCliente = clienteEntity.IdCliente;
                        tmp.RazonSocial = clienteEntity.RazonSocial;
                        tmp.NombreFantasia = clienteEntity.NombreFantasia;
                        tmp.TaxNumber = clienteEntity.TaxNumber;
                        tmp.Contacto = clienteEntity.Contacto;
                        tmp.Telefono = clienteEntity.Telefono;
                        tmp.Celular = clienteEntity.Celular;
                        tmp.Email = clienteEntity.Email;
                        tmp.IdProvinciaFiscal = clienteEntity.IdProvinciaFiscal;
                        tmp.DireccionFiscal = clienteEntity.DireccionFiscal;
                        tmp.LocalidadFiscal = clienteEntity.LocalidadFiscal;
                        tmp.CpFiscal = clienteEntity.CpFiscal;
                        tmp.IdProvinciaEntrega = clienteEntity.IdProvinciaEntrega;
                        tmp.DireccionEntrega = clienteEntity.DireccionEntrega;
                        tmp.LocalidadEntrega = clienteEntity.LocalidadEntrega;
                        tmp.CpEntrega = clienteEntity.CpEntrega;
                        tmp.Notas = clienteEntity.Notas;
                        tmp.EsInscripto = clienteEntity.EsInscripto;
                        tmp.Activo = clienteEntity.Activo;
                        tmp.IdTipoDocumento = clienteEntity.IdTipoDocumento;
                        tmp.IdClaseFiscal = clienteEntity.IdClaseFiscal;
                    }
                    else
                    {
                        tmp.RazonSocial = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.RazonSocial = "error";
            }

            return tmp;
        }

        public static bool addcliente(cliente cl)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var clienteEntity = new ClienteEntity();

                    clienteEntity.RazonSocial = cl.RazonSocial;
                    clienteEntity.NombreFantasia = cl.NombreFantasia;
                    clienteEntity.TaxNumber = cl.TaxNumber;
                    clienteEntity.Contacto = cl.Contacto;
                    clienteEntity.Telefono = cl.Telefono;
                    clienteEntity.Celular = cl.Celular;
                    clienteEntity.Email = cl.Email;
                    clienteEntity.IdProvinciaFiscal = cl.IdProvinciaFiscal;
                    clienteEntity.DireccionFiscal = cl.DireccionFiscal;
                    clienteEntity.LocalidadFiscal = cl.LocalidadFiscal;
                    clienteEntity.CpFiscal = cl.CpFiscal;
                    clienteEntity.IdProvinciaEntrega = cl.IdProvinciaEntrega;
                    clienteEntity.DireccionEntrega = cl.DireccionEntrega;
                    clienteEntity.LocalidadEntrega = cl.LocalidadEntrega;
                    clienteEntity.CpEntrega = cl.CpEntrega;
                    clienteEntity.Notas = cl.Notas;
                    clienteEntity.EsInscripto = cl.EsInscripto;
                    clienteEntity.Activo = cl.Activo;
                    clienteEntity.IdTipoDocumento = cl.IdTipoDocumento;
                    clienteEntity.IdClaseFiscal = cl.IdClaseFiscal;

                    context.Clientes.Add(clienteEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool updatecliente(cliente cl, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var clienteEntity = context.Clientes.FirstOrDefault(c => c.IdCliente == cl.IdCliente);

                    if (clienteEntity is not null)
                    {
                        if (borra == true)
                        {
                            clienteEntity.Activo = false;
                        }
                        else
                        {
                            clienteEntity.RazonSocial = cl.RazonSocial;
                            clienteEntity.NombreFantasia = cl.NombreFantasia;
                            clienteEntity.TaxNumber = cl.TaxNumber;
                            clienteEntity.Contacto = cl.Contacto;
                            clienteEntity.Telefono = cl.Telefono;
                            clienteEntity.Celular = cl.Celular;
                            clienteEntity.Email = cl.Email;
                            clienteEntity.IdProvinciaFiscal = cl.IdProvinciaFiscal;
                            clienteEntity.DireccionFiscal = cl.DireccionFiscal;
                            clienteEntity.LocalidadFiscal = cl.LocalidadFiscal;
                            clienteEntity.CpFiscal = cl.CpFiscal;
                            clienteEntity.IdProvinciaEntrega = cl.IdProvinciaEntrega;
                            clienteEntity.DireccionEntrega = cl.DireccionEntrega;
                            clienteEntity.LocalidadEntrega = cl.LocalidadEntrega;
                            clienteEntity.CpEntrega = cl.CpEntrega;
                            clienteEntity.Notas = cl.Notas;
                            clienteEntity.EsInscripto = cl.EsInscripto;
                            clienteEntity.Activo = cl.Activo;
                            clienteEntity.IdTipoDocumento = cl.IdTipoDocumento;
                            clienteEntity.IdClaseFiscal = cl.IdClaseFiscal;
                        }

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool borrarcliente(cliente cl)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var clienteEntity = context.Clientes.FirstOrDefault(c => c.IdCliente == cl.id_cliente);

                    if (clienteEntity is not null)
                    {
                        context.Clientes.Remove(clienteEntity);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool estaClienteDefault(int id_clientePedidoDefault)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var clienteEntity = context.Clientes.Where(c => c.activo == true && c.IdCliente == id_clientePedidoDefault).OrderBy(c => c.RazonSocial).FirstOrDefault();



                    return clienteEntity is not null;
                }
            }
            catch (Exception ex)
            {
                // MsgBox(ex.Message.ToString)
                return false;
            }
        }

        public static int existecliente(string taxNumber)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var clienteEntity = context.Clientes.FirstOrDefault(c => c.taxNumber == Strings.Trim(taxNumber.ToString()));

                    if (clienteEntity is not null)
                    {
                        return clienteEntity.IdCliente;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        // Public Function info_clienteVendedor(ByVal id_cliente As String) As Integer
        // Dim sqlstr As String
        // Dim id_vendedor As Integer

        // sqlstr = "SELECT c.id_vendedor " & _
        // "FROM clientes AS c " & _
        // "WHERE c.id_vendedor = '" + id_cliente + "'"

        // Try
        // 'Crea y abre una nueva conexión
        // abrirdb(serversql, basedb, usuariodb, passdb)

        // 'Propiedades del SqlCommand
        // Dim comando As New SqlCommand
        // With comando
        // .CommandType = CommandType.Text
        // .CommandText = sqlstr
        // .Connection = CN
        // End With

        // Dim da As New SqlDataAdapter 'Crear nuevo SqlDataAdapter
        // Dim dataset As New DataSet 'Crear nuevo dataset

        // da.SelectCommand = comando

        // 'llenar el dataset
        // da.Fill(dataset, "Tabla")
        // id_vendedor = dataset.Tables("tabla").Rows(0).Item(0).ToString
        // cerrardb()
        // Return id_vendedor
        // Catch ex As Exception
        // MsgBox(ex.Message.ToString)
        // id_vendedor = -1
        // cerrardb()
        // Return id_vendedor
        // End Try
        // End Function

        // Public Function existecliente(ByVal n As String, Optional ByVal a As String = "") As Integer
        // Dim tmp As New cliente

        // Dim sqlstr As String
        // sqlstr = "SELECT id_cliente FROM clientes WHERE razon_social LIKE '%" + Trim(n.ToString) + Trim(a.ToString) + "%'"

        // Try
        // 'Crea y abre una nueva conexión
        // abrirdb(serversql, basedb, usuariodb, passdb)

        // 'Propiedades del SqlCommand
        // Dim comando As New SqlCommand
        // With comando
        // .CommandType = CommandType.Text
        // .CommandText = sqlstr
        // .Connection = CN
        // End With

        // Dim da As New SqlDataAdapter 'Crear nuevo SqlDataAdapter
        // Dim dataset As New DataSet 'Crear nuevo dataset

        // da.SelectCommand = comando

        // 'llenar el dataset
        // da.Fill(dataset, "Tabla")
        // tmp.id_cliente = dataset.Tables("tabla").Rows(0).Item(0).ToString
        // If tmp.id_cliente = 0 Then Return -1
        // cerrardb()
        // Return tmp.id_cliente
        // Catch ex As Exception
        // tmp.razon_social = "error"
        // cerrardb()
        // Return -1
        // End Try
        // End Function
        // ************************************ FUNCIONES DE CLIENTES ***************************
    }
}
