using Clinical.Model;
using Clinical.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clinical.Services.Interface
{
    public interface IDatabase
    {
        Task<int> SaveItemAsync(ClientDetails clientDetails);
        Task<ClientDetails> GetClientDetailsByUserName(string userName);
        Task<List<Appointment>> GetAppointmentsByClientDetailId(int id);

    }
}
