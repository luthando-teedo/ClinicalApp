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
        private IDatabase _database;

        public Appointment appointment { get; set; }

        private DelegateCommand<Appointment> _requestCommand;
        public DelegateCommand<Appointment> RequestCommand =>
    _requestCommand ?? (_requestCommand = new DelegateCommand<Appointment>(ExecuteRequestCommand));

        private async void ExecuteRequestCommand(Appointment appointment)
        {
            var user = _userProfile.GetLoggedInUser();
            var apptmntRequest = await _database.GetAppointmentsByClientDetailId(user.ID);

            //await _database.SaveItemAsync(appointment);
        }

        private ClientDetails _loggedInUser;
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
