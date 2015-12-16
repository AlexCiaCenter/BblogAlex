using BlogAlex.DB;
using BlogAlex.DB.Classes;
using BlogAlex.Web.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogAlex.Web.Controllers
{
    public class BlogController : Controller
    {

        // GET: Blog
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {
            var paginaCorreta = pagina.GetValueOrDefault(1);
            
            //Listar por paginas = 10
            var registroPorPagina = 10;

            //criando conexao com o banco
            var ConexaoBanco = new ConexaoBanco();
            var posts = (from p in ConexaoBanco.Posts
                         where p.Visivel == true
                         select p);
            if (!string.IsNullOrEmpty(tag))
            {
                posts = (from p in posts
                         where p.TagsPost.Any(x => x.IdTag.ToUpper() == tag.ToUpper())
                         select p);
            }
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts
                         where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                            || p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }

            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registroPorPagina);
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / registroPorPagina);



            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts
                               orderby p.DataPublicacao descending
                               select p).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalPaginas = (int)qtdePaginas;
            viewModel.Tag = tag;
            viewModel.Tags = (from p in ConexaoBanco.tagClasss
                              where ConexaoBanco.TagPosts.Any(x => x.IdTag == p.Tag)
                              orderby p.Tag
                              select p.Tag).ToList();
            viewModel.Pesquisa = pesquisa;
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        #region Post
        public ActionResult Post(int id)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts
                         where p.Id == id
                         select new DetalhesPostViewModel
                         {
                             Id = p.Id,
                             Autor = p.Autor,
                             DataPublicacao = p.DataPublicacao,
                             Titulo = p.Titulo,
                             Resumo = p.Resumo,
                             Visivel = p.Visivel,
                             QtdeComentario = p.Comentarios.Count,
                             Descricao = p.Descricao,
                             Tag = p.TagsPost.Select(x => x.TagClass).ToList()
                         }).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado", id));
            }

            return View(post);
        }
        #endregion

        


    }
}

     