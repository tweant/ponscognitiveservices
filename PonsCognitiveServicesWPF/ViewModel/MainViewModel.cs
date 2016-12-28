using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.CognitiveServices.SpeechRecognition;
using PonsCognitiveServicesWPF.Helpers;
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
            get { return _query; }
            set { Set(() => Query, ref _query, value); }
        }


        public RelayCommand GetRestRequestCommand
        {
            get
            {
                return _getRestRequestCommand
                       ?? (_getRestRequestCommand = new RelayCommand(
                           async () =>
                           {
                               WebAddress =
                                   await Task.Run<string>(
                                       () =>
                                           _pons.GeneratePage(
                                               new Uri(
                                                   $"https://api.pons.com/v1/dictionary?q={Query.Replace(' ', '+')}&l=depl")));
                           }));
            }
        }

        #region CognitiveServices

        private RelayCommand _initializeRecording;
        private MicrophoneRecognitionClient _micClient;

        /// <summary>
        /// Gets the InitializeRecordingCommand.
        /// </summary>
        public RelayCommand InitializeRecordingCommand
        {
            get
            {
                return _initializeRecording
                       ?? (_initializeRecording = new RelayCommand(
                           () =>
                           {
                               Query = "Listening ...";

                               if (this._micClient == null)
                               {
                                   this.CreateMicrophoneRecoClient(); //THIS
                               }

                               this._micClient.StartMicAndRecognition(); //THIS
                           }));
            }
        }

        /// <summary>
        /// Creates a new microphone reco client without LUIS intent support.
        /// </summary>
        private void CreateMicrophoneRecoClient()
        {
            _micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                SpeechRecognitionMode.ShortPhrase,
                "de-DE",
                Constants.MicrosoftCognitiveServicesApiKey);

            // Event handlers for speech recognition results
            _micClient.OnMicrophoneStatus += this.OnMicrophoneStatus;
            _micClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            _micClient.OnResponseReceived += this.OnMicShortPhraseResponseReceivedHandler;
            _micClient.OnConversationError += this.OnConversationErrorHandler;
        }

        /// <summary>
        /// Called when the microphone status has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MicrophoneEventArgs"/> instance containing the event data.</param>
        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            if (e.Recording)
            {
                Query = ("Please start speaking.");
            }
        }

        /// <summary>
        /// Called when a partial response is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PartialSpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            Query = $"{e.PartialResult}";
        }

        /// <summary>
        /// Called when a final response is received;
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnMicShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            // we got the final result, so it we can end the mic reco.  No need to do this
            // for dataReco, since we already called endAudio() on it as soon as we were done
            // sending all the data.
            _micClient.EndMicAndRecognition();
            this.WriteResponseResult(e);
        }

        /// <summary>
        /// Writes the response result.
        /// </summary>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void WriteResponseResult(SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.Results.Length == 0)
            {
                Query="No phrase response is available.";
            }
            else
            {
                //this.WriteLine("********* Final n-BEST Results *********");
                //for (int i = 0; i < e.PhraseResponse.Results.Length; i++)
                //{
                //    this.WriteLine(
                //        "[{0}] Confidence={1}, Text=\"{2}\"",
                //        i,
                //        e.PhraseResponse.Results[i].Confidence,
                //        e.PhraseResponse.Results[i].DisplayText);
                //}
                Query = e.PhraseResponse.Results[0].DisplayText.Replace(".","");
                GetRestRequestCommand.Execute(null);

            }
        }

        /// <summary>
        /// Called when an error is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechErrorEventArgs"/> instance containing the event data.</param>
        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {

            Query = $"Error code: [{ e.SpeechErrorCode}] "+e.SpeechErrorText;
        }

        #endregion

        #region Window

        private RelayCommand _windowMove;

        /// <summary>
        /// Gets the WindowMove.
        /// </summary>
        public RelayCommand WindowMove
        {
            get
            {
                return _windowMove
                    ?? (_windowMove = new RelayCommand(
                    () =>
                    {
                        Window wnd = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                        wnd.DragMove();
                    }));
            }
        }

        #endregion
    }
}