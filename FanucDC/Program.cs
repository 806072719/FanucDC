namespace FanucDC
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
            //Application.Run(new MainForm());
        }
    }
}