using CommunityToolkit.Maui;
using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;
using MauiBankingExercise.Views;
using Microsoft.Extensions.Logging;

namespace MauiBankingExercise
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<BankingDatabaseService>();

            builder.Services.AddTransient<AllCustomersViewModel>();
            builder.Services.AddTransient<SingleCustomerViewModel>();
            builder.Services.AddTransient<TransactionViewModel>();


            builder.Services.AddTransient<AllCustomersView>();
            builder.Services.AddTransient<SingleCustomerView>();
            builder.Services.AddTransient<TransactionView>();

            return builder.Build();
        }
    }
}
