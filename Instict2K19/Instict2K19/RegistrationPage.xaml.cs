using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Instict2K19
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel(this);
        }
        protected override bool OnBackButtonPressed()
        {
            //Task.Run(async () =>
            //{
            //    bool shouldExit = await DisplayAlert("Exit", "Are you sure, you want to exit app.", "Yes", "No");
            //    if (shouldExit)
            //        return base.OnBackButtonPressed();
            //    else
            //        return true;
            //});
            return true;
        }
    }
}
