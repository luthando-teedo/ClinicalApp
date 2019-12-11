using Clinical.Model;
using Clinical.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinical.ViewModels
{
    public class RequestPageViewModel : ViewModelBase
    {
        private IUserProfile _userProfile;

        public ApptsClass appointment { get; set; }


        private ClientDetails _loggedInUser;
        public ClientDetails LoggedInUser
        {
            get { return _loggedInUser; }
            set { SetProperty(ref _loggedInUser, value); }
        }
        public RequestPageViewModel(INavigationService navigation, IUserProfile userProfile) : base(navigation)
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
