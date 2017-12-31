using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
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

            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };

            var client = new HttpClient();

            var uri = new Uri("http://a6a54ef8.ngrok.io/api/users/signup");

            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;


            response = await client.PostAsync(uri, content);

            var rootPage = Navigation.NavigationStack.FirstOrDefault();

            Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
            await Navigation.PopToRootAsync();
        }


    }
}
