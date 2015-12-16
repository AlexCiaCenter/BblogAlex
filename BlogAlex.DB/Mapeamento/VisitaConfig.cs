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
    public class VisitaConfig : EntityTypeConfiguration<Visita>
    {
        public VisitaConfig()
        {
            ToTable("VISITA");

            HasKey(X => X.Id);

            Property(x => x.Id)

                .HasColumnName("IDVISITA")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.DataHora)

                .HasColumnName("DATAHORA")
                .IsRequired();

            Property(x => x.Ip)

                .HasColumnName("IP")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.IdPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);
        }
    }
}
