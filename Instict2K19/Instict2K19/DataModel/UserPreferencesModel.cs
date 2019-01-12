using System;
namespace Instict2K19.DataModel
{
    public class UserPreferences
    {
        public string UserName { get; set; }
        public string Gruop { get; set; }
    }

    public class UserPreferencesResponse
    {
        public bool Success { get; set; }
        public Object Error { get; set; }
    }

}
