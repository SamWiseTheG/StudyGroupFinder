using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace StudyGroupFinder
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked_signUp(object sender, System.EventArgs e)
        {
            var rootPage = Navigation.NavigationStack.FirstOrDefault();

            Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
            await Navigation.PopToRootAsync();
        }
    }
}
