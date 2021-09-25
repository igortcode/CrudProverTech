using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProverTech.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Display(Name = "Usuário:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NomeUsuario { get; set; }
        [Display(Name = "Senha:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
