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
                        Gruop = GroupName,
                        UserName = UserName,
                    };
                    var SaveResult = await DependencyService.Get<IUserPreferences>().SaveUserPreferences(preferences);
                    if(SaveResult.Success)
                    {
                        Constants.UserName = UserName;
                        Constants.GroupName = GroupName;
                        await base.View.Navigation.PushAsync(new RegistrationPage());
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
                groupName = string.IsNullOrEmpty(value)?value:value.ToUpper();
                RaisePropertyChanged(() => GroupName);
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = string.IsNullOrEmpty(value) ? value : value.ToUpper();
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