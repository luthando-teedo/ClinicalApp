using Clinical.Messages.Security;
using Clinical.Model.Security;
using Clinical.Services.Interface;
using Clinical.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinical.ViewModels
{
    public class loginPageViewModel : ViewModelBase
    {
        public ClientDetails LoginClient { get; set; }
        public List<ClientDetails> MyClientList { get; set; }
        public bool passwExist { get; set; }
        private IPageDialogService _pageDialogService;
        private IDatabase _database;
        private ISecurityService _securityService;
        private IEventAggregator _eventAggregator;
        private IUserProfile _userProfile;


        private DelegateCommand _loginCommand;
        private DelegateCommand _SignUpCommand;

        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));

        public ClientDetails userProfileCopy { get; set; }

        public DelegateCommand SignUpCommand =>
            _SignUpCommand ?? (_SignUpCommand = new DelegateCommand(ExecuteSignUpCommand));
        public loginPageViewModel(INavigationService navigation, ISecurityService securityService, IEventAggregator eventAggregator, IPageDialogService pageDialogService, IDatabase database, IUserProfile userProfile) : base (navigation)
        {
            _securityService = securityService;
            _database = database;
            _eventAggregator = eventAggregator;
            _userProfile = userProfile;
            _pageDialogService = pageDialogService;

            var LoginInfor = new ClientDetails();
            LoginClient = LoginInfor;

            var ClientDetails = new ClientDetails();
            LoginClient = ClientDetails;

        }

        async void ExecuteLoginCommand()
        {

            var clientDetail = await _database.GetClientDetailsByUserName(LoginClient.UserName);


            if (LoginClient.UserName == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Username is required!", "ok");
            }
            else if (LoginClient.UserName != clientDetail.UserName || LoginClient.Passrd != clientDetail.Passrd)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Wrong Password or Username!", "ok");
            }
            else if (LoginClient.UserName != clientDetail.UserName && LoginClient.Passrd != clientDetail.Passrd)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Wrong Password or Username!", "ok");
            }
            else if (LoginClient.Passrd == null)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Password is required!", "ok");
            }
            else
            {
                    if (clientDetail.Passrd == LoginClient.Passrd)
                    {
                        passwExist = true;

                        var loginResult = _securityService.LogIn("Test User", "Password");

                        // I may have gotten a user profile somewhere..  Use whatever your app does
           
                        if (loginResult)
                        {

                            _userProfile.SetLoggedInUser(clientDetail);

                            _eventAggregator.GetEvent<LoginMessage>().Publish();
                        }


                        await NavigationService.NavigateAsync("MasterDetail/NavigationPage/DashBoardPage", useModalNavigation: true);
                        return;
                    }
                    else
                    {
                        passwExist = false;
                    }
                

                if (passwExist == false)
                {
                    await _pageDialogService.DisplayAlertAsync("Alert", "Incorrect Password Please try again", "ok");
                }
            }                     
           
        }

        void ExecuteSignUpCommand()
        {
            NavigationService.NavigateAsync("RegPage");
        }
    }
}
