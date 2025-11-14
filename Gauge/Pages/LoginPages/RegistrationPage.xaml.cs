using CommunityToolkit.Maui.Markup;

namespace Gauge.Pages.LoginPages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(string phoneNumber)
	{
		InitializeComponent();
        PhoneNumberLabel.Text = phoneNumber;
	}

    /*
    1. Ну собственно. сверху страницы далжна быть анимация. по моим выдумкам у нас круто смотрится этот челик
       черный, поэтому я хочу его сделать лицом вашего приложения. конкретно тут этот чел должен делать вид,
       типо записывает информацию пользователя на листок бумаги. хз, как вам, но как по мне, смотрится ахуенно.

    2. получаете от чела данные, записываете и на сервер - все по классике.

    3. естественно если чел не введет все данные и не нажмет все флажки, на некст страницу его пускать нальзя
    */

	private async void RegisterUser(object sender, EventArgs e)
	{

        await Shell.Current.GoToAsync("RegistrationAnthropometricDataPage");
    }

    /*
    чисто для красоты. если чел нажимает на простой текст, галочка нажимается, если на выделенный -
    переход на страницу с политикой или пользовательским соглашением
    */

    private void TapAgreementCheckBoxText(object sender, TappedEventArgs e)
    {
        AgreementCheckBox.IsChecked = !AgreementCheckBox.IsChecked;
    }

    private void TapPrivacyCheckBoxText(object sender, TappedEventArgs e)
    {
        PrivacyCheckBox.IsChecked = !PrivacyCheckBox.IsChecked;
    }

    private async void TapAgreementCheckBoxPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("AgreementPage");
    }

    private async void TapPrivacyCheckBoxPage(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("PrivacyPage");
    }

    //событие, которое проверяет, чтобы были нажаты оба флажка, а только после этого оно разблокирует кнопку "далее "
    private void AgreementAndPrivacyCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (AgreementCheckBox.IsChecked == true && PrivacyCheckBox.IsChecked == true)
        {
            NextButton.IsEnabled = true;
            NextButton.Opacity = 1;
        }
        else
        {
            NextButton.IsEnabled = false;
            NextButton.Opacity = 0.5;
        }
    }
}