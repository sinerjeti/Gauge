using Gauge.DTOs;
using System.Net.Http.Json;

namespace Gauge.Pages.LoginPages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(string phoneNumber)
	{
		InitializeComponent();
        UserPhoneNumber.Text = phoneNumber;
	}

    /*
    1. Ќу собственно. сверху страницы далжна быть анимаци€. по моим выдумкам у нас круто смотритс€ этот челик
       черный, поэтому € хочу его сделать лицом вашего приложени€. конкретно тут этот чел должен делать вид,
       типо записывает информацию пользовател€ на листок бумаги. хз, как вам, но как по мне, смотритс€ ахуенно.

    2. получаете от чела данные, записываете и на сервер - все по классике.

    3. естественно если чел не введет все данные и не нажмет все флажки, на некст страницу его пускать нальз€
    */

	private async void InRegistrationNextPage(object sender, EventArgs e)
	{
        HttpClient client = new HttpClient();
        RegisterUserDTO newUser = new()
        {
            Username = NewUserName.Text,
            PhoneNumber = UserPhoneNumber.Text,
            Password = NewUserPassword.Text,
            Birthday = NewDate.Date.ToString()
        };
        using var response = await client.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/createuser", newUser);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            await DisplayAlertAsync("error", "damn bro, it`s error", "OK");
            return;
        }
        else
        {
            await DisplayAlertAsync("victory", "next page coming soon been here", "chechnya cool");
        }
    }

    /*
    чисто дл€ красоты. если чел нажимает на простой текст, галочка нажимаетс€, если на выделенный -
    переход на страницу с политикой или пользовательским соглашением
    */

    //private void TapAgreementCheckBoxText(object sender, TappedEventArgs e)
    //{
    //    AgreementCheckBox.IsChecked = !AgreementCheckBox.IsChecked;
    //}

    //private void TapPrivacyCheckBoxText(object sender, TappedEventArgs e)
    //{
    //    PrivacyCheckBox.IsChecked = !PrivacyCheckBox.IsChecked;
    //}

    private async void TapAgreementCheckBoxPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("AgreementPage");
    }

    private async void TapPrivacyCheckBoxPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("PrivacyPage");
    }

    private void AgreementCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (AgreementCheckBox.IsChecked == true && PrivacyCheckBox.IsChecked == true)
        {
            AgreementCheckBox.ClassId = "Valid";
            PrivacyCheckBox.ClassId = "Valid";
        }
        else
        {
            AgreementCheckBox.ClassId = "Invalid";
            PrivacyCheckBox.ClassId = "Invalid";
        }
    }


    private async void NewUserName_Completed(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        CheckUsernameDTO username = new() { Username = NewUserName.Text };
        using var response = await client.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/checkusername", username);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            UserNameBorder.Stroke = Color.FromRgb(0, 255, 0);
            UserNameBorder.ClassId = "Valid";
        }
        else
        {
            UserNameBorder.ClassId = "Invalid";
            UserNameBorder.Stroke = Color.FromRgb(255, 0, 0);
        }
    }
}