using Clinical.Model;
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
    public class RequestPageViewModel : ViewModelBase
    {
        private IUserProfile _userProfile;
       // private IDatabase _database;

        public ApptsClass appointment { get; set; }

        private DelegateCommand<ApptsClass> _requestCommand;
        public DelegateCommand<ApptsClass> RequestCommand =>
    _requestCommand ?? (_requestCommand = new DelegateCommand<ApptsClass>(ExecuteRequestCommand));

        private async void ExecuteRequestCommand(ApptsClass apptsClass)
        {
            
        }

        private ClientDetails _loggedInUser;
        private object _database;

        public ClientDetails LoggedInUser
        {
            get { return _loggedInUser; }
            set { SetProperty(ref _loggedInUser, value); }
        }
        public RequestPageViewModel(INavigationService navigation, IUserProfile userProfile, IDatabase database) : base(navigation)
        {
            _userProfile = userProfile;

            _database = database;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            LoggedInUser = _userProfile.GetLoggedInUser();
        }
    }
}
