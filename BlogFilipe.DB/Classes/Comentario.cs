﻿using BlogFilipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFilipe.DB.Classes
{
    public class Comentario : ClasseBase
    {
        public string Descricao { get; set; }
        public bool AdmPost { get; set; }
        public string Email { get; set; }
        public string PaginaWeb { get; set; }
        public string Nome { get; set; }
        public int IdPost { get; set; }
        public DateTime DataHora { get; set; }

        public virtual Post Post { get; set; }
    }
}
