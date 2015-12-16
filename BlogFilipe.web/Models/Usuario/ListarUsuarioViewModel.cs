using BlogFilipe.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFilipe.web.Models.Usuario
{
    public class ListarUsuarioViewModel
    {
        public List<DB.Classes.Usuario> Usuarios { get; set; }
        
    }
}