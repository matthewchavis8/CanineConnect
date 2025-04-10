using CanineConnect;

namespace YourAppNamespace;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (username == "admin" && password == "password")
        {
            await DisplayAlert("Success", "Logged in!", "OK");

            await Navigation.PushAsync(new MainPage()); // Make sure HomePage exists!
        }
        else
        {
            await DisplayAlert("Login Failed", "Invalid credentials", "Try Again");
        }
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage()); // You'd create this too
    }
}
