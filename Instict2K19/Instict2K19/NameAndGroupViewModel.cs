using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Instict2K19.DataModel;
using Instict2K19.Interface;
using Xamarin.Forms;

namespace Instict2K19
{
    public class NameAndGroupViewModel : BaseViewModel
    {
        public NameAndGroupViewModel(ContentPage view) : base(view)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var Result = await DependencyService.Get<IUserPreferences>().GetUserPreferences();
                if (Result.response.Success)
                {
                    UserName = Result.user.UserName;
                    GroupName = Result.user.Gruop;
                }
                else
                {
                    if (Result.response.Error is Exception ex)
                        await base.View.DisplayAlert(Constants.AppName, ex.Message, "Ok");
                    else
                        await base.View.DisplayAlert(Constants.AppName, "Something went wrong.", "Ok");
                }

            });
        }
        public ICommand SaveCommand { get { return new Command(async () => await SaveCommandEvent()); } }

        private async Task SaveCommandEvent()
        {
            if (Validate())
            {
                try
                {
                    UserPreferences preferences = new UserPreferences()
                    {
                        Gruop = GroupName.ToUpper(),
                        UserName = UserName.ToUpper(),
                    };
                    var SaveResult = await DependencyService.Get<IUserPreferences>().SaveUserPreferences(preferences);
                    if(SaveResult.Success)
                    {
                        Constants.UserName = UserName.ToUpper();
                        Constants.GroupName = GroupName.ToUpper();
                        Application.Current.MainPage= new NavigationPage(new RegistrationPage());
                    }
                    else
                    {
                        if (SaveResult.Error is Exception ex)
                            await base.View.DisplayAlert(Constants.AppName, ex.Message, "Ok");
                        else
                            await base.View.DisplayAlert(Constants.AppName, "Something went wrong.", "Ok");
                    }

                }
                catch(Exception ex)
                {
                   await base.View.DisplayAlert(Constants.AppName, ex.Message, "Ok");
                }
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(GroupName) || string.IsNullOrEmpty(UserName))
            {
                IsError = true;
                return false;
            }
            else
            {
                IsError = false;
                return true;
            }

        }

        private string groupName;
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
                RaisePropertyChanged(() => GroupName);
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private bool isError = false;
        public bool IsError
        {
            get { return isError; }
            set
            {
                isError = value;
                RaisePropertyChanged(() => IsError);
            }
        }
    }
}