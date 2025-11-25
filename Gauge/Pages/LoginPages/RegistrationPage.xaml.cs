using Gauge.DTOs;
using System.Net.Http.Json;

namespace Gauge.Pages.LoginPages;

public partial class RegistrationPage : ContentPage
{
    private static readonly HttpClient _httpClient = new HttpClient();

    public RegistrationPage(string phoneNumber)
	{
		InitializeComponent();
        UserPhoneNumber.Text = $"Введите данные от аккаунта, личные данные\nи код подтверждения из SMS на номер\n{phoneNumber}.";
	}

    /*
    1. Ну собственно. сверху страницы далжна быть анимация. по моим выдумкам у нас круто смотрится этот челик
       черный, поэтому я хочу его сделать лицом вашего приложения. конкретно тут этот чел должен делать вид,
       типо записывает информацию пользователя на листок бумаги. хз, как вам, но как по мне, смотрится ахуенно.
    */

	private async void InRegistrationNextPage(object sender, EventArgs e)
	{
        RegisterButton.IsEnabled = false;
        RegisterButton.Opacity = 0.5;
        try
        {
            RegisterUserDTO newUser = new()
            {
                Username = NewUserName.Text,
                PhoneNumber = UserPhoneNumber.Text,
                Password = NewUserPassword.Text,
                Birthday = DateOnly.FromDateTime((DateTime)NewDate.Date).ToString()
            };
            using var response = await _httpClient.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/createuser", newUser);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                await DisplayAlertAsync("error", "damn bro, it`s error", "OK");
                RegisterButton.IsEnabled = true;
                RegisterButton.Opacity = 1;
            }
            else
            {
                await DisplayAlertAsync("victory", "next page coming soon been here", "chechnya cool");
            }
        }
        catch (Exception)
        {
            await DisplayAlertAsync("erroe", "unknown error was catched", "OK");
            RegisterButton.IsEnabled = true;
            RegisterButton.Opacity = 1;
        }
    }

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
            AgreementCheckBox.ClassId = "";
            PrivacyCheckBox.ClassId = "";
        }
    }


    private async void NewUserName_Completed(object sender, EventArgs e)
    {
        CheckUsernameDTO username = new() { Username = NewUserName.Text };
        using var response = await _httpClient.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/checkusername", username);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            UserNameBorder.Stroke = Color.FromRgb(0, 255, 0);
            UserNameBorder.ClassId = "Valid";
        }
        else
        {
            UserNameBorder.Stroke = Color.FromRgb(255, 0, 0);
            UserNameBorder.ClassId = "";
        }
    }

    private void NewUserPassword_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (NewUserPassword.Text.Length >= 6) NewUserPassword.ClassId = "Valid";
    }



    private void NewDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (NewDate.Date != null) NewDate.ClassId = "Valid";
    }
}