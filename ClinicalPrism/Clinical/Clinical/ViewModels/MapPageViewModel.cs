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
        //public Appointment MapAddress { get; set; }
        private Appointment _mapAddress;
        public Appointment MapAddress
        {
            get { return _mapAddress; }
            set { SetProperty(ref _mapAddress, value); }
        }

        private DelegateCommand<Appointment> _getDirections;
        public DelegateCommand<Appointment> GetDirections =>
    _getDirections ?? (_getDirections = new DelegateCommand<Appointment>(ExecuteGetDirections));

        private async void ExecuteGetDirections(Appointment appointment)
        {
            //var user = _userProfile.GetLoggedInUser();
            // var RecentAppt = await _database.GetAppointmentsByClientDetailId(appointment.ClientDetailId);
            //await MapAddress.GetLocation(Appointment.);

            //await _database.SaveItemAsync(appointment);
        }
        public MapPageViewModel(IDatabase database, INavigationService navigation) : base(navigation)
        {

            _database = database;

            var stuff = new Appointment();
            MapAddress = stuff;
        }
    }
}
