using CommunityToolkit.Maui.Markup;
using Gauge.DTOs;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using WebApiForGauge.Models;

namespace Gauge.Pages.LoginPages;

public partial class LoginPage : ContentPage
{
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
            LoginBorder.Stroke = Color.FromArgb("#2d0c98");
            LoginLabel.TextColor = Color.FromArgb("#2d0c98");
            Button.IsEnabled = false;
            Button.Opacity = 0.5;
        }
    }

    public async void EnterLoginNumber(object sender, EventArgs e)
    {
        try
        {
            Button.Opacity = 0.5;
            Button.IsEnabled = true;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            PhoneNumberRequestDTO phoneNumber = new() { PhoneNumber = LoginNumber.Text };
            using var response = await client.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/checkuserexist", phoneNumber);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Navigation.PushModalAsync(new RegistrationPage());
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await Grid1.TranslateToAsync(0, -55, 500, Easing.SinIn);
                Grid1.TranslationY = 0;
                Grid1.Margin = new(0, 55, 0, 0);
                Grid3.IsVisible = true;
                LoginNumber.IsReadOnly = true;
                LoginBorder.Stroke = Color.FromArgb("#2d0c98");
                LoginLabel.TextColor = Color.FromArgb("#2d0c98");
                ReloadButton.IsVisible = true;
                Button.Clicked -= EnterLoginNumber;
                Button.Clicked += CheckPassword;
                Button.IsEnabled = true;
                Button.Opacity = 1;
            }
        }
        catch (Exception exp)
        {
            await DisplayAlertAsync("Error", $"{exp}", "OK");
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
        try
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            CheckPasswordRequestDTO checkPasswordRequest = new() { 
                PhoneNumber = LoginNumber.Text,
                Password = Password.Text 
            };

            using var response = await client.PostAsJsonAsync("https://webapiforgauge.onrender.com/user/checkpassword", checkPasswordRequest);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlertAsync("Успех", "Пароль верный!", "OK");
            }
            else
            {
                await DisplayAlertAsync("Ошибка", "Неверный пароль!", "OK");
            }
        }
        catch (Exception exp)
        {
            await DisplayAlertAsync("Ошибка", $"Произошла ошибка: {exp.Message}", "OK");
        }
    }

    //private async void EnterLoginNumber() { }
    //private async void EnterVerificationCode() { }
    //private async void EnterPassword() { }
    //private async void OnTextChanged() { }

    /*
    Короче. чел вводит номер телефона и вызывается эта функция. 
    Скорее всего это должен быть класс, потому что тут должно быть несколько функций.
    1. проверка номера на подленность. 
        * если чел нихуя не ввел, надо вывести что-то типо ошибки.
        * если чел ввел неправильный номер (больше 11-ти символов или вообще нахуй буквы)
          надо тоже сказать, чтобы норм номер ввел. 
        * естественно тут понадобится сразу преобразовать номер, на случай, если чел додумается
          вводить номер ТАК блять: 
            +7 (905) 717-84-40 //ну типо правильный короче
        преобразовать номер нужно потому, что мы решили избавиться от почты, оставить только
        регистрацию по номеру.
    2. отправка номера на сервер для проверки, есть ли он в базе данных или нет.
        * тут ты отправляешь на сервер запрос с проверкой есть ли этот номер в базе или нет.
          пока это все происходит, я какую-нибудь анимку добавлю и заебок будет.
        * если же у типа нет инета и запросы к серверу не идут, надо будет ему тоже собщение с
          ошибкой вывести что инета нет.
    3.1 если номер был найден, на телефон пользователя будет отправлен проверочный код. должна
        быть анимация подъема строки с вводом номера чуть выше, а на ее место придти новая
        строка с вводом кода. после успешного ввода кода появится третья строка с вводом пароля,
        а уже после переход на основную страницу. 
    3.2 если номер не был найден, но он правильный, значит чела надо зарегистрировать. 
        оправляете его на страницу RegistrationPage
    */

}