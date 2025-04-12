using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace YourAppNamespace
{
    public partial class CreateAccountModalPage : ContentPage
    {
        // Reuse a single HttpClient instance throughout your app.
        private static readonly HttpClient client = new HttpClient();

        public CreateAccountModalPage()
        {
            InitializeComponent();
        }
    
        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Prepare the registration payload.
            // Change "email" to "username" if your PocketBase configuration uses username.
            var payload = new
            {
                email = ModalUsernameEntry.Text,
                password = ModalPasswordEntry.Text,
                passwordConfirm = ModalPasswordEntry.Text  // remove or adjust if not needed
            };

            // Serialize the payload to JSON.
            string json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Endpoint for creating a new account (registration)
                var pocketBaseUrl = "https://3d0b-131-95-215-14.ngrok-free.app/api/collections/users/records";
                
                // Send the POST request.
                var response = await client.PostAsync(pocketBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Optionally, deserialize the response if you need to process the returned data.
                    await DisplayAlert("Success", "Account created successfully!", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    string errorBody = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", "Account creation failed: " + errorBody, "OK");
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

    // Optionally, you can create a model class if you need to deserialize the response.
    public class CreateAccountResult
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public object User { get; set; }
        // Add additional properties as needed based on PocketBase's response.
    }
}
