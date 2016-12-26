using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServices.Model
{
    public class Hit
    {
        public string Type;
        public bool IsOpenDict;
        public List<Rom> Roms;

        public Hit()
        {
            Roms = new List<Rom>();
        }
    }
}
