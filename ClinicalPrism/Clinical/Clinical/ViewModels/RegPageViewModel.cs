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
    public class RegPageViewModel : ViewModelBase
    {
        private ISecurityService _securityService;
        private IEventAggregator _eventAggregator;
        private IDatabase _database;
        private IPageDialogService _pageDialogService;
        public ClientDetails MyClient { get; set; }


        private DelegateCommand<ClientDetails> _submitCommand;
        public DelegateCommand<ClientDetails> SubmitCommand =>
    _submitCommand ?? (_submitCommand = new DelegateCommand<ClientDetails>(ExecuteSubmitCommand));

        private async void ExecuteSubmitCommand(ClientDetails clientDetails)
        {
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
                await NavigationService.NavigateAsync("MasterDetail/NavigationPage/DashBoardPage", useModalNavigation: true);

                
            }
        }

        public RegPageViewModel(INavigationService NavigationService, IPageDialogService pageDialogService, ISecurityService securityService, IEventAggregator eventAggregator, IDatabase database) : base(NavigationService)
        {
            _securityService = securityService;
            _database = database;
            _eventAggregator = eventAggregator;

            _pageDialogService = pageDialogService;
            var ClientDetails = new ClientDetails();
            MyClient = ClientDetails;
            
        }

        
    }
}
