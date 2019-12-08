using Clinical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinical.Services.Interfaces
{
    public interface IUserProfile
    {
        void SetLoggedInUser(ClientDetails clientDetails);
        ClientDetails GetLoggedInUser();
    }
}
