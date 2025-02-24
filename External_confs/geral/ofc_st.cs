using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External_confs.geral
{
    public class ofc_st
    {
        public void Transforma_Para_OFX(string path)
        {
            var lines = File.ReadAllLines(path);
            StringBuilder @sb = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                (bool, string) bloco = line.GetBlocoLancamento();

                if (!bloco.Item1 && !bloco.Item2.IsVazio())
                {
                    line += "</" + bloco.Item2 + ">";
                }

                @sb.AppendLine(line.Trim());
            }

            File.WriteAllText(path, sb.ToString());
        }
    }
}
