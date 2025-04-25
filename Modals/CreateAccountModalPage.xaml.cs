using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CanineConnect;
using CanineConnect.Pages;

namespace CanineConnect.Modals
{
    public partial class CreateAccountModalPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();

        public CreateAccountModalPage()
        {
            InitializeComponent();
        }
    
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            var payload = new
            {
                email = ModalUsernameEntry.Text,
                password = ModalPasswordEntry.Text,
                passwordConfirm = ModalPasswordEntry.Text 
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try {
                var pocketBaseUrl = "https://78c5-131-95-215-14.ngrok-free.app/api/collections/users/records";
                var response = await client.PostAsync(pocketBaseUrl, content);

                if (response.IsSuccessStatusCode) {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Success", "Account created successfully!", "OK");
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                } else {
                    string errorBody = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", "Account creation failed: " + errorBody, "OK");
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
