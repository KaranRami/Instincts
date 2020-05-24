
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace Instict2K19
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public ContentPage View;
        public BaseViewModel(ContentPage view)
        {
            View = view;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(String property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            MemberExpression expression = propertyExpression.Body as MemberExpression;
            RaisePropertyChanged(expression.Member.Name);
        }
        public async Task SingleRun(Func<Task> operation)
        {
            object _lock = new object();
            lock (_lock)
            {
                if (CommandInitiated)
                    return;

                CommandInitiated = true;
            }

            try
            {
                await operation();
            }
            catch (Exception ex)
            {
                await View.DisplayAlert("Error:",ex.Message.ToString(),"OK");
            }

            CommandInitiated = false;

        }
        public async Task<bool> AskForPermission(Permission permission)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                    {
                        await View.DisplayAlert(permission.ToString(), "Need permission to use "+ permission.ToString(), "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(permission))
                        status = results[permission];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                await View.DisplayAlert("Error:", ex.Message.ToString(), "OK");
                return false;
            }
        }
        #region Property
        private bool commandInitiated;
        public bool CommandInitiated
        {
            get { return commandInitiated; }
            set { commandInitiated = value; }
        }
        #endregion
        public ICommand SupportCommand { get { return new Command(async () => await SupportCommandEvent()); } }
        private async Task SupportCommandEvent()
        {
            await View.DisplayAlert("Support", "Call: +91 9726642480", "Ok");
        }
    }
}
