using BlogAlex.DB;
using BlogAlex.DB.Classes;
using BlogAlex.Web.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogAlex.Web.Controllers
{
    public class AdministracaoController : Controller
    {
        public object DataPublicacao { get; private set; }
        public object HoraPublicacao { get; private set; }

        // GET: Administracao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.datapublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = new Post();
                post.DataPublicacao = new DateTime(viewModel.datapublicacao.Year,
                                                   viewModel.datapublicacao.Month,
                                                   viewModel.datapublicacao.Day,
                                                   viewModel.HoraPublicacao.Hour,
                                                   viewModel.HoraPublicacao.Minute,
                                                   viewModel.HoraPublicacao.Second);

                post.Titulo = viewModel.Titulo;
                post.Resumo = viewModel.Resumo;
                post.Autor = viewModel.Autor;
                post.Descricao = viewModel.Descricao;
                post.Visivel = viewModel.Visivel;
                post.TagsPost = new List<TagPost>();
                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.tagClasss
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.tagClasss.Add(tagClass);
                            {
                                var tagPost = new TagPost();
                                tagPost.IdTag = item;
                                conexao.TagPosts.Add(tagPost);

                            }
                        }
                    }
                }


                conexao.Posts.Add(post);


                //tratar erro 
                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Erro no banco", exp.Message);
                }
            }

            return View(viewModel);


        }
        public ActionResult EditarPost(int id)
        {
            var conexao = new ConexaoBanco();

            var post = (from x in conexao.Posts
                        where x.Id == id
                        select x).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }
            var viewmodel = new CadastrarPostViewModel();

            viewmodel.datapublicacao = post.DataPublicacao;
            viewmodel.HoraPublicacao = post.DataPublicacao;
            viewmodel.Autor = post.Autor;
            viewmodel.Titulo = post.Titulo;
            viewmodel.Resumo = post.Resumo;
            viewmodel.Descricao = post.Descricao;
            viewmodel.Visivel = post.Visivel;
            viewmodel.Id = post.Id;
            viewmodel.Tags = (from p in post.TagsPost
                              select p.IdTag).ToList();

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();

                var post = (from x in conexao.Posts
                            where x.Id == viewModel.Id
                            select x).FirstOrDefault();


                post.DataPublicacao = new DateTime(viewModel.datapublicacao.Year,
                                                   viewModel.datapublicacao.Month,
                                                   viewModel.datapublicacao.Day,
                                                   viewModel.HoraPublicacao.Hour,
                                                   viewModel.HoraPublicacao.Minute,
                                                   viewModel.HoraPublicacao.Second);
                post.Titulo = viewModel.Titulo;
                post.Resumo = viewModel.Resumo;
                post.Autor = viewModel.Autor;
                post.Descricao = viewModel.Descricao;
                post.Visivel = viewModel.Visivel;
                post.Id = viewModel.Id;

                var postsTagsAtuais = post.TagsPost.ToList();
                foreach (var item in postsTagsAtuais)
                {
                    conexao.TagPosts.Remove(item);
                }


                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.tagClasss
                                         where p.Tag.ToLower() == item.ToLower()
                                         select p).Any();
                        if (!tagExiste)
                        {
                            var tagClass = new TagClass();
                            tagClass.Tag = item;
                            conexao.tagClasss.Add(tagClass);
                            var tagPost = new TagPost();
                            tagPost.IdTag = item;
                            conexao.TagPosts.Add(tagPost);
                        }

                    }
                }
                //tratar erro 
                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Erro no banco", exp.Message);
                }
            }

            return View(viewModel);
        }

        public ActionResult ExcluirPost(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var post = (from p in conexaoBanco.Posts
                        where p.Id == id
                        select p).FirstOrDefault();
            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não existe.", id));
            }
            conexaoBanco.Posts.Remove(post);
            conexaoBanco.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }

            #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
        #endregion
    }
}









