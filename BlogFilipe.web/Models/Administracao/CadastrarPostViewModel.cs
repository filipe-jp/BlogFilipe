using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogFilipe.web.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo deve ser entre 2 e 100.")]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo deve ser entre 2 e 100.")]
        public string Autor { get; set; }

        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O campo Resumo é obrigatório.")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "A quantidade de caracteres no campo deve ser entre 2 e 1000.")]
        public string Resumo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao { get; set; }

        [DisplayName("Data de publicação")]
        [Required(ErrorMessage = "O campo Data de publicação é obrigatório.")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora de publicação")]
        [Required(ErrorMessage = "O campo Hora de publicação é obrigatório.")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Visível")]
        [Required(ErrorMessage = "O campo Visível é obrigatório.")]
        public bool Visivel { get; set; }

        public List<string> Tags { get; set; }


    }
}