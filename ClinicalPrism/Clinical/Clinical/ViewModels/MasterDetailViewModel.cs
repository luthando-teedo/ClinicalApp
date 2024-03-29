﻿using Clinical.Messages.Security;
using Clinical.Model.Security;
using Clinical.Services;
using Clinical.Services.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Clinical.ViewModels
{
    public class MasterDetailViewModel :ViewModelBase
    {
        private ISecurityService _securityService;
        private IEventAggregator _eventAggregator;


        private string _profileImage;
        public string ProfileImage
        {
            get { return _profileImage; }
            set { SetProperty(ref _profileImage, value); }
        }


        private DelegateCommand<MenuItem> _navigateCommand;
        private ObservableCollection<MenuItem> _menuItems;
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        public DelegateCommand<MenuItem> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<MenuItem>(ExecuteNavigateCommand));

        public async void ExecuteNavigateCommand(MenuItem menu)
        {
            if (menu.MenuType == Enums.MenuTypeEnum.LogOut)
            {
                _securityService.LogOut();

                await NavigationService.NavigateAsync(menu.NavigationPath);
            }
            else
                await NavigationService.NavigateAsync(menu.NavigationPath);

        }

        public IList<MenuItem> allMenuItemsProperty;
        

        public MasterDetailViewModel(INavigationService navigationService, ISecurityService securityService, IEventAggregator eventAggregator) : base(navigationService)
        {
            _securityService = securityService;
            _eventAggregator = eventAggregator;

            MenuItems = new ObservableCollection<MenuItem>(_securityService.GetAllowedAccessItems());

            
            _eventAggregator.GetEvent<LogOutMessage>().Subscribe(LogOutEvent);
          
        }

        public void LoginEvent(UserProfile userProfile)
        {
            
            ProfileImage = "clinicalOne.png";

            MenuItems = new ObservableCollection<MenuItem>(_securityService.GetAllowedAccessItems());

            NavigationService.NavigateAsync("NavigationPage/RequestPage");
         
        }

        public void LogOutEvent()
        {
            ProfileImage = "splash_logo.png";

            MenuItems = new ObservableCollection<MenuItem>(_securityService.GetAllowedAccessItems());

            NavigationService.NavigateAsync("NavigationPage/LoginView");
         
        }

        
    }
}
