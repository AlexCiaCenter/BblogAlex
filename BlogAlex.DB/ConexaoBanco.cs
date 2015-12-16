using BlogAlex.DB.Classes;
using BlogAlex.DB.Infra;
using BlogAlex.DB.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAlex.DB
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
       
        public class ConexaoBanco : DbContext
            {
        public static object posts;

        public ConexaoBanco() : base("ConexaoMYSQL")
            {
                Database.Log = (p => Debug.WriteLine(p));
            }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<TagPost> TagPosts { get; set; }
        public DbSet<TagClass> tagClasss{ get; set; }
        public DbSet<Imagem> Imagems { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ConexaoBanco>(new MeuCriadorDeBanco());

            modelBuilder.Configurations.Add(new ArquivoConfig());
            modelBuilder.Configurations.Add(new ComentarioConfig());
            modelBuilder.Configurations.Add(new DownloadConfig());
            modelBuilder.Configurations.Add(new ImagemConfig());
            modelBuilder.Configurations.Add(new PostConfig());
            modelBuilder.Configurations.Add(new TagClassConfig());
            modelBuilder.Configurations.Add(new TagPostConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new VisitaConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
