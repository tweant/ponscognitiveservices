using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PonsCognitiveServicesWPF.Model;
using PonsCognitiveServicesWPF.Model.Language;

namespace PonsCognitiveServicesWPF.Services
{
    public class PonsDictionaryService : IPonsDictionaryService
    {
        private readonly IPonsRestService _ponsRestService;

        public PonsDictionaryService(IPonsRestService ponsRestService)
        {
            _ponsRestService = ponsRestService;
        }

        public async Task<string> GeneratePage(Uri uri)
        {
            var stringRes = await _ponsRestService.GetRequest(uri);
            var textReader = new StringReader(stringRes);
            var jsonReader = new JsonTextReader(textReader);
            var jsonConverter = new JsonSerializer();
            List<PonsResponse> obj = jsonConverter.Deserialize<List<PonsResponse>>(jsonReader);

            StringBuilder sb = new StringBuilder();

            var pons = obj[0];
            sb.Append("<!DOCTYPE html>");
            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<meta charset=\"utf-8\" />");
            sb.Append("<style>body {\r\n    margin: -1px 0px 0px 0px;\r\n}\r\n.results {\r\n    font-family: Arial,\"Arial Unicode MS\",sans-serif;\r\n    font-size: 14px;\r\n    line-height: 20px;\r\n}\r\n.entry {\r\n    color:rgb(51, 51, 51);\r\n    display:block;\r\n    margin-bottom: 40px;\r\n}\r\n.romhead.opened {\r\n    background-color:rgb(238, 238, 238);\r\nborder-bottom-color:rgb(0, 123, 48);\r\nborder-bottom-style:solid;\r\nborder-bottom-width:0px;\r\nborder-left-color:rgb(0, 123, 48);\r\nborder-left-style:solid;\r\nborder-left-width:0px;\r\nborder-right-color:rgb(0, 123, 48);\r\nborder-right-style:solid;\r\nborder-right-width:0px;\r\nborder-top-color:rgb(0, 123, 48);\r\nborder-top-style:solid;\r\nborder-top-width:1px;\r\ncolor:rgb(51, 51, 51);\r\ndisplay:block;\r\npadding-bottom:12px;\r\npadding-left:10px;\r\npadding-top:12px;\r\nposition:relative;\r\ntext-size-adjust:100%;\r\n}\r\n.romhead.opened h2 {\r\n    color:rgb(51, 51, 51);\r\ndisplay:block;\r\nfont-family:Arial, \"Arial Unicode MS\", sans-serif;\r\nfont-size:17.5px;\r\nfont-weight:normal;\r\nline-height:20px;\r\nmargin-bottom:0px;\r\nmargin-left:0px;\r\nmargin-right:0px;\r\nmargin-top:0px;\r\ntext-rendering:optimizeLegibility;\r\ntext-size-adjust:100%;\r\n-webkit-margin-after:0px;\r\n-webkit-margin-before:0px;\r\n-webkit-margin-end:0px;\r\n-webkit-margin-start:0px;\r\n}\r\n.romhead.opened h2 span.flexion {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    text-rendering: optimizeLegibility;\r\n}\r\n.romhead.opened h2 span.phonetics {\r\n    color:rgb(128, 128, 128);\r\n    font-style: normal;\r\n    font-weight: normal;\r\n}\r\n.romhead.opened h2 span.phonetics span.info {\r\n    font-style: italic;\r\n}\r\n.romhead.opened h2 span.wordclass {\r\n    color:rgb(128, 128, 128);\r\n    font-style: normal;\r\n    font-weight: normal;\r\n}\r\n.romhead.opened h2 span.genus {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    font-weight: normal;\r\n}\r\n.translations.opened h3 {\r\n    background-color:rgb(238, 238, 238);\r\nborder-bottom-color:rgb(213, 213, 213);\r\nborder-bottom-style:solid;\r\nborder-bottom-width:0px;\r\nborder-left-color:rgb(213, 213, 213);\r\nborder-left-style:solid;\r\nborder-left-width:0px;\r\nborder-right-color:rgb(213, 213, 213);\r\nborder-right-style:solid;\r\nborder-right-width:0px;\r\nborder-top-color:rgb(213, 213, 213);\r\nborder-top-style:solid;\r\nborder-top-width:1px;\r\ncolor:rgb(51, 51, 51);\r\ndisplay:block;\r\nfont-family:Arial, \"Arial Unicode MS\", sans-serif;\r\nfont-size:14px;\r\nfont-style:italic;\r\nfont-weight:normal;\r\nmargin-bottom:0px;\r\nmargin-left:0px;\r\nmargin-right:0px;\r\nmargin-top:0px;\r\npadding-bottom:4px;\r\npadding-left:10px;\r\npadding-right:10px;\r\npadding-top:4px;\r\ntext-rendering:optimizeLegibility;\r\n}\r\n.translations.entity {\r\n    margin-bottom: 25px;\r\n}\r\ndl.dl-horizontal.kne.first {\r\n    border-top-color:rgb(213, 213, 213);\r\nborder-top-style:solid;\r\nborder-top-width:1px;\r\nmargin:0;\r\nline-height: 20px;\r\nwidth: 100%;\r\noverflow: hidden;\r\n}\r\ndt {\r\n    float: left;\r\n    overflow-x: visible;\r\n    overflow-y: visible;\r\n    position: relative;\r\n    text-align: left;\r\n    text-overflow: clip;\r\n    white-space: normal;\r\n    width: 50%;\r\n    margin: 0;\r\n    padding: 0;\r\n\r\n}\r\n.dt-inner.left {\r\n    padding-bottom: 4px;\r\n    padding-left: 20px;\r\n    padding-right: 5px;\r\n    padding-top: 4px;\r\n}\r\n.dt-inner.right {\r\n    padding-bottom: 4px;\r\n    padding-right: 20px;\r\n    padding-left: 5px;\r\n    padding-top: 4px;\r\n}\r\ndd {\r\n    width: 50%;\r\n    float: left;\r\n    margin: 0;\r\n    padding: 0;\r\n    font-weight: bold;\r\n}\r\nspan.genus {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    font-weight: normal;\r\n}\r\nspan.tempus {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    font-weight: normal;\r\n}\r\nspan.rhetoric {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    font-weight: normal;\r\n}\r\nspan.example {\r\n    font-style: italic;\r\n}\r\nspan.topic {\r\n    color:rgb(128, 128, 128);\r\n    font-style: normal;\r\n    font-weight: normal;\r\n    font-size: 11.5px;\r\n}\r\nspan.case {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    font-weight: normal;\r\n}\r\nspan.style {\r\n    color:rgb(128, 128, 128);\r\n    font-style: italic;\r\n    font-weight: normal;\r\n}\r\nspan.or {\r\n    font-style: italic;\r\n}\r\n</style>");
            sb.Append("<title>PONS Translation</title>");
            sb.Append("</head>");
            sb.Append("<body>");
            sb.Append("<div class=\"results\">");
            foreach (Hit hit in pons.Hits)
            {
                sb.Append("<div class=\"entry\">");
                foreach (Rom rom in hit.Roms)
                {
                    sb.Append("<div class=\"rom first\">");
                    sb.Append("<div class=\"romhead opened\">");
                    sb.Append("<h2>");
                    sb.Append(rom.HeadwordFull);
                    sb.Append("</h2>");
                    sb.Append("</div>");
                    sb.Append("<div class=\"translations opened\">");
                    foreach (Arab arab in rom.Arabs)
                    {
                        sb.Append("<div class=\"translations entity\">");
                        if (arab.Header != "")
                        {
                            sb.Append("<h3 class>");
                            sb.Append(arab.Header);
                            sb.Append("</h3>");
                        }
                        foreach (TranslationContext translation in arab.Translations)
                        {
                            sb.Append("<dl class=\"dl-horizontal kne first\">");
                            sb.Append("<dt>");
                            sb.Append("<div class=\"dt-inner left\">");
                            sb.Append(translation.Source);
                            sb.Append("</div>");
                            sb.Append("</dt>");
                            sb.Append("<dd>");
                            sb.Append("<div class=\"dt-inner right\">");
                            sb.Append(translation.Target);
                            sb.Append("</div>");
                            sb.Append("</dd>");
                            sb.Append("</dl>");
                        }
                        sb.Append("</div>");
                    }
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
            }
            sb.Append("</div>");
            sb.Append("</body>");
            sb.Append("</html>");

            #region SavingToFile
            //string filename = ApplicationData.Current.LocalFolder.Path + "\\test.html";
            //using (FileStream fs = new FileStream(filename, FileMode.Create))
            //{
            //    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
            //    {
            //        w.Write(sb.ToString());
            //    }
            //}
            #endregion


            return sb.ToString();
        }

        public async Task<string> LookForQuery(string languageFromCode, string languageToCode, string query)
        {
            var uri = new Uri($"https://api.pons.com/v1/dictionary?q={query.Replace(' ', '+')}&l={languageFromCode}{languageToCode}");
            return await GeneratePage(uri);
        }

        public async Task<string> LookForQuery(Translation translation, string query)
        {
            var uri = new Uri($"https://api.pons.com/v1/dictionary?q={query.Replace(' ', '+')}&l={translation.DictionaryCode}&lf={translation.LanguageFrom.PonsCode}");
            return await GeneratePage(uri);
        }
    }
}