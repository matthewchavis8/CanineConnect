using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CanineConnect;
using CanineConnect.Pages;

namespace CanineConnect.Modals
{
    public partial class LoginModalPage : ContentPage
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "https://78c5-131-95-215-14.ngrok-free.app/api";

        public LoginModalPage()
        {
            InitializeComponent();
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            var email = ModalUsernameEntry.Text?.Trim();
            var pass  = ModalPasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass)) {
                await DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            var result = await TryLogin(email, pass);
            if (result) {
                await DisplayAlert("Success", "Logged in successfully!", "OK");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
        }

        private async Task<bool> TryLogin(string email, string password)
        {
            var payload = new { identity = email, password = password };
            var json    = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage resp;
            string body;
            try {
                resp = await client.PostAsync(
                    $"{BaseUrl}/collections/users/auth-with-password",
                    content
                );
                body = await resp.Content.ReadAsStringAsync();
            } catch (Exception ex) {
                await DisplayAlert("Error", $"Network error: {ex.Message}", "OK");
                return false;
            }

            if (!resp.IsSuccessStatusCode) {
                if (body.Contains("verified") && body.Contains("email")) {
                    await RequestVerificationEmail(email);
                    await DisplayAlert(
                        "Email Not Verified",
                        "We’ve sent you a verification link. Please click it in your email, then tap Submit again.",
                        "OK"
                    );
                } else {
                    await DisplayAlert("Login Failed", body, "OK");
                }
                return false;
            }

            using var doc = JsonDocument.Parse(body);
            var root   = doc.RootElement;
            var record = root.GetProperty("record");
            bool verified = record.GetProperty("verified").GetBoolean();

            if (!verified) {
                await RequestVerificationEmail(email);
                await DisplayAlert(
                    "Email Not Verified",
                    "Your email is still unverified. Please check your inbox for the link.",
                    "OK"
                );
                return false;
            }

            var token = root.GetProperty("token").GetString();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return true;
        }

        private async Task RequestVerificationEmail(string email)
        {
            var payload = new { email };
            var json    = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try {
                await client.PostAsync(
                    $"{BaseUrl}/collections/users/request-verification",
                    content
                );
            } catch {}
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
