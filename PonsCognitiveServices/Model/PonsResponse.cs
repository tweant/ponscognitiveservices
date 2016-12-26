using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PonsCognitiveServices.Helpers;

namespace PonsCognitiveServices.Model
{
    public class PonsResponse
    {
        public PonsLanguage Language;
        public List<Hit> Hits;

        public PonsResponse()
        {
            Hits = new List<Hit>();
        }

    }
}
