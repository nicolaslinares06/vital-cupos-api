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
    public class CitesT005RecaudoConfiguration : IEntityTypeConfiguration<CitestT005Recaudo>
    {
        public void Configure(EntityTypeBuilder<CitestT005Recaudo> entity)
        {
            entity.HasKey(e => e.PkT005codigo)
                .HasName("PK_CITEST_T005_RECAUDO");

            entity.ToTable("CITEST_T005_RECAUDO");

            entity.Property(e => e.PkT005codigo)
                .HasColumnType("numeric(20, 0)")
                .ValueGeneratedOnAdd()
                .HasColumnName("PK_T005CODIGO");

            entity.Property(e => e.A005banco)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005BANCO");

            entity.Property(e => e.A005codigoCertificado)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005CODIGO_CERTIFICADO");

            entity.Property(e => e.A005codigoDocumentoSoportetransferencia)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005CODIGO_DOCUMENTO_SOPORTETRANSFERENCIA");

            entity.Property(e => e.A005codigoParametricaTipoPago)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005CODIGO_PARAMETRICA_TIPO_PAGO");

            entity.Property(e => e.A005codigoParametricaTipodocumento)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005CODIGO_PARAMETRICA_TIPODOCUMENTO");

            entity.Property(e => e.A005codigoUsuarioCreacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005CODIGO_USUARIO_CREACION");

            entity.Property(e => e.A005codigoUsuarioModificacion)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(false)
                .HasColumnName("A005CODIGO_USUARIO_MODIFICACION");

            entity.Property(e => e.A005estadoRegistro)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005ESTADO_REGISTRO");

            entity.Property(e => e.A005fechaConsignacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A005FECHA_CONSIGNACION");

            entity.Property(e => e.A005fechaCreacion)
                .HasColumnType("datetime")
                .IsRequired(true)
                .HasColumnName("A005FECHA_CREACION");

            entity.Property(e => e.A005fechaModificacion)
                .HasColumnType("datetime")
                .IsRequired(false)
                .HasColumnName("A005FECHA_MODIFICACION");

            entity.Property(e => e.A005numeroCuenta)
                .HasColumnType("numeric(20, 0)")
                .IsRequired(true)
                .HasColumnName("A005NUMERO_CUENTA");

            entity.Property(e => e.A005observaciones)
                .HasMaxLength(200)
                .IsRequired(true)
                .IsUnicode(false)
                .HasColumnName("A005OBSERVACIONES");

            entity.Property(e => e.A005valor)
                .HasColumnType("numeric(18, 0)")
                .IsRequired(true)
                .HasColumnName("A005VALOR");

            entity.HasOne(d => d.A005codigoCertificadoNavigation)
                .WithMany(p => p.CitestT005Recaudos)
                .HasForeignKey(d => d.A005codigoCertificado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T005_RECAUDO_CITEST_T001_CERTIFICADO");

            entity.HasOne(d => d.A005codigoDocumentoSoportetransferenciaNavigation)
                .WithMany(p => p.CitestT005Recaudos)
                .HasForeignKey(d => d.A005codigoDocumentoSoportetransferencia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T005_RECAUDO_ADMINT_T009_DOCUMENTO");

            entity.HasOne(d => d.A005codigoParametricaTipoPagoNavigation)
                .WithMany(p => p.CitestT005Recaudos)
                .HasForeignKey(d => d.A005codigoParametricaTipoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITEST_T005_RECAUDO_ADMINT_T008_PARAMETRICA");
        }
    }
}
