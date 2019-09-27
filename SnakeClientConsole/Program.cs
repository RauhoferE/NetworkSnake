
namespace SnakeClientConsole
{

    public class Program
    {
        public static void Main(string[] args)
        {
            IInputWatcher watcher = new KeyBoardWatcher();
            IRenderer renderer = new ConsoleRenderer(120, 32);
            App app = new App(watcher, renderer);
            app.Start();
        }
    }
}
