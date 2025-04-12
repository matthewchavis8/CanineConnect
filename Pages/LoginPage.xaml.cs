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
        await Navigation.PushModalAsync(new LoginModalPage());
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
       await Navigation.PushModalAsync(new CreateAccountModalPage());
    }
}
