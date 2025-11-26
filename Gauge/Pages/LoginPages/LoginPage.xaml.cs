using Gauge.DTOs;
using Microsoft.Maui.Animations;
using System.Net.Http.Json;
using WebApiForGauge.Models;

namespace Gauge.Pages.LoginPages;

public partial class LoginPage : ContentPage
{
    private static readonly HttpClient _httpClient = new HttpClient();
    public LoginPage()
    {
        InitializeComponent();
    }

    private void ChangedLoginNumber(object sender, TextChangedEventArgs e)
    {
        if (LoginNumber.Text.Length == 18)
        {
            Button.IsEnabled = true;
            Button.Opacity = 1;
        }
        else
        {
            Button.IsEnabled = false;
            Button.Opacity = 0.5;
        }
    }

    public async void EnterLoginNumber(object sender, EventArgs e)
    {
        if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlertAsync("error", "Отсутствует подключение к интернету!", "OK");
            return;
        }

        try
        {
            Button.Opacity = 0.5;
            Button.IsEnabled = true;
            LoginBorder.Stroke = Colors.Yellow;         //тут короче строа перекрашивается в желтый цвет, пока идет подключение к апишке
            LoginNumberLabel.TextColor = Colors.Yellow; //мне бы хотелось, чтобы это была плавная анимация переливания с цвета в цвет, а не резкое окрашивание
            PhoneNumberRequestDTO phoneNumber = new() { PhoneNumber = LoginNumber.Text };
            using var response = await _httpClient.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/checkuserexist", phoneNumber);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Navigation.PushModalAsync(new RegistrationPage(LoginNumber.Text));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await Grid1.TranslateToAsync(0, -67.5, 500, Easing.SinIn);
                LoginBorder.Stroke = Colors.Green; //то же самое, хочу анимации(((
                LoginNumberLabel.TextColor = Colors.Green;
                Grid2.IsVisible = true;
                await Grid2.FadeToAsync(1, 500);
                Grid3.IsVisible = true;
                await Grid3.FadeToAsync(1, 500);

                /*
                короче, теперь все сетки расположены в одной строке общей сетки. при изменении параметра IsEnabled
                никто никуда прыгать и съезжать не будет. тебе нужно анимку сделать в таком порядке: 
                1. у тебя съездает Grid1 на некоторые коорды && кнопка становится меньше и меняет параметр HorizontalOptions на End
                ?2. появляется строка с вводом пароля и строка с вводом кода. пока не знаю, как это реализовать, главное сделать первое.
                */

                /*await Grid1.TranslateToAsync(0, -55, 500, Easing.SinIn);
                Grid3.IsVisible = true;
                LoginNumber.IsReadOnly = true;
                LoginBorder.Stroke = Color.FromArgb("#6134f0");
                LoginNumberLabel.TextColor = Color.FromArgb("#6134f0");
                ReloadButton.IsVisible = true;
                Button.Clicked -= EnterLoginNumber;
                Button.Clicked += CheckPassword;
                Button.IsEnabled = false;
                Button.Opacity = 0.5;*/
            }
        }
        catch (Exception)
        {
            await DisplayAlertAsync("Error", "ошибка выполнения", "OK");
        }

        /*
        дальше идет проверка кода. оправляем введенный пользователем код на сервер для
        проверки. 
        * если код введен неправильный, надо будет label и border перекрасить в красный и
          сделать такую же штучку, как и с первой строкой ввода. 
        * если код введен правильный, анимация идет дальше.
        еще надо сделать обработчик для кнопки ReloadButton. она ес че возвращает страницу
        в первоначальное положение.
        */

    }
    
    public async void CheckPassword(object sender, EventArgs e)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlertAsync("error", "Отсутствует подключение к интернету!", "OK");
            return;
        }

        CheckPasswordRequestDTO checkPasswordRequest = new() 
        { 
            PhoneNumber = LoginNumber.Text,
            Password = Password.Text 
        };

        using var response = await _httpClient.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/checkpassword", checkPasswordRequest);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            await DisplayAlertAsync("Успех", "Пароль верный!", "OK");
            //вместо этого переход на другую страницу, когда Захар её оформит
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        {
            await DisplayAlertAsync("error", $"Ошибка с нашей стороны\nУже устраняем её!", "OK");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
        {
            await DisplayAlertAsync("error", "На данный момент сервер недоступен", "ОК");
        }
        else
        {
            await DisplayAlertAsync("Ошибка", "Произошла ошибка", "OK");
        }
    }

    private async void ReloadButton_Clicked(object sender, EventArgs e)
    {
        /*
        ReloadButton.IsVisible = false;
        Grid3.IsVisible = false;
        Grid1.TranslationY = -57;
        await Grid1.TranslateToAsync(0, 9, 500, Easing.SinIn);
        Grid1.TranslationY = 0;
        Grid1.Margin = new(0, 0, 0, 0);
        LoginNumber.Text = string.Empty;
        LoginNumber.IsReadOnly = false;
        Password.Text = string.Empty;
        Button.Clicked -= CheckPassword;
        Button.Clicked += EnterLoginNumber;
        */
    }
}