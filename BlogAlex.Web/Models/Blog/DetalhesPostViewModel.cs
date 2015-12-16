using BlogAlex.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogAlex.Web.Models.Blog
{
    public class DetalhesPostViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
        public Boolean Visivel { get; set; }
        public string Resumo { get; set; }
        public int QtdeComentario { get; set; }
        public string Descricao { get; set; }
        public IList<TagClass> Tag { get; set; }

        /*CADASTRAR COMENTARIO*/

        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo e-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage = "E-mail inválido ")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string ComentarioDescriao { get; set; }

        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web deve possuir no máximo {1} caracteres!")]
        public string ComentarioPaginaWeb { get; set; }
    }
}