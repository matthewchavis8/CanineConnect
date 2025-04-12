using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace YourAppNamespace
{
    public partial class LoginModalPage : ContentPage
    {
        // Reuse a single HttpClient instance throughout your app.
        private static readonly HttpClient client = new HttpClient();

        public LoginModalPage()
        {
            InitializeComponent();
        }
    
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Prepare the authentication payload.
            var payload = new
            {
                // 'identity' is usually the email or username
                identity = ModalUsernameEntry.Text,
                password = ModalPasswordEntry.Text
            };

            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {  
                // Use the login endpoint (authentication) for PocketBase.
                var pocketBaseUrl = "https://3d0b-131-95-215-14.ngrok-free.app/api/collections/users/auth-with-password";
                var response = await client.PostAsync(pocketBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Optionally, deserialize the response to get the token.
                    await DisplayAlert("Success", "Logged in successfully!", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    string errorBody = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", "Invalid credentials: " + errorBody, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }

    // Optionally, a model class for deserializing the login result.
    public class AuthResult
    {
        public string Token { get; set; }
        public object User { get; set; }
        // Add additional properties as defined by your PocketBase response.
    }
}
