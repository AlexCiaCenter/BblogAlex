using BlogAlex.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAlex.DB.Classes
{
    public class TagPost : ClasseBase
    {
        public string IdTag { get; set; }
        public int IdPost { get; set; }

        public virtual Post Post { get; set; }
        public virtual TagClass TagClass { get; set; }
    }
}
