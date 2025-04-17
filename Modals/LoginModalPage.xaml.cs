using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CanineConnect;
using Microsoft.Maui.Controls;

namespace YourAppNamespace
{
    public partial class LoginModalPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        public LoginModalPage()
        {
            InitializeComponent();
        }
    
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            var payload = new {
                identity = ModalUsernameEntry.Text,
                password = ModalPasswordEntry.Text
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try {    
                var pocketBaseUrl = "https://14b0-131-95-215-14.ngrok-free.app/api/collections/users/auth-with-password";
                var response = await client.PostAsync(pocketBaseUrl, content);

                if (response.IsSuccessStatusCode) {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Success", "Logged in successfully!", "OK");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                } else {
                    string errorBody = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", "Invalid credentials: " + errorBody, "OK");
                }
            } catch (Exception ex) {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
