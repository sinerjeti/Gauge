namespace Gauge.Pages.LoginPages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
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
        await Shell.Current.GoToAsync("RegistrationAnthropometricDataPage"); //for test
    }

    /*
    чисто дл€ красоты. если чел нажимает на простой текст, галочка нажимаетс€, если на выделенный -
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

    private void AgreementCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (AgreementCheckBox.IsChecked == true && PrivacyCheckBox.IsChecked == true)
        {
            Huynya_knopka.Opacity = 1;
            Huynya_knopka.IsEnabled = true;
        }
        else
        {
            Huynya_knopka.Opacity = 0.5;
            Huynya_knopka.IsEnabled = false;
        }
    }

}