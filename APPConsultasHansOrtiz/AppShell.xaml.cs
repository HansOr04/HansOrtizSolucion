namespace APPConsultasHansOrtiz
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rutas para navegación
            Routing.RegisterRoute(nameof(Views.HOCreatePage), typeof(Views.HOCreatePage));
            Routing.RegisterRoute(nameof(Views.HODetailPage), typeof(Views.HODetailPage));
            Routing.RegisterRoute(nameof(Views.HOEditPage), typeof(Views.HOEditPage));
        }
    }
}