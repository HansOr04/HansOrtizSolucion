using APPConsultasHansOrtiz.Services;

namespace APPConsultasHansOrtiz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registrar servicios
            builder.Services.AddSingleton<ApiService>();

            // Registrar páginas
            builder.Services.AddTransient<Views.HOListPage>();
            builder.Services.AddTransient<Views.HOCreatePage>();
            builder.Services.AddTransient<Views.HODetailPage>();
            builder.Services.AddTransient<Views.HOEditPage>();

            return builder.Build();
        }
    }
}