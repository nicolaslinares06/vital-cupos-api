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
    public class CuposT003NovedadConfiguration : IEntityTypeConfiguration<CupostT003Novedad>
    {
        public void Configure(EntityTypeBuilder<CupostT003Novedad> entity)
        {
            entity.HasKey(e => e.PkT003codigo)
                  .HasName("PK_CUPOST_T003_NOVEDAD");

            entity.ToTable("CUPOST_T003_NOVEDAD");

            entity.Property(e => e.PkT003codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T003CODIGO");

            entity.Property(e => e.A003codigoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_EMPRESA");

            entity.Property(e => e.A003codigoEmpresaTraslado)
               .HasColumnType("numeric(20, 0)")
               .IsRequired(false)
               .HasColumnName("A003CODIGO_EMPRESA_TRASLADO");

            entity.Property(e => e.A003codigoParametricaDisposicionEspecimen)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003CODIGO_PARAMETRICA_DISPOSICION_ESPECIMEN");

            entity.Property(e => e.A003codigoParametricaTiponovedad)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_PARAMETRICA_TIPONOVEDAD");

            entity.Property(e => e.A003codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A003CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A003codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A003CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A003cuposDisponibles)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003CUPOS_DISPONIBLES");

            entity.Property(e => e.A003estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003ESTADO_REGISTRO");

            entity.Property(e => e.A003estadoEmpresa)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003ESTADO_EMPRESA");

            entity.Property(e => e.A003estadoEmisionPermisosCITES)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A003ESTADO_EMISION_PERMISOS_CITES");

            entity.Property(e => e.A003fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A003FECHA_CREACION");

            entity.Property(e => e.A003fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A003FECHA_MODIFICACION");

            entity.Property(e => e.A003fechaRegistroNovedad)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A003FECHA_REGISTRO_NOVEDAD");

            entity.Property(e => e.A003inventarioDisponible)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003INVENTARIO_DISPONIBLE");

            entity.Property(e => e.A003numeroCupospendientesportramitar)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A003NUMERO_CUPOSPENDIENTESPORTRAMITAR");

            entity.Property(e => e.A003observacionesDetalle)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003OBSERVACIONES_DETALLE");

            entity.Property(e => e.A003observaciones)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003OBSERVACIONES");

            entity.Property(e => e.A003observacionesDetalle)
                .HasMaxLength(200)
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003OBSERVACIONES_DETALLE");

            entity.Property(e => e.A003saldoProduccionDisponible)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .IsUnicode(false)
                .HasColumnName("A003SALDO_PRODUCCION_DISPONIBLE");

            entity.HasOne(d => d.A003codigoParametricaTiponovedadNavigation)
                .WithMany(p => p.CupostT003Novedads)
                .HasForeignKey(d => d.A003codigoParametricaTiponovedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUPOST_T003_NOVEDAD_ADMINT_T008_PARAMETRICA");
        }
    }
}
