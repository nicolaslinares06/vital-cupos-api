using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repository.Configurations;
using Repository.Models;

namespace Repository
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdmintT001Puerto> AdmintT001Puertos { get; set; } = null!;
        public virtual DbSet<AdmintT002Pai> AdmintT002Pais { get; set; } = null!;
        public virtual DbSet<AdmintT003Departamento> AdmintT003Departamentos { get; set; } = null!;
        public virtual DbSet<AdmintT004Ciudad> AdmintT004Ciudads { get; set; } = null!;
        public virtual DbSet<AdmintT005Especiman> AdmintT005Especimen { get; set; } = null!;
        public virtual DbSet<AdmintT006PlantillaDocumento> AdmintT006PlantillaDocumentos { get; set; } = null!;
        public virtual DbSet<AdmintT007AdminTecnica> AdmintT007AdminTecnicas { get; set; } = null!;
        public virtual DbSet<AdmintT008Parametrica> AdmintT008Parametricas { get; set; } = null!;
        public virtual DbSet<AdmintT009Documento> AdmintT009Documentos { get; set; } = null!;
        public virtual DbSet<AdmintT010Modulo> AdmintT010Modulos { get; set; } = null!;
        public virtual DbSet<AdmintT011Rol> AdmintT011Rols { get; set; } = null!;
        public virtual DbSet<AdmintT012Usuario> AdmintT012Usuarios { get; set; } = null!;
        public virtual DbSet<AdmintT013Auditorium> AdmintT013Auditoria { get; set; } = null!;
        public virtual DbSet<AdmintT014RlRolModuloPermiso> AdmintT014RlRolModuloPermisos { get; set; } = null!;
        public virtual DbSet<AdmintT015RlUsuarioRol> AdmintT015RlUsuarioRols { get; set; } = null!;
        public virtual DbSet<AdmintT016RlUsuarioCertificado> AdmintT016RlUsuarioCertificados { get; set; } = null!;
        public virtual DbSet<AdmintT017DiaNoHabil> AdmintT017DiaNoHabils { get; set; } = null!;
        public virtual DbSet<AdmintT018Notificacion> AdmintT018Notificacions { get; set; } = null!;
        public virtual DbSet<AdminT019ExceptionLog> AdminT019ExceptionLog { get; set; } = null!;
        public virtual DbSet<AdmintV001Usuario> AdmintV001Usuarios { get; set; } = null!;
        public virtual DbSet<AdmintV002Role> AdmintV002Roles { get; set; } = null!;
        public virtual DbSet<AdmintV003UsuarioRole> AdmintV003UsuarioRole { get; set; } = null!;
        public virtual DbSet<AdmintV004Auditoria> AdmintV004Auditoria { get; set; } = null!;
        public virtual DbSet<CitestT001Certificado> CitestT001Certificados { get; set; } = null!;
        public virtual DbSet<CitestT002Subpartidaarancelarium> CitestT002Subpartidaarancelaria { get; set; } = null!;
        public virtual DbSet<CitestT003Persona> CitestT003Personas { get; set; } = null!;
        public virtual DbSet<CitestT004Evaluacion> CitestT004Evaluacions { get; set; } = null!;
        public virtual DbSet<CitestT005Recaudo> CitestT005Recaudos { get; set; } = null!;
        public virtual DbSet<CitestT006Informacionespeciman> CitestT006Informacionespecimen { get; set; } = null!;
        public virtual DbSet<CitestT007Salvoconductomovilizacion> CitestT007Salvoconductomovilizacions { get; set; } = null!;
        public virtual DbSet<CitestT008Estado> CitestT008Estados { get; set; } = null!;
        public virtual DbSet<CitestT009ActaSeguimiento> CitestT009ActaSeguimientos { get; set; } = null!;
        public virtual DbSet<CitestT010RlCertificadoDocumento> CitestT010RlCertificadoDocumentos { get; set; } = null!;
        public virtual DbSet<CitestT011RlCertificadoEvaluacion> CitestT011RlCertificadoEvaluacions { get; set; } = null!;
        public virtual DbSet<CitestV001Analistas> CitestV001Analistas { get; set; } = null!;
        public virtual DbSet<CupostT001Empresa> CupostT001Empresas { get; set; } = null!;
        public virtual DbSet<CupostT002Cupo> CupostT002Cupos { get; set; } = null!;
        public virtual DbSet<CupostT003Novedad> CupostT003Novedads { get; set; } = null!;
        public virtual DbSet<CupostT004FacturacompraCartaventum> CupostT004FacturacompraCartaventa { get; set; } = null!;
        public virtual DbSet<CupostT005Especieaexportar> CupostT005Especieaexportars { get; set; } = null!;
        public virtual DbSet<CupostT006Precintosymarquilla> CupostT006Precintosymarquillas { get; set; } = null!;
        public virtual DbSet<CupostT007ActaVisitum> CupostT007ActaVisita { get; set; } = null!;
        public virtual DbSet<CupostT008CortePiel> CupostT008CortePiels { get; set; } = null!;
        public virtual DbSet<CupostT009CuotaPecesPai> CupostT009CuotaPecesPais { get; set; } = null!;
        public virtual DbSet<CupostT010CantidadCuotaPecesPai> CupostT010CantidadCuotaPecesPais { get; set; } = null!;
        public virtual DbSet<CupostT011RlCupoEvaluacion> CupostT011RlCupoEvaluacions { get; set; } = null!;
        public virtual DbSet<CupostT012RlNovedadDocumento> CupostT012RlNovedadDocumentos { get; set; } = null!;
        public virtual DbSet<CupostT013RlFacturaDocumento> CupostT013RlFacturaDocumentos { get; set; } = null!;
        public virtual DbSet<CuposV001ResolucionCupos> CuposV001ResolucionCupos { get; set; } = null!;
        public virtual DbSet<CuposV001Precintoymarquilla> CuposV001Precintosymarquillas { get; set; } = null!;
        public virtual DbSet<CuposV002GestionPrecintosNacionales> CuposV002GestionPrecintosNacionales { get; set; } = null!;
        public virtual DbSet<CuposV003SolicitudPrecintosNacionales> CuposV003SolicitudPrecintosNacionales { get; set; } = null!;
        public virtual DbSet<CuposV004NumeracionesPrecintos> CuposV004NumeracionesPrecintos { get; set; } = null!;
        public virtual DbSet<CupostT014Resolucion> CupostT014Resolucion { get; set; } = null!;
        public virtual DbSet<CupostT015ActaVisitaDocumentoOrigenPiel> CupostT015ActaVisitaDocOrigenPiel { get; set; } = null!;
        public virtual DbSet<CupostT016ActaVisitaResolucion> CupostT016ActaVisitaNumResolucion { get; set; } = null!;
        public virtual DbSet<CupostT017ActaVisitaSalvoConducto> CupostT017ActaVisitaDocSalvoConducto { get; set; } = null!;
        public virtual DbSet<CupostT018ActaVisitaDocumentos> CupostT018ActaVisitaArchivosDocumentos { get; set; } = null!;
        public virtual DbSet<CupostT019Solicitudes> CupostT019Solicitudes { get; set; } = null!;
        public virtual DbSet<CupostT020RlSolicitudesDocumento> CupostT020RlSolicitudesDocumento { get; set; } = null!;
        public virtual DbSet<CupostT021CertificadoFloraNoMaderable> CupostT021CertificadoFloraNoMaderable { get; set; } = null!;
        public virtual DbSet<CupostT022RlCertificadoFloraNoMaderableDocumento> CupostT022RlCertificadoFloraNoMaderableDocumento { get; set; } = null!;
        public virtual DbSet<CupostT023RlCupoDocumento> CupostT023RlCupoDocumento { get; set; } = null!;
        public virtual DbSet<CupostT024RlCuotaPecesPaisDocumento> CupostT024RlCuotaPecesPaisDocumento { get; set; } = null!;
        public virtual DbSet<CupostT025FacturaCompraCartaVentaDocumento> CupostT025FacturaCompraCartaVentaDocumento { get; set; } = null!;
        public virtual DbSet<CupostT026FacturaCompraCupo> CupostT026FacturaCompraCupo { get; set; } = null!;
        public virtual DbSet<Wsv001CheckQuotasSealsLabels> WSV001CheckQuotasSealsLabels { get; set; } = null!;
        public virtual DbSet<CuposV001ReportesEmpresasMarcaje> CuposV001ReportesEmpresas { get; set; } = null!;
        public virtual DbSet<CuposV002ReportesPrecintos> CuposV002ReportesPrecintos { get; set; } = null!;
        public virtual DbSet<CuposV005ActaVisitaCortes> CuposV005ActaVisitaCortes { get; set; } = null!;
        public virtual DbSet<CupostT027NumeracionesSolicitud> CupostT027NumeracionesSolicitud { get; set; } = null!;
        public virtual DbSet<CupostT028SalvoconductosSolicitud> CupostT028SalvoconductosSolicitud { get; set; } = null!;
        public virtual DbSet<CupostT029CortesPielSolicitud> CupostT029CortesPielSolicitud { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:bisait.com\\MSSQLSERVER,1434;Database=MINAMBIENTE;user=sa;password=bisa112233");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminT001PuertoConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT002PaisConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT003DepartamentoConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT004CiudadConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT005EspecimanConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT006PlantillaDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT007AdminTecnicaConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT008ParametricaConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT009DocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT010ModuloConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT011RolConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT012UsuarioConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT013AuditoriumConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT014RlRolModuloPermisoConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT015RLUsuarioRolConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT016RlUsuarioCertificadoConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT017DiaNoHabil());

            modelBuilder.ApplyConfiguration(new AdminT018NotificacionConfiguration());

            modelBuilder.ApplyConfiguration(new AdminT019ExceptionLogCongiguration());

            modelBuilder.ApplyConfiguration(new AdminV001UsuarioConfiguration());

            modelBuilder.ApplyConfiguration(new AdminV002RoleConfiguration());

            modelBuilder.ApplyConfiguration(new AdminV003UsuarioRoleConfiguration());

            modelBuilder.ApplyConfiguration(new AdminV004AuditoriaConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT001CertificadoConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT002SubpartidaarancelariumConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT003PersonaConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT004EvaluacionConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT005RecaudoConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT006InformacionEspecimanConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT007SalvoconductomovilizacionConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT008EstadoConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT009ActaSeguimientoConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT010RlCertificadoDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CitesT011RlCertificadoEvaluacionConfiguration());

            modelBuilder.ApplyConfiguration(new CitesV001AnalistasConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT001EmpresaConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT002CupoConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT003NovedadConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT004FacturacompraCartaventumConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT005EspecieaexportarConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT006PrecintosymarquillaConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT007ActaVisitumConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT008CortePielConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT009CuotaPecesPaisConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT010CantidadCuotaPecesPaisConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT011RlCupoEvaluacionConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT012RlNovedadDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CuposT013RlFacturaDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV001ResolucionCuposConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV001PrecintoymarquillaConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV002GestionPrecintosNacionalesConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV003SolicitudPrecintosNacionalesConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV004NumeracionesPrecintosConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT014ResolucionConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT015ActaVisitaDocumentoOrigenPielConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT016ActaVisitaResolucionConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT017ActaVisitaSalvoConductoConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT018ActaVisitaDocumentosConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT019SolicitudesConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT020RlSolicitudesDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT021CertificadoFloraNoMaderableConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT022RlCertificadoFloraNoMaderableDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT023RlCupoDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT024RlCuotaPecesPaisDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT025FacturaCompraCartaVentaDocumentoConfiguration());

            modelBuilder.ApplyConfiguration(new CupostT026FacturaCompraCupoConfiguration());

            modelBuilder.ApplyConfiguration(new Wsv001CheckQuotasSealsLabelsConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV001ReportesEmpresasMarcajeConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV002ReportesPrecintosConfiguration());

            modelBuilder.ApplyConfiguration(new CuposV005ActaVisitaCortesConfiguration());
            
            modelBuilder.ApplyConfiguration(new CupostT027NumeracionesSolicitudConfiguration());
            
            modelBuilder.ApplyConfiguration(new CupostT029CortesPielSolicitudConfiguration());
            
            modelBuilder.ApplyConfiguration(new CupostT028SalvoconductosSolicitudConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
