using Clinical.Model.Security;
using Clinical.Services.Interface;
using Clinical.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinical.ViewModels
{
    public class DashBoardPageViewModel : ViewModelBase
    {
        private IUserProfile _userProfile;

        private ClientDetails _loggedInUser;
        public ClientDetails LoggedInUser
        {
            get { return _loggedInUser; }
            set { SetProperty(ref _loggedInUser, value); }
        }

        
        private DelegateCommand _ViewFolderCommand;

        public DelegateCommand ViewFolderCommand =>
            _ViewFolderCommand ?? (_ViewFolderCommand = new DelegateCommand(ExecuteViewFolderCommand));

        private async void ExecuteViewFolderCommand()
        {
            await NavigationService.NavigateAsync("MasterDetail/NavigationPage/RequestPage", useModalNavigation: true);
        }

        public DashBoardPageViewModel(INavigationService navigation, IUserProfile userProfile) : base(navigation)
        {
            _userProfile = userProfile;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            LoggedInUser = _userProfile.GetLoggedInUser();
        }

    }
}
