using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogAlex.Web.Models.Usuario
{
    public class CadastrarUsuarioViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo título deve ser entre {2} e {1}.")]
        public string Nome { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O login é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo Nome deve possuir no máximo {1} caracteres!")]
        public string Login { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "A senha é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo senha deve possuir no máximo {1} caracteres!")]
        public string Senha { get; set; }

        [DisplayName("Confirmar senha")]
        [Required(ErrorMessage = "o campo confirmar senha é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo confirmar senha deve possuir no máximo {1} caracteres!")]
        [Compare("Senha",ErrorMessage = "As senha digitadas não conferem. ")]
        public string ConfirmarSenha { get; set; }
    }
}