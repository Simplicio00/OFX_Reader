using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External_confs.objs
{
    public class lancamento
    {
        public DateTime Data { get; set; }
        public string? ComplementoHistorico { get; set; }
        public float ValorLancamento { get; set; }
    }
}
