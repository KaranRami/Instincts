using System;
using System.Threading.Tasks;
using Instict2K19.DataModel;

namespace Instict2K19.Interface
{
    public interface IUserPreferences
    {
        Task<UserPreferencesResponse> SaveUserPreferences(UserPreferences preferences);
        Task<(UserPreferencesResponse response, UserPreferences user)> GetUserPreferences();
        Task<UserPreferencesResponse> ClearUserPreferences();
    }
}
