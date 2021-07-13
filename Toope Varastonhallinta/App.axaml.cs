using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Toope_Varastonhallinta
{
    public class App : Application
    {
        public static Tietokantakonteksti DB = new Tietokantakonteksti();
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            DB.Database.EnsureCreated();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
