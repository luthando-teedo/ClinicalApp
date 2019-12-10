using Prism.Events;
using Clinical.Enums;
using Clinical.Messages.Security;
using Clinical.Model.Security;
using Clinical.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Clinical.Services
{
    public class FakeSecurityService : ISecurityService
    {
        private IEventAggregator _eventAggregator;
        public IList<MenuItem> _allMenuItems;
        public IList<MenuItem> allMenuItems;

        public bool LoggedIn { get; set; }

        public FakeSecurityService(IEventAggregator eventAggregator)
        {
            CreateMenuItems();

            _eventAggregator = eventAggregator;
        }

        public IList<MenuItem> GetAllowedAccessItems()
        {
            if (LoggedIn)
            {
                var accessItems = new List<MenuItem>();

                foreach(var item in _allMenuItems)
                {
                    if (item.MenuType == MenuTypeEnum.Secured || item.MenuType == MenuTypeEnum.UnSecured ||item.MenuType == MenuTypeEnum.LogOut )
                    {
                        accessItems.Add(item);
                    }
                }

                return accessItems.OrderBy(x => x.MenuOrder).ToList();
            }
            else
            {
                var accessItems = new List<MenuItem>();

                foreach (var item in _allMenuItems)
                {
                    if (item.MenuType == MenuTypeEnum.UnSecured || item.MenuType == MenuTypeEnum.Login)
                    {
                        accessItems.Add(item);
                    }
                }

                return accessItems.OrderBy(x => x.MenuOrder).ToList();
            }
        }

        public bool LogIn(string userName, string password)
        {
            // Do Your Stuff to Check if Legit (ie API Calls)

            LoggedIn = true;

            return true;
        }

        public void LogOut()
        {
            LoggedIn = false;

            _eventAggregator.GetEvent<LogOutMessage>().Publish();
        }



        private void CreateMenuItems()
        {
            _allMenuItems = new List<MenuItem>();

            var menuItem = new MenuItem();
            menuItem.MenuItemId = 1;
            menuItem.MenuItemName = "Login";
            menuItem.NavigationPath = "NavigationPage/loginPage";
            menuItem.MenuType = MenuTypeEnum.Login;
            menuItem.MenuOrder = 99;
            menuItem.ImageName =  "door.png";

            _allMenuItems.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.MenuItemId = 2;
            menuItem.MenuItemName = "Logout";
            menuItem.NavigationPath = "NavigationPage/loginPage";
            menuItem.MenuOrder = 99;
            menuItem.MenuType = MenuTypeEnum.LogOut;
            menuItem.ImageName = "exit.png";

            _allMenuItems.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.MenuItemId = 3;
            menuItem.MenuItemName = "Request Delivery";
            menuItem.NavigationPath = "NavigationPage/FolderPage";
            menuItem.MenuOrder = 3;
            menuItem.MenuType = MenuTypeEnum.Secured;
            menuItem.ImageName = "location.png";

            _allMenuItems.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.MenuItemId = 4;
            menuItem.MenuItemName = "Edit Profile";
            menuItem.NavigationPath = "NavigationPage/OtherView";
            menuItem.MenuOrder = 4;
            menuItem.MenuType = MenuTypeEnum.Secured;
            menuItem.ImageName = "profile.png";

            _allMenuItems.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.MenuItemId = 6;
            menuItem.MenuItemName = "Dashboard";
            menuItem.NavigationPath = "NavigationPage/DashBoardPage";
            menuItem.MenuOrder = 1;
            menuItem.MenuType = MenuTypeEnum.Secured;
            menuItem.ImageName = "home.png";

            _allMenuItems.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.MenuItemId = 5;
            menuItem.MenuItemName = "About Us";
            menuItem.NavigationPath = "NavigationPage/ViewPdfView";
            menuItem.MenuOrder = 5;
            menuItem.MenuType = MenuTypeEnum.UnSecured;
            menuItem.ImageName = "infor.png";

            


            _allMenuItems.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.MenuItemId = 6;
            menuItem.MenuItemName = "Dashboard";
            menuItem.NavigationPath = "NavigationPage/DashBoardPage";
            menuItem.MenuOrder = 1;
            menuItem.MenuType = MenuTypeEnum.Secured;
            menuItem.ImageName = "home.png";


            allMenuItems = _allMenuItems;
        }
    }
}
