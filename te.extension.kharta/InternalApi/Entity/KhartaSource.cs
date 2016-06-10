using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using kcgModels = kharta.coria.graphica.Models;
using Telligent.Evolution.Extensibility.Api.Entities.Version1;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Content.Version1;

namespace te.extension.kharta.InternalApi
{
    internal class KhartaSource: kcgModels.Source
    {
        internal string HtmlName(string target)
        {
            // var _kcgName = new KhartaOntology();
            string name =  Name;
            if (target == "web" || target == "email" || target == "raw")
            {
                //html s/b applied, translated, etc.. unless raw
            }
            return name;
            //    throw new NotImplementedException();
        }
    }
}
