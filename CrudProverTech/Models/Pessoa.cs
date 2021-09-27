using CrudProverTech.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudProverTech.Models
{
    public class Pessoa
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        [Remote("ValidaData", "Pessoas")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Range(1, 100, ErrorMessage = "Você não é tão velho assim")]
        public int Idade { get; set; }
        public SexoEnum Sexo { get; set; }
        public bool Ativo { get; set; }
        public Cargo Cargo { get; set; }
        [Display(Name ="Cargo")]
        public int CargoId { get; set; }
    }
}
