﻿using BlogAlex.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAlex.DB.Mapeamento
{
    public class TagPostConfig : EntityTypeConfiguration<TagPost>
    {
        public TagPostConfig()
        {
            ToTable("TAGPOST");

            HasKey(X => X.Id);

            Property(x => x.Id)

                .HasColumnName("IDTAGPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdTag)

                .HasColumnName("IDTAG")
                .HasMaxLength(20)
                .IsRequired();

            HasRequired(x => x.Post)
                 .WithMany()
                 .HasForeignKey(x => x.IdPost);

            HasRequired(x => x.TagClass)
                 .WithMany()
                 .HasForeignKey(x => x.IdTag);
        }
    }
}
