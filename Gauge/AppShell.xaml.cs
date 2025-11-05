using Gauge.Pages.LoginPages;
namespace Gauge
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SecondLoginPage), typeof(SecondLoginPage));
            Routing.RegisterRoute(nameof(RegisterLoginPage), typeof(RegisterLoginPage));
        }
    }
}
