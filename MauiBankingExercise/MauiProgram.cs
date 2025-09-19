using CommunityToolkit.Maui;
using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;
using MauiBankingExercise.Views;
using Microsoft.Extensions.Logging;
using MauiBankingExercise.Interfaces;
using MauiBankingExercise.Configuration;

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
            builder.Services.AddSingleton<IBankService, BankApiService>();
            builder.Services.AddSingleton<ApplicationSettings>();

            builder.Services.AddTransient<AllCustomersViewModel>();
            builder.Services.AddTransient<SingleCustomerViewModel>();
            builder.Services.AddTransient<TransactionViewModel>();
            builder.Services.AddTransient<AddTransactionViewModel>();
            

            builder.Services.AddTransient<AllCustomersView>();
            builder.Services.AddTransient<SingleCustomerView>();
            builder.Services.AddTransient<TransactionView>();
            builder.Services.AddTransient<AddTransactionView>();



            return builder.Build();
        }
    }
}
