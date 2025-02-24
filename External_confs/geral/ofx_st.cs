using External_confs.objs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using External_confs.attributes;
using static External_confs.attributes.OfxAttribute;

namespace External_confs.geral
{
    public class ofx_st
    {
        public required string path;
        private List<lancamento> _lanc;

        private void GetHistorico()
        {
            var list = new OfxAttribute("MEMO", TipoRetorno.ListaInteira, path)._Get_;

            list.ForEach(x =>
            {
                _lanc[list.IndexOf(x)].ComplementoHistorico = x.valor.CorrigeCaracteresEspeciais();
            });
        }

        private void GetValor()
        {
            var list = new OfxAttribute("TRNAMT", TipoRetorno.ListaInteira, path)._Get_;

            list.ForEach(x =>
            {
                _lanc[list.IndexOf(x)].ValorLancamento = float.Parse(x.valor.Replace(".", ","));
            });
        }

        private void GetData()
        {
            var list = new OfxAttribute("DTPOSTED", TipoRetorno.ListaInteira, path)._Get_;
            list.ForEach(x =>
            {
                DateTime.TryParseExact(x.valor.Substring(0, 8), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dta);
                _lanc[list.IndexOf(x)].Data = dta;
            });
        }

        private void GetTotalReg()
        {
            new OfxAttribute("STMTTRN", TipoRetorno.ListaInteira, path)._Get_
            .ForEach(x =>
            {
                _lanc.Add(new lancamento { });
            });
        }

        private void RemoveSujeira()
        {
            try
            {
                var text = File.ReadAllText(path);
                var index = text.IndexOf("<");
                text = text.Substring(index);
                File.WriteAllText(path, text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CorrigeFechamentoBlocos()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var lines = File.ReadLines(path).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                (bool, string) bloco = line.GetBlocoLancamento();

                if (!bloco.Item2.IsVazio())
                {
                    if (!bloco.Item1)
                    {
                        string conteudo_linha = line.Trim().Substring(bloco.Item2.Length + 2).CorrigeCaracteresEspeciais();
                        line = "<" + bloco.Item2 + ">" + conteudo_linha + "</" + bloco.Item2 + ">";
                    }
                    else
                    {
                        string conteudo_linha = line.Trim().Substring(bloco.Item2.Length + 2);
                        conteudo_linha = conteudo_linha.Substring(0, conteudo_linha.IndexOf("<"));
                        line = "<" + bloco.Item2 + ">" + conteudo_linha.CorrigeCaracteresEspeciais() + "</" + bloco.Item2 + ">";
                    }
                }

                stringBuilder.AppendLine(line);
            }

            File.WriteAllText(path, stringBuilder.ToString());
        }


        public List<lancamento> MontaLancamentos()
        {
            _lanc = new List<lancamento>();

            if (path.ToUpper().EndsWith("OFX"))
            {
                CorrigeFechamentoBlocos();
            }

            RemoveSujeira();
            GetTotalReg();
            GetHistorico();
            GetValor();
            GetData();

            return _lanc;
        }

    }
}
