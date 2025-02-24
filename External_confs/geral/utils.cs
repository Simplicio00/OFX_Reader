using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace External_confs.geral
{
    public class utils
    {
        public static Regex BlocoReg { get => new Regex(@"<(\/){0,1}([A-Z]{1,20})>"); }
        public enum Blocos
        {
            DTD = 1,
            CPAGE = 2,
            BANKID = 3,
            ACCTID = 4,
            ACCTTYPE = 5,
            TRNTYPE = 6,
            DTSTART = 7,
            DTEND = 8,
            LEDGER = 9,
            DTPOSTED = 10,
            TRNAMT = 11,
            FITID = 12,
            CHKNUM = 13,
            MEMO = 14,
            NAME = 15
        }

        /// <summary>
        /// Esses caracteres podem causar problemas ao percorrerem o leitor do XML.
        /// REF: https://www.ibm.com/docs/pt-br/tsafm/4.1.0?topic=reference-xml-special-characters
        /// </summary>
        public static List<(char, string)> CaracteresEspeciais
        {
            get => new List<(char, string)>
            {
                ('&',"&amp;"),
                ('>',"&lt;"),
                ('<',"&gt;"),
                ('"',"&quot;"),
            };
        }

    }
}
