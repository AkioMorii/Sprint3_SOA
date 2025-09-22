using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Models
{
    public class Contatos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(40)]
        public string Nome { get; set; }
        [StringLength(40)]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve conter exatamente 11 Números.")]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [RegularExpression(@"^\(?\d{2}\)?\s?\d{5}-\d{4}$|^\(?\d{2}\)?\s?\d{4}-\d{4}$", ErrorMessage = "O telefone deve conter entre 10 e 11 números.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [StringLength(50)]
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? UltimaAlteracao { get; set; }
    }
}
