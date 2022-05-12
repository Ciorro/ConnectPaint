using Connect.Dialogs;
using Connect.Layouts;
using HlyssUI;
using HlyssUI.Components.Routers;
using HlyssUI.Styling;
using HlyssUI.Themes;
using SFML.System;
using SFML.Window;

namespace Connect
{
    class Program
    {
        public static HlyssApplication App = new HlyssApplication();

        static void Main(string[] args)
        {
            HlyssApplication.InitializeStyles();
            HlyssApplication.LoadDefaultTheme();
            StyleBank.LoadFromFile("Data/style.xml");

            ThemeManager.SetTheme("dark");

            HlyssForm.WindowSettings.AntialiasingLevel = 8;

            HlyssForm form = new HlyssForm()
            {
                Size = new Vector2u(1280, 720),
                Title = "ConnectPaint"
            };

            form.Show();
            form.Window.SetFramerateLimit(60);

            form.Root.Children.Add(new BasicRouter()
            {
                Name = "router"
            });

            (form.Root.GetChild("router") as Router).Navigate(new Main());

            form.Window.KeyPressed += (_, __) =>
            {
                if (__.Code == Keyboard.Key.F12)
                    HlyssApplication.Debug = !HlyssApplication.Debug;
                if (__.Code == Keyboard.Key.C)
                    Console.Clear();
            };

            App.RegisterForm("main_form", form);

            while (form.IsOpen)
            {
                try
                {
                    App.UpdateAllForms();
                    App.DrawAllForms();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n\\!/ ERROR: {e.Message}\n[{e}]\n");
                }
            }
        }
    }
}
