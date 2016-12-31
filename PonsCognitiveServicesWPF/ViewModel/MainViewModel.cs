using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.CognitiveServices.SpeechRecognition;
using PonsCognitiveServicesWPF.Helpers;
using PonsCognitiveServicesWPF.Services;
using PonsCognitiveServicesWPF.Model.Language;
using Language = PonsCognitiveServicesWPF.Model.Language.Language;

namespace PonsCognitiveServicesWPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPonsDictionaryService _pons;
        private readonly ILanguagesTreeService _languageTree;
        private string _webAddress = "Welcome page";
        private RelayCommand _getRestRequestCommand;
        private string _query = "Enter query...";

        public MainViewModel(IPonsDictionaryService ponsRestService, ILanguagesTreeService languageTreeService)
        {
            _pons = ponsRestService;
            _languageTree = languageTreeService;

            //
            LanguageFrom = _languageTree.LanguageTree.Find(x => x.PonsCode.Equals("de"));
            LanguageTranslation = LanguageFrom.TranslatesTo.Find(x => x.LanguageTo.PonsCode.Equals("pl"));
            //LanguageTo = _languageTree.LanguageTree.Find(x => x.PonsCode.Equals("pl"));
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
                               try
                               {
                                   WebAddress = await _pons.LookForQuery(LanguageTranslation, Query);
                               }
                               catch (NullReferenceException)
                               {
                                   WebAddress = "NullReference Exception";
                               }
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
            if (LanguageTranslation != null)
            {
                _micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                    SpeechRecognitionMode.ShortPhrase,
                    LanguageTranslation.LanguageFrom.CurrentLocale.Code,
                    Constants.MicrosoftCognitiveServicesApiKey);

                // Event handlers for speech recognition results
                _micClient.OnMicrophoneStatus += this.OnMicrophoneStatus;
                _micClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
                _micClient.OnResponseReceived += this.OnMicShortPhraseResponseReceivedHandler;
                _micClient.OnConversationError += this.OnConversationErrorHandler;
            }
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
                Query = "No phrase response is available.";
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
                Query = e.PhraseResponse.Results[0].DisplayText.Replace(".", "");
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
            Query = $"Error code: [{e.SpeechErrorCode}] " + e.SpeechErrorText;
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
                               Window wnd = Application.Current.Windows.OfType<Window>()
                                   .SingleOrDefault(x => x.IsActive);
                               wnd?.DragMove();
                           }));
            }
        }

        private RelayCommand _minimizeWindow;
        private RelayCommand _maximizeWindow;
        private RelayCommand _closeWindow;

        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _closeWindow
                       ?? (_closeWindow = new RelayCommand(
                           () => { Environment.Exit(0); }));
            }
        }

        public RelayCommand MaximizeWindowCommand
        {
            get
            {
                return _maximizeWindow
                       ?? (_maximizeWindow = new RelayCommand(
                           () =>
                           {
                               Window wnd = Application.Current.Windows.OfType<Window>()
                                   .SingleOrDefault(x => x.IsActive);
                               if (wnd != null)
                               {
                                   wnd.WindowState = wnd.WindowState == WindowState.Normal
                                       ? WindowState.Maximized
                                       : WindowState.Normal;
                               }
                           }));
            }
        }

        public RelayCommand MinimizeWindowCommand
        {
            get
            {
                return _minimizeWindow
                       ?? (_minimizeWindow = new RelayCommand(
                           () =>
                           {
                               Window wnd = Application.Current.Windows.OfType<Window>()
                                   .SingleOrDefault(x => x.IsActive);
                               if (wnd != null) wnd.WindowState = WindowState.Minimized;
                           }));
            }
        }

        #endregion

        #region SettingsPanel

        private bool _isSettingsPanelShown = false;
        private string _settingsButtonContext = ((char) 0xE09D).ToString();
        private RelayCommand _settingsPanelToggle;
        private Language _languageFrom;
        private Translation _languageTo;
        private RelayCommand _reverseLanguages;
        private SpeechLocale _selectedLocale;

        public string SettingsButtonContext
        {
            get { return _settingsButtonContext; }
            set { Set(() => SettingsButtonContext, ref _settingsButtonContext, value); }
        }

        public bool IsSettingsPanelShown
        {
            get { return _isSettingsPanelShown; }
            set { Set(() => IsSettingsPanelShown, ref _isSettingsPanelShown, value); }
        }

        public RelayCommand SettingsPanelToggle
        {
            get
            {
                return _settingsPanelToggle
                       ?? (_settingsPanelToggle = new RelayCommand(
                           () =>
                           {
                               if (IsSettingsPanelShown)
                               {
                                   IsSettingsPanelShown = false;
                                   SettingsButtonContext = ((char) 0xE09D).ToString();
                               }
                               else
                               {
                                   IsSettingsPanelShown = true;
                                   SettingsButtonContext = ((char) 0xE09C).ToString();
                               }
                           }));
            }
        }

        public Language LanguageFrom
        {
            get { return _languageFrom; }
            set
            {
                if (value.IsPonsAvailable && LanguageTranslation != null)
                {
                    //ZAMIANA
                    if (value == LanguageTranslation.LanguageTo)
                    {
                        LanguageTranslation =
                            LanguagesTree.Find(x => x == value).TranslatesTo.Find(x => x.LanguageTo == LanguageFrom);
                    }
                    else
                    {
                        Translation newTranslation =
                            value.TranslatesTo.Find(x => x.LanguageTo == LanguageTranslation.LanguageTo);

                        LanguageTranslation = newTranslation == null ? value.TranslatesTo[0] : newTranslation;


                        //ZOSTAJE
                    }
                    //if (LanguageTranslation == null) LanguageTranslation = value.TranslatesTo[0];
                    //Translation res = LanguageTranslation.LanguageTo.TranslatesTo.Find(x=>x.LanguageTo==LanguageFrom); //LanguageFrom
                    //LanguageTranslation = res!=null
                    //    ? res
                    //    : value.TranslatesTo[0];
                    //if (!(value.TranslatesTo.FindIndex(x=>x.LanguageTo==LanguageTranslation.LanguageTo)>=0)) //LanguageTo
                    //    LanguageTranslation = value.TranslatesTo[0];
                    ////Updating SelectedLocale
                }
                Set(() => LanguageFrom, ref _languageFrom, value);

                if (LanguageFrom.IsMicrosoftSpeechRecognitionAvailable)
                    this.CreateMicrophoneRecoClient();
                SelectedLocale = value.CurrentLocale;
            }
        }

        public Translation LanguageTranslation
        {
            get { return _languageTo; }
            set { Set(() => LanguageTranslation, ref _languageTo, value); }
        }

        public List<Language> LanguagesTree => _languageTree.LanguageTree;


        public SpeechLocale SelectedLocale
        {
            get { return _selectedLocale; }
            set
            {
                if (value != null)
                    _languageTree.SetLanguageLocale(LanguageFrom.PonsCode, value.Code);
                //TODO try catch
                Set(() => SelectedLocale, ref _selectedLocale, value);
            }
        }


        public RelayCommand ReverseLanguages
        {
            get
            {
                return _reverseLanguages
                       ?? (_reverseLanguages = new RelayCommand(
                           () =>
                           {
                               if (LanguageTranslation != null)
                               {
                                   LanguageFrom = LanguageTranslation.LanguageTo;
                               }
                               else
                                   throw new Exception("ERROR: 0001");
                           }));
            }
        }

        #endregion
    }
}