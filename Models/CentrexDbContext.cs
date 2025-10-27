using Microsoft.Extensions.Configuration;
using System.IO;

namespace Centrex.Models;

public partial class CentrexDbContext : DbContext
{

    // 🔹 Constructor vacío para compatibilidad con WinForms
    public CentrexDbContext()
        : base(new DbContextOptionsBuilder<CentrexDbContext>()
            .UseSqlServer(GetConnectionString())
            .Options)
    {
    }

    public CentrexDbContext(DbContextOptions<CentrexDbContext> options)
        : base(options)
    {
    }

    private static string GetConnectionString()
    {
        // Intenta leer desde appsettings.json o App.config
        try
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var conn = config.GetConnectionString("CentrexConnection");

            if (!string.IsNullOrEmpty(conn))
                return conn;

            // fallback si no existe appsettings.json
            return "Server=127.0.0.1;Database=dbCentrex;User Id=sa;Password=Ladeda78;TrustServerCertificate=True;";
        }
        catch
        {
            return "Server=127.0.0.1;Database=dbCentrex;User Id=sa;Password=Ladeda78;TrustServerCertificate=True;";
        }
    }


    public virtual DbSet<AjusteStockEntity> AjusteStockEntity { get; set; }

    public virtual DbSet<AsocItemEntity> AsocItemEntity { get; set; }

    public virtual DbSet<BancoEntity> BancoEntity { get; set; }

    public virtual DbSet<CajaEntity> CajaEntity { get; set; }

    public virtual DbSet<CambioEntity> CambioEntity { get; set; }

    public virtual DbSet<CcClienteEntity> CcClienteEntity { get; set; }

    public virtual DbSet<CcProveedorEntity> CcProveedorEntity { get; set; }

    public virtual DbSet<ChequeEntity> ChequeEntity { get; set; }

    public virtual DbSet<ChequesACobrarEntity> ChequesACobrar { get; set; }

    public virtual DbSet<ClienteEntity> ClienteEntity { get; set; }

    public virtual DbSet<CobroChequeEntity> CobroChequeEntity { get; set; }

    public virtual DbSet<CobroEntity> CobroEntity { get; set; }

    public virtual DbSet<CobroNfacturaImporteEntity> CobroNfacturaImporteEntity { get; set; }

    public virtual DbSet<CobroRetencionEntity> CobroRetencionEntity { get; set; }

    public virtual DbSet<ComprobanteCompraConceptoEntity> ComprobanteCompraConceptoEntity { get; set; }

    public virtual DbSet<ComprobanteCompraEntity> ComprobanteCompraEntity { get; set; }

    public virtual DbSet<ComprobanteCompraImpuestoEntity> ComprobanteCompraImpuestoEntity { get; set; }

    public virtual DbSet<ComprobanteCompraItemEntity> ComprobanteCompraItemEntity { get; set; }

    public virtual DbSet<ComprobanteEntity> ComprobanteEntity { get; set; }

    public virtual DbSet<ConceptoCompraEntity> ConceptoCompraEntity { get; set; }

    public virtual DbSet<CondicionCompraEntity> CondicionCompraEntity { get; set; }

    public virtual DbSet<ConsultaPersonalizadaEntity> ConsultaPersonalizadaEntity { get; set; }

    public virtual DbSet<CuentaBancariaEntity> CuentaBancariaEntity { get; set; }

    public virtual DbSet<EmpresaEntity> EmpresaEntity { get; set; }

    public virtual DbSet<ImpuestoEntity> ImpuestoEntity { get; set; }

    public virtual DbSet<ItemEntity> ItemEntity { get; set; }

    public virtual DbSet<ItemImpuestoEntity> ItemImpuestoEntity { get; set; }

    public virtual DbSet<MarcaItemEntity> MarcaItemEntity { get; set; }

    public virtual DbSet<OrdenCompraEntity> OrdenCompraEntity { get; set; }

    public virtual DbSet<OrdenCompraItemEntity> OrdenCompraItemEntity { get; set; }

    public virtual DbSet<PagoChequeEntity> PagoChequeEntity { get; set; }

    public virtual DbSet<PagoEntity> PagoEntity { get; set; }

    public virtual DbSet<PagoNfacturaImporteEntity> PagoNfacturaImporteEntity { get; set; }

    public virtual DbSet<PaisEntity> PaisEntity { get; set; }

    public virtual DbSet<PedidoEntity> PedidoEntity { get; set; }

    public virtual DbSet<PedidoItemEntity> PedidoItemEntity { get; set; }

    public virtual DbSet<PerfilEntity> PerfilEntity { get; set; }

    public virtual DbSet<PermisoEntity> PermisoEntity { get; set; }

    public virtual DbSet<PermisoPerfilEntity> PermisoPerfilEntity { get; set; }

    public virtual DbSet<ProduccionAsocItemEntity> ProduccionAsocItemEntity { get; set; }

    public virtual DbSet<ProduccionEntity> ProduccionEntity { get; set; }

    public virtual DbSet<ProduccionItemEntity> ProduccionItemEntity { get; set; }

    public virtual DbSet<ProveedorEntity> ProveedorEntity { get; set; }

    public virtual DbSet<ProvinciaEntity> ProvinciaEntity { get; set; }

    public virtual DbSet<RegistroStockEntity> RegistroStockEntity { get; set; }

    public virtual DbSet<SeparadorDecimalEntity> SeparadorDecimalEntity { get; set; }

    public virtual DbSet<SysClaseComprobanteEntity> SysClaseComprobanteEntity { get; set; }

    public virtual DbSet<SysClaseFiscalEntity> SysClaseFiscalEntity { get; set; }

    public virtual DbSet<SysEstadoChequeEntity> SysEstadoChequeEntity { get; set; }

    public virtual DbSet<SysModoMiPymeEntity> SysModoMiPymeEntity { get; set; }

    public virtual DbSet<SysMonedaEntity> SysMonedaEntity { get; set; }

    public virtual DbSet<TipoComprobanteEntity> TipoComprobanteEntity { get; set; }

    public virtual DbSet<TipoDocumentoEntity> TipoDocumentoEntity { get; set; }

    public virtual DbSet<TipoItemEntity> TipoItemEntity { get; set; }

    public virtual DbSet<TmpCobroRetencionEntity> TmpCobroRetencionEntity { get; set; }

    public virtual DbSet<TmpOcItemEntity> TmpOcItemEntity { get; set; }

    public virtual DbSet<TmpPedidoItemEntity> TmpPedidoItemEntity { get; set; }

    public virtual DbSet<TmpProduccionAsocItemEntity> TmpProduccionAsocItemEntity { get; set; }

    public virtual DbSet<TmpProduccionItemEntity> TmpProduccionItemEntity { get; set; }

    public virtual DbSet<TmpRegistroStockEntity> TmpRegistroStockEntity { get; set; }

    public virtual DbSet<TmpSelChEntity> TmpSelChEntity { get; set; }

    public virtual DbSet<TmpTransferenciaEntity> TmpTransferenciaEntity { get; set; }

    public virtual DbSet<TransaccionEntity> TransaccionEntity { get; set; }

    public virtual DbSet<TransferenciaEntity> TransferenciaEntity { get; set; }

    public virtual DbSet<UsuarioEntity> UsuarioEntity { get; set; }

    public virtual DbSet<UsuarioPerfilEntity> UsuarioPerfilEntity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AjusteStockEntity>(entity =>
        {
            entity.ToTable("ajustes_stock", tb => tb.HasTrigger("Ajuste_Stock"));

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_ajustes_stock_fecha");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.AjusteStockEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ajustes_stock_items");
        });

        modelBuilder.Entity<AsocItemEntity>(entity =>
        {
            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.AsocItemEntityIdItemNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_asocItems_items");

            entity.HasOne(d => d.IdItemAsocNavigation).WithMany(p => p.AsocItemEntityIdItemAsocNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_asocItems_items1");
        });

        modelBuilder.Entity<BancoEntity>(entity =>
        {
            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.BancoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bancos_paises");
        });

        modelBuilder.Entity<CambioEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cambios_activo");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cambios_fecha");
        });

        modelBuilder.Entity<CcClienteEntity>(entity =>
        {
            entity.ToTable("cc_clientes", tb => tb.HasTrigger("No_borrar_ultima_CC_cliente"));

            entity.Property(e => e.IdCc).ValueGeneratedOnAdd();
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cc_clientes_activo");
            entity.Property(e => e.IdMoneda)
                .HasDefaultValue(1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cc_clientes_id_moneda");
            entity.Property(e => e.Saldo).HasAnnotation("Relational:DefaultConstraintName", "DF_cc_clientes_saldo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CcClienteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cc_clientes_clientes");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.CcClienteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cc_clientes_sysMoneda");
        });

        modelBuilder.Entity<CcProveedorEntity>(entity =>
        {
            entity.ToTable("cc_proveedores", tb => tb.HasTrigger("No_borrar_ultima_CC_proveedor"));

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cc_proveedores_activo");
            entity.Property(e => e.IdMoneda)
                .HasDefaultValue(1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cc_proveedores_id_moneda");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.CcProveedorEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cc_proveedores_sysMoneda");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.CcProveedorEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cc_proveedores_proveedores");
        });

        modelBuilder.Entity<ChequeEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_activo");
            entity.Property(e => e.FechaCobro)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_fecha_cobro");
            entity.Property(e => e.FechaDeposito)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_fecha_deposito");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_fecha_ingreso");
            entity.Property(e => e.FechaSalida)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_fecha_salida");
            entity.Property(e => e.IdCliente)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_id_cliente");
            entity.Property(e => e.IdCuentaBancaria)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_id_cuentaBancaria");
            entity.Property(e => e.IdEstadoch)
                .HasDefaultValue(1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_id_estadoC");
            entity.Property(e => e.IdProveedor)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cheques_id_proveedor");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.ChequeEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cheques_bancos");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ChequeEntity).HasConstraintName("FK_cheques_clientes");

            entity.HasOne(d => d.IdCuentaBancariaNavigation).WithMany(p => p.ChequeEntity).HasConstraintName("FK_cheques_cuentas_bancarias");

            entity.HasOne(d => d.IdEstadochNavigation).WithMany(p => p.ChequeEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cheques_sysestados_cheques");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ChequeEntity).HasConstraintName("FK_cheques_proveedores");
        });

        modelBuilder.Entity<ChequesACobrarEntity>(entity =>
        {
            entity.ToView("cheques_a_cobrar");
        });

        modelBuilder.Entity<ClienteEntity>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Table__677F38F5CAA705E3");

            entity.ToTable("clientes", tb => tb.HasTrigger("creaCCCliente"));

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_clientes_activo");
            entity.Property(e => e.EsInscripto)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_clientes_esInscripto");

            entity.HasOne(d => d.IdClaseFiscalNavigation).WithMany(p => p.ClienteEntity).HasConstraintName("FK_clientes_sys_ClasesFiscales");

            entity.HasOne(d => d.IdProvinciaEntregaNavigation).WithMany(p => p.ClienteEntityIdProvinciaEntregaNavigation).HasConstraintName("FK_clientes_provincias1");

            entity.HasOne(d => d.IdProvinciaFiscalNavigation).WithMany(p => p.ClienteEntityIdProvinciaFiscalNavigation).HasConstraintName("FK_clientes_provincias");

            entity.HasOne(d => d.IdPaisEntregaNavigation).WithMany(p => p.ClienteEntityIdPaisEntregaNavigation).HasConstraintName("FK_clientes_paises1");

            entity.HasOne(d => d.IdPaisFiscalNavigation).WithMany(p => p.ClienteEntityIdPaisFiscalNavigation).HasConstraintName("FK_clientes_paises");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.ClienteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_clientes_tipos_documentos");
        });

        modelBuilder.Entity<CobroChequeEntity>(entity =>
        {
            entity.HasOne(d => d.IdChequeNavigation).WithMany(p => p.CobroChequeEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_cheques_cheques");

            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.CobroChequeEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_cheques_cobros");
        });

        modelBuilder.Entity<CobroEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_activo_1");
            entity.Property(e => e.DineroEnCc).HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_dineroEnCc");
            entity.Property(e => e.Efectivo).HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_efectivo");
            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_fecha_carga");
            entity.Property(e => e.IdCobroNoOficial)
                .HasDefaultValue(-1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_id_cobro_no_oficial");
            entity.Property(e => e.Total).HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_total");
            entity.Property(e => e.TotalCh).HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_totalCh");
            entity.Property(e => e.TotalRetencion).HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_totalRetencion");
            entity.Property(e => e.TotalTransferencia).HasAnnotation("Relational:DefaultConstraintName", "DF_cobros_transferencia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CobroEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_clientes");
        });

        modelBuilder.Entity<CobroNfacturaImporteEntity>(entity =>
        {
            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.CobroNfacturaImporteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_Nfacturas_importes_cobros");
        });

        modelBuilder.Entity<CobroRetencionEntity>(entity =>
        {
            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.CobroRetencionEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_retenciones_cobros");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.CobroRetencionEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_retenciones_impuestos");
        });

        modelBuilder.Entity<ComprobanteCompraConceptoEntity>(entity =>
        {
            entity.HasOne(d => d.IdComprobanteCompraNavigation).WithMany(p => p.ComprobanteCompraConceptoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_conceptos_comprobantes_compras");

            entity.HasOne(d => d.IdConceptoCompraNavigation).WithMany(p => p.ComprobanteCompraConceptoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_conceptos_conceptos_compra");
        });

        modelBuilder.Entity<ComprobanteCompraEntity>(static entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_comprobantes_compras_activo");
            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_comprobantes_compras_fecha_carga");

            entity.HasOne(d => d.IdCcProveedorNavigation).WithMany(p => p.ComprobanteCompraEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_cc_proveedores");

            entity.HasOne(d => d.IdCondicionCompraNavigation).WithMany(p => p.ComprobanteCompraEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_condiciones_compra");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.ComprobanteCompraEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_sysMoneda");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ComprobanteCompraEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_proveedores");

            entity.HasOne(d => d.IdTipoComprobanteNavigation).WithMany(p => p.ComprobanteCompraEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_tipos_comprobantes");
        });

        modelBuilder.Entity<ComprobanteCompraImpuestoEntity>(entity =>
        {
            entity.HasOne(d => d.IdComprobanteCompraNavigation).WithMany(p => p.ComprobanteCompraImpuestoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_impuestos_comprobantes_compras");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.ComprobanteCompraImpuestoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_impuestos_impuestos");
        });

        modelBuilder.Entity<ComprobanteCompraItemEntity>(entity =>
        {
            entity.HasOne(d => d.IdComprobanteCompraNavigation).WithMany(p => p.ComprobanteCompraItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_items_comprobantes_compras");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.ComprobanteCompraItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_compras_items_items");
        });

        modelBuilder.Entity<ComprobanteEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_comprobantes_activo");
            entity.Property(e => e.Testing)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_comprobantes_testing");

            entity.HasOne(d => d.IdModoMiPymeNavigation).WithMany(p => p.ComprobanteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_sys_modoMiPyme");

            entity.HasOne(d => d.IdTipoComprobanteNavigation).WithMany(p => p.ComprobanteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comprobantes_tipos_comprobantes");
        });

        modelBuilder.Entity<ConsultaPersonalizadaEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_consultas_personalizadas_activo");
        });

        modelBuilder.Entity<CuentaBancariaEntity>(entity =>
        {
            entity.HasKey(e => e.IdCuentaBancaria).HasName("PK_cuentas_bancarias_1");

            entity.Property(e => e.Saldo)
                .HasDefaultValue(0m)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_cuentas_bancarias_saldo");

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.CuentaBancariaEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cuentas_bancarias_bancos");

            entity.HasOne(d => d.IdMonedaNavigation).WithMany(p => p.CuentaBancariaEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cuentas_bancarias_sysMoneda");
        });

        modelBuilder.Entity<ImpuestoEntity>(entity =>
        {
            entity.ToTable("impuestos", tb =>
            {
                tb.HasTrigger("CrearItems_Impuestos");
                tb.HasTrigger("activaImpuesto");
            });

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_impuestos_activo");
            entity.Property(e => e.EsPercepcion)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_impuestos_esPercepcion");
            entity.Property(e => e.EsRetencion)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_impuestos_esRetencion");
        });

        modelBuilder.Entity<ItemEntity>(entity =>
        {
            entity.ToTable("items", tb =>
            {
                tb.HasTrigger("BorrarItem_Impuesto");
                tb.HasTrigger("CrearItem_Impuesto");
            });

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_items_activo_2");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(0)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_items_cantidad");
            entity.Property(e => e.EsDescuento)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_items_activo");
            entity.Property(e => e.EsMarkup)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_items_activo_1");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.ItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items_marcas_items");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items_proveedores");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.ItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items_tipos_items");
        });

        modelBuilder.Entity<ItemImpuestoEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_items_impuestos_activo");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.ItemImpuestoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items_impuestos_impuestos");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.ItemImpuestoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_items_impuestos_items");
        });

        modelBuilder.Entity<MarcaItemEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_marcas_items_activo");
        });

        modelBuilder.Entity<OrdenCompraEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_ordenes_compras_activo");
            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_ordenes_compras_fecha");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.OrdenCompraEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordenes_compras_proveedores");
        });

        modelBuilder.Entity<OrdenCompraItemEntity>(entity =>
        {
            entity.HasKey(e => e.IdOcItem).HasName("PK_ordenesCompra_items");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.OrdenCompraItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordenesCompras_items_items");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.OrdenCompraItemEntity).HasConstraintName("FK_ordenesCompra_items_ordenes_compras");
        });

        modelBuilder.Entity<PagoChequeEntity>(entity =>
        {
            entity.HasOne(d => d.IdChequeNavigation).WithMany(p => p.PagoChequeEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_cheques_cheques");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.PagoChequeEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_cheques_pagos");
        });

        modelBuilder.Entity<PagoEntity>(static entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_activo");
            entity.Property(e => e.DineroEnCc).HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_dineroEnCc");
            entity.Property(e => e.Efectivo).HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_efectivo");
            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_fecha_carga");
            entity.Property(e => e.Total).HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_total");
            entity.Property(e => e.TotalCh).HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_totalCh");
            entity.Property(e => e.TotalTransferencia).HasAnnotation("Relational:DefaultConstraintName", "DF_pagos_transferencia");

            entity.HasOne(d => d.IdCcProveedorNavigation).WithMany(p => p.PagoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_cc_proveedores");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.PagoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_proveedores");
        });

        modelBuilder.Entity<PagoNfacturaImporteEntity>(entity =>
        {
            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.PagoNfacturaImporteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pagos_nFacturas_importes_pagos");
        });

        modelBuilder.Entity<PedidoEntity>(entity =>
        {
            entity.ToTable("pedidos", tb => tb.HasTrigger("ingresoPresupuesto"));

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_activo");
            entity.Property(e => e.Cerrado)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_cerrado");
            entity.Property(e => e.EsDuplicado)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_esDuplicado");
            entity.Property(e => e.EsPresupuesto)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_esPresupuesto");
            entity.Property(e => e.EsTest)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_esTest");
            entity.Property(e => e.FechaEdicion)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_fecha_edicion");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.PedidoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_clientes");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.PedidoEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_comprobantes");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PedidoEntity).HasConstraintName("FK_pedidos_usuarios");

            entity.HasOne(d => d.CcClienteEntity).WithMany(p => p.PedidoEntity).HasConstraintName("FK_pedidos_cc_clientes");
        });

        modelBuilder.Entity<PedidoItemEntity>(entity =>
        {
            entity.HasKey(e => e.IdPedidoItem).HasName("PK_pedidos_items_1");

            entity.ToTable("pedidos_items", tb =>
            {
                tb.HasTrigger("Debitar_Stock");
                tb.HasTrigger("Regresar_Stock");
                tb.HasTrigger("Update_Stock");
            });

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_items_activo");
            entity.Property(e => e.Precio)
                .HasDefaultValue(1m)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_pedidos_items_precio");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.PedidoItemEntity).HasConstraintName("FK_pedidos_items_items");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoItemEntity).HasConstraintName("FK_pedidos_items_pedidos1");
        });

        modelBuilder.Entity<PermisoPerfilEntity>(entity =>
        {
            entity.HasOne(d => d.IdPefilNavigation).WithMany(p => p.PermisoPerfilEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_permisos_perfiles_perfiles");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.PermisoPerfilEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_permisos_perfiles_permisos");
        });

        modelBuilder.Entity<ProduccionAsocItemEntity>(entity =>
        {
            entity.HasOne(d => d.IdProduccionNavigation).WithMany(p => p.ProduccionAsocItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produccion_asocItems_produccion");

            entity.HasOne(d => d.AsocItemEntity).WithMany(p => p.ProduccionAsocItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produccion_asocItems_asocItems");
        });

        modelBuilder.Entity<ProduccionEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_produccion_activo");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProduccionEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produccion_proveedores");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ProduccionEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produccion_usuarios");
        });

        modelBuilder.Entity<ProduccionItemEntity>(entity =>
        {
            entity.ToTable("produccion_items", tb => tb.HasTrigger("Update_Stock_Produccion"));

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_produccion_items_activo");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.ProduccionItemEntityIdItemNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produccion_items_items");

            entity.HasOne(d => d.IdItemRecibidoNavigation).WithMany(p => p.ProduccionItemEntityIdItemRecibidoNavigation).HasConstraintName("FK_produccion_items_items1");

            entity.HasOne(d => d.IdProduccionNavigation).WithMany(p => p.ProduccionItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produccion_items_produccion");
        });

        modelBuilder.Entity<ProveedorEntity>(entity =>
        {
            entity.ToTable("proveedores", tb => tb.HasTrigger("creaCCProveedor"));

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_proveedores_activo");
            entity.Property(e => e.EsInscripto)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_proveedores_esInscripto");

            entity.HasOne(d => d.IdClaseFiscalNavigation).WithMany(p => p.ProveedorEntity).HasConstraintName("FK_proveedores_sys_ClasesFiscales");

            entity.HasOne(d => d.IdProvinciaEntregaNavigation).WithMany(p => p.ProveedorEntityIdProvinciaEntregaNavigation).HasConstraintName("FK_proveedores_provincias1");

            entity.HasOne(d => d.IdProvinciaFiscalNavigation).WithMany(p => p.ProveedorEntityIdProvinciaFiscalNavigation).HasConstraintName("FK_proveedores_provincias");

            entity.HasOne(d => d.IdPaisEntregaNavigation).WithMany(p => p.ProveedorEntityIdPaisEntregaNavigation).HasConstraintName("FK_proveedores_paises1");

            entity.HasOne(d => d.IdPaisFiscalNavigation).WithMany(p => p.ProveedorEntityIdPaisFiscalNavigation).HasConstraintName("FK_proveedores_paises");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.ProveedorEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_proveedores_tipos_documentos");
        });

        modelBuilder.Entity<ProvinciaEntity>(entity =>
        {
            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.ProvinciaEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_provincias_paises");
        });

        modelBuilder.Entity<RegistroStockEntity>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK_registros_stock_1");

            entity.ToTable("registros_stock", tb => tb.HasTrigger("UpdateItems"));

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_registros_stock_activo");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_registros_stock_fecha_ingreso");
            entity.Property(e => e.IdProveedor)
                .HasDefaultValue(1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_registros_stock_id_proveedor");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.RegistroStockEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_registros_stock_items");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.RegistroStockEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_registros_stock_proveedores");
        });

        modelBuilder.Entity<SysModoMiPymeEntity>(entity =>
        {
            entity.HasKey(e => e.IdModoMiPyme).HasName("PK_sys:_modoMiPyme");
        });

        modelBuilder.Entity<TipoComprobanteEntity>(entity =>
        {
            entity.Property(e => e.IdTipoComprobante).ValueGeneratedNever();
            entity.Property(e => e.SignoCliente).IsFixedLength();
            entity.Property(e => e.SignoProveedor).IsFixedLength();

            entity.HasOne(d => d.IdClaseComprobanteNavigation).WithMany(p => p.TipoComprobanteEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tipos_comprobantes_sys_claseComprobante");
        });

        modelBuilder.Entity<TipoDocumentoEntity>(entity =>
        {
            entity.Property(e => e.IdTipoDocumento).ValueGeneratedNever();
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tipos_documentos_activo");
        });

        modelBuilder.Entity<TipoItemEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tipos_items_activo");
        });

        modelBuilder.Entity<TmpCobroRetencionEntity>(entity =>
        {
            entity.HasKey(e => e.IdTmpRetencion).HasName("PK_tmpretenciones");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.TmpCobroRetencionEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpretenciones_impuestos");
        });

        modelBuilder.Entity<TmpOcItemEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpOC_items_activo");

            entity.HasOne(d => d.IdOcItemNavigation).WithMany(p => p.TmpOcItemEntity).HasConstraintName("FK_tmpOC_items_ordenesCompra_items");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.TmpOcItemEntity).HasConstraintName("FK_tmpOC_items_ordenes_compras");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TmpOcItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpOC_items_usuarios");
        });

        modelBuilder.Entity<TmpPedidoItemEntity>(entity =>
        {
            entity.HasKey(e => e.IdTmpPedidoItem).IsClustered(false);

            entity.HasIndex(e => e.IdItem, "IX_tmppedidos_items").IsClustered();

            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmppedidos_items_activo");
            entity.Property(e => e.IdPedido)
                .HasDefaultValueSql("(NULL)")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmppedidos_items_id_pedido");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.TmpPedidoItemEntity).HasConstraintName("FK_tmppedidos_items_pedidos");

            entity.HasOne(d => d.IdPedidoItemNavigation).WithMany(p => p.TmpPedidoItemEntity)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tmppedidos_items_pedidos_items");
        });

        modelBuilder.Entity<TmpProduccionAsocItemEntity>(entity =>
        {
            entity.HasOne(d => d.IdProduccionNavigation).WithMany(p => p.TmpProduccionAsocItemEntity).HasConstraintName("FK_tmpproduccion_asocItems_produccion");

            entity.HasOne(d => d.IdProduccionItemNavigation).WithMany(p => p.TmpProduccionAsocItemEntity).HasConstraintName("FK_tmpproduccion_asocItems_produccion_items");

            entity.HasOne(d => d.IdTmpProduccionItemNavigation).WithMany(p => p.TmpProduccionAsocItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpproduccion_asocItems_tmpproduccion_items");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TmpProduccionAsocItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpproduccion_asocItems_usuarios");

            entity.HasOne(d => d.AsocItemEntity).WithMany(p => p.TmpProduccionAsocItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpproduccion_asocItems_asocItems");
        });

        modelBuilder.Entity<TmpProduccionItemEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpproduccion_items_activo");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.TmpProduccionItemEntity).HasConstraintName("FK_tmpproduccion_items_items");

            entity.HasOne(d => d.IdProduccionNavigation).WithMany(p => p.TmpProduccionItemEntity).HasConstraintName("FK_tmpproduccion_items_produccion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TmpProduccionItemEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpproduccion_items_usuarios");
        });

        modelBuilder.Entity<TmpRegistroStockEntity>(entity =>
        {
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpregistros_stock_activo");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpregistros_stock_fecha_ingreso");
            entity.Property(e => e.IdIngreso)
                .HasDefaultValueSql("([dbo].[idultimoingreso]()+(1))")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpregistros_stock_id_ingreso");
            entity.Property(e => e.IdProveedor)
                .HasDefaultValue(1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpregistros_stock_id_proveedor");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.TmpRegistroStockEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpregistros_stock_items");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TmpRegistroStockEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpregistros_stock_proveedores");
        });

        modelBuilder.Entity<TmpSelChEntity>(entity =>
        {
            entity.Property(e => e.Seleccionado)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_tmpSelCH_seleccionado");

            entity.HasOne(d => d.IdChequeNavigation).WithMany(p => p.TmpSelChEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmpSelCH_cheques");

            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.TmpSelChEntity).HasConstraintName("FK_tmpSelCH_cobros");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.TmpSelChEntity).HasConstraintName("FK_tmpSelCH_pagos");
        });

        modelBuilder.Entity<TmpTransferenciaEntity>(entity =>
        {
            entity.HasOne(d => d.IdCuentaBancariaNavigation).WithMany(p => p.TmpTransferenciaEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmptransferencias_cuentas_bancarias");
        });

        modelBuilder.Entity<TransaccionEntity>(entity =>
        {
            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.TransaccionEntity).HasConstraintName("FK_transacciones_cobros");

            entity.HasOne(d => d.IdComprobanteCompraNavigation).WithMany(p => p.TransaccionEntity).HasConstraintName("FK_transacciones_comprobantes_compras");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.TransaccionEntity).HasConstraintName("FK_transacciones_pagos");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.TransaccionEntity).HasConstraintName("FK_transacciones_pedidos");

            entity.HasOne(d => d.IdTipoComprobanteNavigation).WithMany(p => p.TransaccionEntity).HasConstraintName("FK_transacciones_tipos_comprobantes");
        });

        modelBuilder.Entity<TransferenciaEntity>(entity =>
        {
            entity.HasKey(e => e.IdTransferencia).HasName("PK_cobros_transferencias");

            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.TransferenciaEntity).HasConstraintName("FK_cobros_transferencias_cobros");

            entity.HasOne(d => d.IdCuentaBancariaNavigation).WithMany(p => p.TransferenciaEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cobros_transferencias_cuentas_bancarias");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.TransferenciaEntity).HasConstraintName("FK_transferencias_pagos");
        });

        modelBuilder.Entity<UsuarioEntity>(entity =>
        {
            entity.Property(e => e.Logueado)
                .HasDefaultValue(false)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_usuarios_logueado");
        });

        modelBuilder.Entity<UsuarioPerfilEntity>(entity =>
        {
            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.UsuarioPerfilEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarios_perfiles_perfiles");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioPerfilEntity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarios_perfiles_usuarios");
        });

        OnModelCreatingGeneratedFunctions(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
