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
        private readonly IPonsRestService _pons;
        private string _restResult;
        private RelayCommand _getRestRequestCommand;


        public MainViewModel(IPonsRestService ponsRestService)
        {
            _pons = ponsRestService;
        }


        public string RestResult
        {
            get { return _restResult; }
            set { Set(() => RestResult, ref _restResult, value); }
        }


        public RelayCommand GetRestRequestCommand
        {
            get
            {
                return _getRestRequestCommand
                    ?? (_getRestRequestCommand = new RelayCommand(
                    async () =>
                    {
                        RestResult= await _pons.GetRequest(new Uri("https://api.pons.com/v1/dictionary?q=to%20care%20for&l=deen"));
                    }));
            }
        }
    }
}