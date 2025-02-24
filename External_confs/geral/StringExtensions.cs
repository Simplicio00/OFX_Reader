using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static External_confs.geral.utils;

namespace External_confs.geral
{
    public static class StringExtensions
    {
        public static (bool, string) GetBlocoLancamento(this string content)
        {
            if (content.IsVazio()) { return (false, ""); }

            bool contem_fechamento = false;
            string bloco = string.Empty;

            Array values = Enum.GetValues(typeof(Blocos));

            foreach (Match match in utils.BlocoReg.Matches(content))
            {
                string name = match.Groups[2].Value;
                string closer = match.Groups[1].Value;

                foreach (var item in values)
                {
                    if (name.Equals(Enum.GetName(typeof(Blocos), item)))
                    {
                        bloco = name;
                        break;
                    }
                }

                if (!closer.IsVazio())
                {
                    contem_fechamento = true;
                }
            }

            return (contem_fechamento, bloco);
        }
        public static string CorrigeCaracteresEspeciais(this string content)
        {
            string _line = string.Empty;

            char[] caracteres = content.ToCharArray();

            foreach (var item in caracteres)
            {
                var caractere_especial = CaracteresEspeciais.FirstOrDefault(x => x.Item1.Equals(item));

                if (!caractere_especial.Item2.IsVazio())
                {
                    _line += caractere_especial.Item2;
                    continue;
                }

                _line += item;
            }

            return _line;
        }
        public static bool IsVazio(this string content) => string.IsNullOrEmpty(content);
        public static string Remover(this string content, string[] conteudo_a_remover)
        {
            foreach (var item in conteudo_a_remover)
            {
                content = content.Trim().Contains(item) ? content.Trim().Replace(item, "") : content.Trim();
            }

            return content;
        }
    }
}
