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
    public class ComentarioConfig : EntityTypeConfiguration<Comentario>
    {
        public ComentarioConfig()
        {
            ToTable("COMENTARIO");

            HasKey(X => X.Id);

            Property(x => x.Id)

                .HasColumnName("IDCOMENTARIO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)

                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataHora)

                .HasColumnName("DATAHORA")
                .IsRequired();



            Property(x => x.Descricao)

                .HasColumnName("DESCRICAO")
                .IsMaxLength()
                .IsRequired();

            Property(x => x.AdmPost)
                .HasColumnName("ADMPOST")
                .IsRequired();

            Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100);


            Property(x => x.PaginaWeb)
                .HasColumnName("PAGINAWEB")
                .HasMaxLength(100);
                

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(X => X.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);
            
        }
    }
}
