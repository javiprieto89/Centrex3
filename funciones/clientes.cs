using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    static class clientes
    {
        // ************************************ FUNCIONES DE CLIENTES ***************************
        public static ClienteEntity info_cliente(int id_cliente)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var clienteEntity = context.ClienteEntity.AsNoTracking().Include(c => c.IdProvinciaFiscalNavigation).Include(c => c.IdProvinciaEntregaNavigation).FirstOrDefault(c => c.IdCliente == id_cliente);

                    if (clienteEntity is not null)
                    {
                        return clienteEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el cliente:" + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }


        }

        public static bool addcliente(ClienteEntity cl)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
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
                    clienteEntity.IdPaisFiscal = cl.IdPaisFiscal;
                    clienteEntity.IdPaisEntrega = cl.IdPaisEntrega;

                    context.ClienteEntity.Add(clienteEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool updatecliente(ClienteEntity cl, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var clienteEntity = context.ClienteEntity.FirstOrDefault(c => c.IdCliente == cl.IdCliente);

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
                            clienteEntity.IdPaisFiscal = cl.IdPaisFiscal;
                            clienteEntity.IdPaisEntrega = cl.IdPaisEntrega;
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool borrarcliente(ClienteEntity cl)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var clienteEntity = context.ClienteEntity.FirstOrDefault(c => c.IdCliente == cl.IdCliente);

                    if (clienteEntity is not null)
                    {
                        context.ClienteEntity.Remove(clienteEntity);
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
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool estaClienteDefault(int id_clientePedidoDefault)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var clienteEntity = context.ClienteEntity.Where(c => c.Activo == true && c.IdCliente == id_clientePedidoDefault).OrderBy(c => c.RazonSocial).FirstOrDefault();

                    return clienteEntity is not null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static int existecliente(string taxNumber)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    string trimmedTaxNumber = taxNumber.Trim();
                    var clienteEntity = context.ClienteEntity.FirstOrDefault(c => c.TaxNumber == trimmedTaxNumber);

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
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}
