using Clinical.Services.Interface;
using Clinical.Services.Interfaces;
using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Clinical.ViewModels
{
    public class EditPageViewModel : ViewModelBase
    {
        private IUserProfile _userProfile;
        private IDatabase _database;
        public string filePath { get; set; }
        //public string newImage { get; set; }


        private IPageDialogService _dialogService;

        private ClientDetails _loggedInUser;
        public ClientDetails LoggedInUser
        {
            get { return _loggedInUser; }
            set { SetProperty(ref _loggedInUser, value); }
        }

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

            //var image = newImage;
            //filePath = image;
            //image = LoggedInUser.Image;


            PickImage = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

        }

        private async void ExecuteSubmitCommand(ClientDetails clientDetails)
        {
            var image = filePath;
            if (image == null)
            {
                LoggedInUser.Image = LoggedInUser.Image;
            }else 
            {
                LoggedInUser.Image = image;
            }
            
            var SaveChange = await _database.GetClientDetailsByUserName(LoggedInUser.IdNumber);

            await _database.SaveItemAsync(LoggedInUser);

            await NavigationService.NavigateAsync("MasterDetail/NavigationPage/DashBoardPage", useModalNavigation: true);
        }
            public EditPageViewModel(INavigationService navigation, IUserProfile userProfile, IPageDialogService pageDialogService, IDatabase database) : base(navigation)
        {
            _userProfile = userProfile;
            _dialogService = pageDialogService;

            _database = database;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            LoggedInUser = _userProfile.GetLoggedInUser();
        }
    }
}
