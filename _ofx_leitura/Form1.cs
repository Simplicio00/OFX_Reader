using External_confs.geral;
using External_confs.objs;
using System.Security.Cryptography;
using System.Text;

namespace _ofx_leitura
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Text = openFileDialog1.FileName.Split('\\').Last();

                var _ofx = new ofx_st
                {
                    path = openFileDialog1.FileName
                };

                if (_ofx.path.ToUpper().EndsWith(".OFC"))
                {
                    ofc_st _ofc = new ofc_st();
                    _ofc.Transforma_Para_OFX(_ofx.path);
                }

                var lancamentos = _ofx.MontaLancamentos();
                var _string = new StringBuilder();

                _string.AppendLine("_________________________");
                _string.AppendLine("TOTAL LANCAMENTOS : " + lancamentos.Count);
                _string.AppendLine("TOTAL EM VALORES  : " + string.Format("{0:N2}", lancamentos.Sum(x => x.ValorLancamento)));
                _string.AppendLine("_________________________");

                foreach (var item in lancamentos)
                {
                    _string.AppendLine("_________________________");
                    _string.AppendLine("_________________________");
                    _string.AppendLine(item.Data.Date.ToString("dd/MM/yyyy"));
                    _string.AppendLine(item.ComplementoHistorico);
                    _string.AppendLine(string.Format("{0:N2}",item.ValorLancamento));
                    _string.AppendLine("_________________________");
                    _string.AppendLine("_________________________");
                }

                richTextBox1.Text = _string.ToString();
            }
        }

    }
}
