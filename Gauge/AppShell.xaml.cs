using Gauge.Pages.LoginPages;
namespace Gauge
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(AgreementPage), typeof(AgreementPage));
            Routing.RegisterRoute(nameof(PrivacyPage), typeof(PrivacyPage));
            Routing.RegisterRoute(nameof(RegistrationAnthropometricDataPage), typeof(RegistrationAnthropometricDataPage));
        }
    }
}
