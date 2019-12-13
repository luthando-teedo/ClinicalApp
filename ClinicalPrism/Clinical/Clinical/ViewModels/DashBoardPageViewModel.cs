using Clinical.Model;
using Clinical.Model.Security;
using Clinical.Services.Interface;
using Clinical.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Clinical.ViewModels
{
    public class DashBoardPageViewModel : ViewModelBase
    {
        private IUserProfile _userProfile;

        private IDatabase _database;

        private ClientDetails _loggedInUser;
        public ClientDetails LoggedInUser
        {
            get { return _loggedInUser; }
            set { SetProperty(ref _loggedInUser, value); }
        }


        private ObservableCollection<Appointment> _allAppt;
        public ObservableCollection<Appointment> AllAppt
        {
            get { return _allAppt; }
            set { SetProperty(ref _allAppt, value); }
        }


        private DelegateCommand _ViewFolderCommand;

        public DelegateCommand ViewFolderCommand =>
            _ViewFolderCommand ?? (_ViewFolderCommand = new DelegateCommand(ExecuteViewFolderCommand));

        private async void ExecuteViewFolderCommand()
        {
            await NavigationService.NavigateAsync("MasterDetail/NavigationPage/FolderPage", useModalNavigation: true);
        }

        public DashBoardPageViewModel(INavigationService navigation, IUserProfile userProfile, IDatabase database) : base(navigation)
        {
            _userProfile = userProfile;

            _database = database;
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            LoggedInUser = _userProfile.GetLoggedInUser();

            var user = _userProfile.GetLoggedInUser();

            var appointments = await _database.GetAppointmentsByClientDetailId(user.ID);
            AllAppt = new ObservableCollection<Appointment>(appointments);
        }

    }
}
