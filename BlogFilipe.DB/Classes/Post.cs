using BlogFilipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFilipe.DB.Classes
{
    public class Post : ClasseBase
    {
        public string Autor { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Descricao { get; set; }
        public string Resumo { get; set; }
        public string Titulo { get; set; }
        public bool Visivel { get; set; }

        public virtual IList<Comentario> Comentario { get; set; }
        public virtual IList<Arquivo> Arquivo { get; set; }
        public virtual IList<Imagem> Imagem { get; set; }
        public virtual IList<Visita> Visita { get; set; }
        public virtual IList<TagPost> TagsPost { get; set; }
    }
}
