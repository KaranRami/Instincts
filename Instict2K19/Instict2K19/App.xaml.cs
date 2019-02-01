using System;
using System.IO;
using System.Threading.Tasks;
using Instict2K19.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Instict2K19
{
    public partial class App : Application
    {
        static RegistrationDatabase database;
        public static RegistrationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RegistrationDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RegistrationSQLite.sqlite"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            Task.Run(async () => {
                try
                {
                    var UserPreferencesResult = await DependencyService.Get<IUserPreferences>().GetUserPreferences();
                    if (UserPreferencesResult.response.Success)
                    {
                        Constants.UserName = UserPreferencesResult.user.UserName;
                        Constants.GroupName = UserPreferencesResult.user.Gruop;
                        MainPage = new NavigationPage(new RegistrationPage());
                    }
                    else
                    {
                        MainPage = new NavigationPage(new NameAndGroupPage());
                    }

                }
                catch (Exception ex)
                {
                    MainPage = new NavigationPage(new NameAndGroupPage());
                }

            }).Wait();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
