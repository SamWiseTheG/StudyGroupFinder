using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
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

            var uri = new Uri("http://a6a54ef8.ngrok.io/api/users/login");
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);
            //var ads = "dsad";
            /*
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            */
        }

    }
}
