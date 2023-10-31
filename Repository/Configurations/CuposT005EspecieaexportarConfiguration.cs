using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class CuposT005EspecieaexportarConfiguration : IEntityTypeConfiguration<CupostT005Especieaexportar>
    {
        public void Configure(EntityTypeBuilder<CupostT005Especieaexportar> entity)
        {
            entity.HasKey(e => e.PkT005codigo)
                  .HasName("PK_CUPOST_T005_ESPECIEAEXPORTAR");

            entity.ToTable("CUPOST_T005_ESPECIEAEXPORTAR");

            entity.Property(e => e.PkT005codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T005CODIGO");

            entity.Property(e => e.A005añoProduccion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005AÑO_PRODUCCION");

            entity.Property(e => e.A005codigoCupo)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005CODIGO_CUPO");

            entity.Property(e => e.A005codigoEspecie)
                .IsRequired(true)
                .HasColumnName("A005CODIGO_ESPECIE");

            entity.Property(e => e.A005codigoParametricaPagoCuotaDerepoblacion)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005CODIGO_PARAMETRICA_PAGO_CUOTA_DEREPOBLACION");

            entity.Property(e => e.A005codigoParametricaTipoMarcaje)
                .IsRequired(true)
                .HasColumnName("A005CODIGO_PARAMETRICA_TIPO_MARCAJE");

            entity.Property(e => e.A005codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A005codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A005condicionesMarcaje)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005CONDICIONES_MARCAJE");

            entity.Property(e => e.A005cuotaRepoblacionParaAprovechamiento)
                .IsRequired(true)
                .HasColumnName("A005CUOTA_REPOBLACION_PARA_APROVECHAMIENTO")
                .IsFixedLength();

            entity.Property(e => e.A005cupoAprovechamientoOtorgados)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005CUPO_APROVECHAMIENTO_OTORGADOS");

            entity.Property(e => e.A005cupoAprovechamientoOtorgadosPagados)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005CUPO_APROVECHAMIENTO_OTORGADOS_PAGADOS");

            entity.Property(e => e.A005estadoRegistro)
                .HasColumnType("numeric(20,0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005ESTADO_REGISTRO");

            entity.Property(e => e.A005fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A005FECHA_CREACION");

            entity.Property(e => e.A005fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A005FECHA_MODIFICACION");

            entity.Property(e => e.A005fechaRadicado)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A005FECHA_RADICADO");

            entity.Property(e => e.A005individuosDestinadosARepoblacion)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005INDIVIDUOS_DESTINADOS_A_REPOBLACION");

            entity.Property(e => e.A005marcaLote)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005MARCA_LOTE");

            entity.Property(e => e.A005nombreEspecie)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005NOMBRE_ESPECIE");

            entity.Property(e => e.A005observaciones)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005OBSERVACIONES");

            entity.Property(e => e.A005poblacionDisponibleParaCupos)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005POBLACION_DISPONIBLE_PARA_CUPOS");

            entity.Property(e => e.A005poblacionParentalHembra)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005POBLACION_PARENTAL_HEMBRA");

            entity.Property(e => e.A005poblacionParentalMacho)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005POBLACION_PARENTAL_MACHO");

            entity.Property(e => e.A005poblacionParentalTotal)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005POBLACION_PARENTAL_TOTAL");

            entity.Property(e => e.A005poblacionSalioDeIncubadora)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005POBLACION_SALIO_DE_INCUBADORA");

            entity.Property(e => e.A005tasaReposicion)
                .HasMaxLength(50)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A005TASA_REPOSICION");

            entity.Property(e => e.A005NumeroInternoFinalCuotaRepoblacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005NUMERO_INTERNO_FINAL_CUOTA_REPOBLACION");

            entity.Property(e => e.A005NumeroInternoInicialCuotaRepoblacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005NUMERO_INTERNO_INICIAL_CUOTA_REPOBLACION");

            entity.Property(e => e.A005codigoDocumentoSoporte)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005CODIGO_DOCUMENTO_SOPORTE");

            entity.Property(e=> e.A005numeroMortalidad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005NUMERO_MORTALIDAD");

            entity.HasOne(d => d.A005codigoCupoNavigation)
                .WithMany(p => p.CupostT005Especieaexportars)
                .HasForeignKey(d => d.A005codigoCupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T005_ESPECIEAEXPORTAR_CUPOST_T002_CUPO");

            entity.HasOne(d => d.A005codigoUsuarioCreacionNavigation)
                .WithMany(p => p.CupostT005EspecieaexportarA005codigoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.A005codigoUsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T005_ESPECIEAEXPORTAR_ADMIN_T013_AUDITORIA");

            entity.HasOne(d => d.A005codigoUsuarioModificacionNavigation)
                .WithMany(p => p.CupostT005EspecieaexportarA005codigoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.A005codigoUsuarioModificacion)
                .HasConstraintName("FK_CUPOST_T005_ESPECIEAEXPORTAR_ADMIN_T013_AUDITORIA_02");
        }
    }
}
