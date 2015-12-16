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
    public class ArquivoConfig : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfig()
        {
            ToTable("ARQUIVO");

            HasKey(X => X.Id);

            Property(x => x.Id)

                .HasColumnName("IDARQUIVO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)

                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Extensao)

                .HasColumnName("EXTENSAO")
                .HasMaxLength(10)
                .IsRequired();

            Property(x => x.Bytes)

                .HasColumnName("BYTES")
                .IsRequired();

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(X => X.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);


        }
    }
}
