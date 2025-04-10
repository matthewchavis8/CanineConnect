namespace YourAppNamespace;

public partial class LoginModalPage : ContentPage
{
    public LoginModalPage()
    {
        InitializeComponent();
    }
    
    private async void OnSubmitClicked(object sender, EventArgs e)
    {

         if (ModalUsernameEntry.Text == "admin" && ModalPasswordEntry.Text == "password")
         {
             await DisplayAlert("Success", "Logged in successfully!", "OK");

             await Navigation.PopModalAsync();
         }
         else
         {
             await DisplayAlert("Error", "Invalid credentials. Please try again.", "OK");
         }
    }
    
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
