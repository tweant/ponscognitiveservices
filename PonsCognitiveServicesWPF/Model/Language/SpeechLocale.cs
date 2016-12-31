using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonsCognitiveServicesWPF.Model.Language
{
    public class SpeechLocale
    {
        public string Name { get; set; }
        /// <summary>
        /// Format xx-XX (e.g. en-UK)
        /// </summary>
        public string Code { get; set; }
        public bool IsMicrosoftTextToSpeechAvailable { get; set; }

        public SpeechLocale(string name, string code,bool isMicrosoftTextToSpeechAvailable)
        {
            Name = name;
            Code = code;
            IsMicrosoftTextToSpeechAvailable = isMicrosoftTextToSpeechAvailable;
        }
    }
}
