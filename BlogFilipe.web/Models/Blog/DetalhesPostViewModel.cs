using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogFilipe.web.Models.Blog
{
    public class DetalhesPostViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Resumo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime HoraPublicacao { get; set; }

        public List<string> Tags { get; set; }

        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage ="O campo nome deve possuir no máximo {1} caracteres!")]
        [Required(ErrorMessage ="O campo nome é obrigtório!")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage ="O campo E-mail deve possuir no máximo {1} caracteres!")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descricao")]
        [Required(ErrorMessage ="O campo descrição é obrigatório!")]
        public string ComentarioDescricao { get; set; }

        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage ="O campo página web deve possuir no máximo {1} caracteres!")]
        public string ComentarioPaginaWeb { get; set; }
    }
}