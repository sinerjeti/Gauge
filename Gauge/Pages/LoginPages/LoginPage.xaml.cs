using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Gauge.Pages.LoginPages;

public partial class LoginPage : ContentPage
{

    int huy = 0;

    public LoginPage()
    {
        InitializeComponent();
    }

    private void ChangedLoginNumber(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(LoginNumber.Text))
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
        var number = LoginNumber.Text.ToString();
        if (number.Length >= 11 && number.Length <= 15)
        {
            //тут будет проверка номера на сервере
            Grid2.IsVisible = true;
            LoginNumber.IsReadOnly = true;
            LoginBorder.Stroke = Color.FromArgb("#2d0c98");
            LoginLabel.TextColor = Color.FromArgb("#2d0c98");
        }
        else
        {
            LoginBorder.Stroke = Colors.Red;
            LoginLabel.TextColor = Colors.Red;
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