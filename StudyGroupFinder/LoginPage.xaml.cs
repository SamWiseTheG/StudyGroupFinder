using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StudyGroupFinder
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }

        async void Handle_Clicked_SignUp(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void Handle_Clicked_Login(object sender, System.EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };
            var isValidUser = ValidUser(user);

            if (isValidUser)
            {
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
        }

        bool ValidUser(User user)
        {
            //TODO:David check db
            var testUser = new User
            {
                Username = "test",
                Password = "123"
            };
            if (user.Username == testUser.Username && user.Password==testUser.Password)
            {
                return true;
            }
            else
            {
                passwordEntry.Text = "";
                return false;
            }
        }
    }
}
