using System;
using System.Collections.Generic;

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
    }
}
