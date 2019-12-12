using Clinical.Messages.Security;
using Clinical.Model.Security;
using Clinical.Services.Interface;
using Clinical.Services.Interfaces;
using Plugin.Media;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Clinical.ViewModels
{
    public class RegPageViewModel : ViewModelBase
    {
        public string  filePath { get; set; }
        private IPageDialogService _dialogService;
        private ISecurityService _securityService;
        private IEventAggregator _eventAggregator;
        private IDatabase _database;
        private IPageDialogService _pageDialogService;
        public ClientDetails MyClient { get; set; }


        private DelegateCommand<ClientDetails> _submitCommand;
        public DelegateCommand<ClientDetails> SubmitCommand =>
        _submitCommand ?? (_submitCommand = new DelegateCommand<ClientDetails>(ExecuteSubmitCommand));

        private DelegateCommand _pickPhotoCommand;
        public DelegateCommand PickPhotoCommand =>
            _pickPhotoCommand ?? (_pickPhotoCommand = new DelegateCommand(ExecutePickPhotoCommand));

        private ImageSource _pickImage;

        public ImageSource PickImage
        {
            get { return _pickImage; }
            set { SetProperty(ref _pickImage, value); }
        }


        private async void ExecutePickPhotoCommand()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {

            });


            if (file == null)
                return;
            //this is getting the file path
            filePath = file.Path;
            await _dialogService.DisplayAlertAsync("File Location", filePath, "OK");



            PickImage = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

        }

        private async void ExecuteSubmitCommand(ClientDetails clientDetails)
        {
            var image = filePath;
            MyClient.Image = image;
            var clientDetail = await _database.GetClientDetailsByUserName(MyClient.IdNumber);

            if (MyClient.IdNumber == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "ID Number is required!", "ok");
            }
            else if (MyClient.FullName == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "FullName is required!", "ok");
            }
            else if (MyClient.Passrd == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Password is required!", "ok");

            }
            else if (MyClient.PostalAddress == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Address is required!", "ok");
            }
            else if (MyClient.UserName == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Username is required!", "ok");
            }
            else if (MyClient.PhoneNumber == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Phone number is required!", "ok");
            }
            else if (MyClient.EmailAddress == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Email address is required!", "ok");
            }
            else
            {

                await _database.SaveItemAsync(MyClient);

                var loginResult = _securityService.LogIn("Test User", "Password");

                // I may have gotten a user profile somewhere..  Use whatever your app does
                var userProfile = new UserProfile();

                if (loginResult)
                {
                    _eventAggregator.GetEvent<LoginMessage>().Publish();
                }
                await NavigationService.NavigateAsync("MasterDetail/NavigationPage/loginPage", useModalNavigation: true);

                
            }
        }

        public RegPageViewModel(INavigationService NavigationService, IPageDialogService pageDialogService, ISecurityService securityService, IEventAggregator eventAggregator, IDatabase database) : base(NavigationService)
        {
            _securityService = securityService;
            _database = database;
            _eventAggregator = eventAggregator;

            _dialogService = pageDialogService;

            _pageDialogService = pageDialogService;
            var ClientDetails = new ClientDetails();
            MyClient = ClientDetails;
            
        }

        
    }
}
