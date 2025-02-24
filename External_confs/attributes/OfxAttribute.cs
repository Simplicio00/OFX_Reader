using External_confs.objs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace External_confs.attributes
{
    //[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    //public class OfxAttribute : Attribute
    public class OfxAttribute
    {
        public enum TipoRetorno
        {
            ListaInteira = 0
        }

        string path;
        XmlDocument xmlDoc;
        TipoRetorno Tipo;
        string Name;

        public OfxAttribute(string name, TipoRetorno tipo, string path)
        {
            xmlDoc = new XmlDocument();

            this.Name = name;
            this.Tipo = tipo;
            this.path = path;
        }

        public virtual List<ofx_object> _Get_
        {
            get => Faz();
        }

        private List<ofx_object> Faz()
        {
            var list = new List<ofx_object>();
            xmlDoc.Load(path);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName(Name);
            list = new List<ofx_object>(nodeList.Count);

            foreach (XmlNode item in nodeList)
            {
                list.Add(new ofx_object { coluna = item.Name, valor = item.InnerText });
            }

            return list;
        }

    }
}
