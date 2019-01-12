using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Instict2K19
{
    public partial class NameAndGroupPage : ContentPage
    {
        public NameAndGroupPage()
        {
            InitializeComponent();
            BindingContext = new NameAndGroupViewModel(this);
        }
    }
}
