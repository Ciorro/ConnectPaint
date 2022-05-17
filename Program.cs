using Connect.Layouts;
using Connect.Widgets;
using HlyssUI;
using HlyssUI.Components.Routers;
using HlyssUI.Styling;
using HlyssUI.Themes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Reflection;

namespace Connect
{
    class Program
    {
        public static HlyssApplication App = new HlyssApplication();

        static void Main(string[] args)
        {
            HlyssApplication.InitializeStyles();
            HlyssApplication.LoadDefaultTheme();
            StyleBank.LoadFromFile(GetFullPath("Data/style.xml"));

            ThemeManager.SetTheme("dark");

            HlyssForm.WindowSettings.AntialiasingLevel = 8;

            HlyssForm form = new HlyssForm()
            {
                Size = new Vector2u(1280, 720),
                Title = "ConnectPaint",
                Icon = new Image(GetFullPath("Data/favicon.png"))
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

            if (args.Length == 1)
            {
                Console.WriteLine(args[0]);
                App.UpdateAllForms();

                if (File.Exists(args[0]))
                {
                    //Load file contents
                    var content = File.ReadAllText(args[0]);

                    //Get canvas
                    var canvas = form.Root.FindChild("canvas") as Canvas;

                    canvas.Import(content);
                }
            }

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

        public static string GetFullPath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }
    }
}
