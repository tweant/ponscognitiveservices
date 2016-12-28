using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PonsCognitiveServicesWPF.Services;

namespace PonsCognitiveServicesWPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPonsDictionaryService _pons;
        private string _webAddress = "http://www.wp.pl/";
        private RelayCommand _getRestRequestCommand;
        private string _query = "Enter query...";

        public MainViewModel(IPonsDictionaryService ponsRestService)
        {
            _pons = ponsRestService;
        }


        public string WebAddress
        {
            get { return _webAddress; }
            set { Set(() => WebAddress, ref _webAddress, value); }
        }

        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                Set(() => Query, ref _query, value);
            }
        }


        public RelayCommand GetRestRequestCommand
        {
            get
            {
                return _getRestRequestCommand
                    ?? (_getRestRequestCommand = new RelayCommand(
                    async () =>
                    {
                        WebAddress= await Task.Run<string>(()=>_pons.GeneratePage(new Uri($"https://api.pons.com/v1/dictionary?q={Query.Replace(' ','+')}&l=depl")));
                    }));
            }
        }

        
    }
}