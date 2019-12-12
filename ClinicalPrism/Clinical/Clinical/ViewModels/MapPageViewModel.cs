using Clinical.Model;
using Clinical.Services.Interface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinical.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private IDatabase _database;
        public Appointment appointment { get; set; }
        //private Appointment _appointment;
        //public Appointment Appointment
        //{
        //    get { return _appointment; }
        //    set { SetProperty(ref _appointment, value); }
        //}

        private DelegateCommand<Appointment> _requestCommand;
        public DelegateCommand<Appointment> RequestCommand =>
    _requestCommand ?? (_requestCommand = new DelegateCommand<Appointment>(ExecuteRequestCommand));

        private async void ExecuteRequestCommand(Appointment appointment)
        {
            //var user = _userProfile.GetLoggedInUser();
            // var RecentAppt = await _database.GetAppointmentsByClientDetailId(appointment.ClientDetailId);

            await _database.SaveItemAsync(appointment);
        }
        public MapPageViewModel(IDatabase database, INavigationService navigation) : base(navigation)
        {

            _database = database;
        }
    }
}
