using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PonsCognitiveServices.Services;

namespace PonsCognitiveServices.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPonsDictionaryService _pons;
        private string _webAddress = "http://www.wp.pl/";
        private RelayCommand _getRestRequestCommand;


        public MainViewModel(IPonsDictionaryService ponsRestService)
        {
            _pons = ponsRestService;
        }


        public string WebAddress
        {
            get { return _webAddress; }
            set { Set(() => WebAddress, ref _webAddress, value); }
        }


        public RelayCommand GetRestRequestCommand
        {
            get
            {
                return _getRestRequestCommand
                    ?? (_getRestRequestCommand = new RelayCommand(
                    async () =>
                    {
                        WebAddress= await Task.Run<string>(()=>_pons.GeneratePage(new Uri("https://api.pons.com/v1/dictionary?q=to%20care%20for&l=deen")));
                    }));
            }
        }

        
    }
}