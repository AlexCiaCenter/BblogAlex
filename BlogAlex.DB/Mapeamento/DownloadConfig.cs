using BlogAlex.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAlex.DB.Mapeamento
{
    public class DownloadConfig : EntityTypeConfiguration<Download>
    {
        public DownloadConfig()
        {
            ToTable("DOWNLOAD");

            HasKey(X => X.Id);

            Property(x => x.Id)

                .HasColumnName("IDDOWNLOAD")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Ip)

                .HasColumnName("IP")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataHora)

                .HasColumnName("DATAHORA")
                .IsRequired();

            Property(x => x.IdArquivo)
                .HasColumnName("IDARQUIVO")
                .IsRequired();

            HasRequired(X => X.Arquivo)
                .WithMany()
                .HasForeignKey(x => x.IdArquivo);
        }
    }
}
