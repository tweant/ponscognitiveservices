using System.Windows.Navigation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using PonsCognitiveServicesWPF.Helpers;
using PonsCognitiveServicesWPF.Services;

//using PonsCognitiveServices.Model;

namespace PonsCognitiveServicesWPF.ViewModel
{
    public class ViewModelLocator
    {
        public const string SecondPageKey = "SecondPage";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var pons = new PonsRestService();
            pons.SetSecretKey(Constants.PonsApiSecret);
            SimpleIoc.Default.Register<IPonsRestService>( ()=>pons);
            SimpleIoc.Default.Register<IPonsDictionaryService, PonsDictionaryService>();

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            //}
            //else
            //{
            //    SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            SimpleIoc.Default.Register<MainViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}
