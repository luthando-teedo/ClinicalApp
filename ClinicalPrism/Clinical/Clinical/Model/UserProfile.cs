using Clinical.Services.Interfaces;
using Clinical.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinical.Model.Security
{
    public class UserProfile : IUserProfile
    {

        private ClientDetails _loggedInUser;

        public void SetLoggedInUser(ClientDetails clientDetails)
        {
            _loggedInUser = clientDetails;
        }

        public ClientDetails GetLoggedInUser()
        {
            return _loggedInUser;
        }
    }
}
