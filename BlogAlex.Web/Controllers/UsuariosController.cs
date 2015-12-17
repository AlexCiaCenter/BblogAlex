using BlogAlex.DB;
using BlogAlex.DB.Classes;
using BlogAlex.Web.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogAlex.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {

        // GET: Usuarios
        public ActionResult Index()
        {
            var conexao = new ConexaoBanco();
            var usuarios = (from x in conexao.Usuarios
                           orderby x.Nome
                           select x).ToList();
            return View(usuarios);
        }
        public ActionResult CadastrarUsuario()
        {
           return View();
        }

        // GET: Usuario
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.Login = viewmodel.Nome.ToUpper();
                usuario.Nome = viewmodel.Login;
                usuario.Senha = viewmodel.Senha;

                conexao.Usuarios.Add(usuario);
                try
                {
                    var jaexiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();

                    if (jaexiste)
                    {
                        throw new Exception(string.Format("Usuario com código{0} não encontrado.", viewmodel.Id));
                    }

                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {

                    ModelState.AddModelError("Ocorreu um erro no banco ao Incluir", exp.Message); ;
                }


            }
            return View(viewmodel);
        }

        public ActionResult EditarUsuario(int id)
        {
            var conexao = new ConexaoBanco();

            var usuario = (from x in conexao.Usuarios
                           where x.Id == id
                           select x).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuário com código {0} não encontrado.", id));
            }
            var viewmodel = new CadastrarUsuarioViewModel();

            viewmodel.Nome = usuario.Nome;
            viewmodel.Login = usuario.Login;
            viewmodel.Senha = usuario.Senha;
            viewmodel.Id = usuario.Id;
            return View(viewmodel);


        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {

                var conexao = new ConexaoBanco();
                var usuario = (from x in conexao.Usuarios
                               where x.Id == viewmodel.Id
                               select x).FirstOrDefault();

                viewmodel.Id = usuario.Id;
                usuario.Login = viewmodel.Login.ToUpper();
                usuario.Nome = viewmodel.Nome;
                usuario.Senha = viewmodel.Senha;

                conexao.Usuarios.Add(usuario);
                try
                {
                    var jaexiste = (from p in conexao.Usuarios
                                    where p.Login.ToUpper() == usuario.Login
                                    select p).Any();

                    if (jaexiste)
                    {
                        throw new Exception(string.Format("Já existe Login cadastrado com o login {0}.", usuario.Login));
                    }

                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {

                    ModelState.AddModelError("Ocorreu um erro no banco ao salvar", exp.Message); ;
                }


            }
            return View(viewmodel);
        }


        public ActionResult ExcluirUsuario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var usuario = (from p in conexaoBanco.Usuarios
                        where p.Id == id
                        select p).FirstOrDefault();
            if (usuario == null)
            {
                throw new Exception(string.Format("Usuário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Usuarios.Remove(usuario);
            conexaoBanco.SaveChanges();

            return RedirectToAction("Index");


        }
    }
}

