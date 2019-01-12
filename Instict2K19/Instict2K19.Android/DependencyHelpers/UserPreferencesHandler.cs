using System;
using System.Threading.Tasks;
using Android.Content;
using Instict2K19.DataModel;
using Instict2K19.Droid.DependencyHelpers;
using Instict2K19.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserPreferencesHandler))]
namespace Instict2K19.Droid.DependencyHelpers
{
    public class UserPreferencesHandler:IUserPreferences
    {
        public UserPreferencesHandler()
        {
        }

        public async Task<(UserPreferencesResponse response,UserPreferences user)> GetUserPreferences()
        {
            UserPreferencesResponse userPreferencesResponse = new UserPreferencesResponse();
            UserPreferences userPreferences = new UserPreferences();
            try
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences(Constants.AppName, FileCreationMode.Private);
                if (prefs.Contains("username") && prefs.Contains("group"))
                {
                    userPreferences.UserName = prefs.GetString("username", string.Empty);
                    userPreferences.Gruop = prefs.GetString("group", string.Empty);
                    prefs.Dispose();
                }
                if (!string.IsNullOrEmpty(userPreferences.UserName) && !string.IsNullOrEmpty(userPreferences.Gruop))
                {
                    userPreferencesResponse.Success = true;
                    userPreferencesResponse.Error = null;
                    return (response: userPreferencesResponse, user: userPreferences);
                }
                else
                {
                    userPreferencesResponse.Success = false;
                    userPreferencesResponse.Error = new Exception("No data found");
                    return (response: userPreferencesResponse, user: userPreferences);
                }
            }
            catch (Exception ex)
            {
                userPreferencesResponse.Success = false;
                userPreferencesResponse.Error = ex;
                return (response:userPreferencesResponse,user:userPreferences);
            }
        }

        public async Task<UserPreferencesResponse> ClearUserPreferences()
        {
            UserPreferencesResponse userPreferencesResponse = new UserPreferencesResponse();
            try
            {
                var prefs = Android.App.Application.Context.GetSharedPreferences(Constants.AppName, FileCreationMode.Private);
                var prefEditor = prefs.Edit();
                prefEditor.PutString("username", string.Empty);
                prefEditor.PutString("group", string.Empty);
                prefEditor.Apply();
                prefEditor.Commit();
                prefs.Dispose();
                userPreferencesResponse.Success= true;
                userPreferencesResponse.Error = null;
                return userPreferencesResponse;
            }
            catch (Exception ex)
            {
                userPreferencesResponse.Success = false;
                userPreferencesResponse.Error = ex;
                return userPreferencesResponse;
            }
        }

        public async Task<UserPreferencesResponse> SaveUserPreferences(UserPreferences preferences)
        {
            UserPreferencesResponse userPreferencesResponse = new UserPreferencesResponse();
            try
            {

                    var prefs = Android.App.Application.Context.GetSharedPreferences(Constants.AppName, FileCreationMode.Private);
                    var prefEditor = prefs.Edit();
                    prefEditor.PutString("username", preferences.UserName);
                    prefEditor.PutString("group", preferences.Gruop);
                    prefEditor.Apply();
                    prefEditor.Commit();
                    prefs.Dispose();
                    userPreferencesResponse.Success = true;
                    userPreferencesResponse.Error = null;
                    return userPreferencesResponse;

            }
            catch(Exception ex)
            {
                userPreferencesResponse.Success = false;
                userPreferencesResponse.Error = ex;
                return userPreferencesResponse;
            }
        }
    }
}
