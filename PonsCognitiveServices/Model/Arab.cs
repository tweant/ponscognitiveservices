using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServices.Model
{
    /// <summary>
    /// An arab contains a header (arabic numeral) and stands for a specific meaning of the
    /// headword described in the rom. For example, the "substantive"-rom of "cut" has 12 arabs.
    /// </summary>
    public class Arab
    {
        public string Header;
        public List<Translation> Translations;

        public Arab()
        {
            Translations = new List<Translation>();
        }
    }
}
