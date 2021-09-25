using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProverTech.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Sobrenome { get; set; }
        [Display(Name="Usuário:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NomeUsuario { get; set; }
        public int Idade { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
