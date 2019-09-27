using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    using System.Diagnostics.Eventing.Reader;
    using System.Threading.Tasks;
    using NetworkLibrary;

    public class App
    {
        private PlayerClient player;
        private IInputWatcher inputWatcher;
        private IRenderer renderer;
        private InputValidator validator;
        private WindowWatcher windowWatcher;
        private InputValidatorForIPInput inputValidatorForIpInput;
        private IpAdressCreator ipAdressCreator;

        public App(IInputWatcher keyInputWatcher, IRenderer renderer)
        {
            this.inputWatcher = keyInputWatcher;
            this.renderer = renderer;
            this.windowWatcher = new WindowWatcher(renderer.WindowWidth, renderer.WindowHeight);
            this.windowWatcher.Start();
            this.validator = new InputValidator();
            this.ipAdressCreator = new IpAdressCreator();
            this.inputValidatorForIpInput = new InputValidatorForIPInput();


            this.inputWatcher.OnKeyInputReceived += this.ipAdressCreator.GetInput;
            this.ipAdressCreator.OnCharPressed += this.inputValidatorForIpInput.AddChar;
            this.ipAdressCreator.OnDeleteKeyPressed += this.inputValidatorForIpInput.DeleteLastEntry;
            this.ipAdressCreator.OnEnterPressed += this.inputValidatorForIpInput.SendIpAdress;
            this.inputValidatorForIpInput.OnKeyInput += this.renderer.PrintUserInput;
            this.inputValidatorForIpInput.OnDeleteKeyPressed += this.renderer.DeleteUserInput;
            this.inputValidatorForIpInput.OnErrorMessagePrint += this.ExitAppOnError;
            this.inputValidatorForIpInput.OnEnterPressed += this.StartClient;

            this.renderer.PrintMessage(this, new MessageContainerEventArgs(new MessageContainer("Please put in an ipadress.")));
        }

        private void StartClient(object sender, MessageContainerEventArgs e)
        {
            var adress = IPHelper.GetIPAdress(e.MessageContainer.Message);
            this.player = new PlayerClient(adress);
            this.player.OnErrorMessageReceived += this.renderer.PrintErrorMessage;
            this.player.OnFieldMessageReceived += this.renderer.PrintField;
            this.player.OnNormalTextReceived += this.renderer.PrintMessage;
            this.player.OnObjectListReceived += this.renderer.PrintGameObjectsAndInfo;
            this.player.OnServerDisconnect += this.CatchDisconnect;

            this.inputWatcher.OnKeyInputReceived -= this.ipAdressCreator.GetInput;
            this.inputWatcher.OnKeyInputReceived += this.validator.GetInput;
            this.validator.OnSnakeMoved += this.SendSnakeMovement;

            try
            {
                this.player.Start();
            }
            catch (Exception ex)
            {
                this.renderer.PrintErrorMessage(this, new MessageContainerEventArgs(new MessageContainer(ex.Message)));
                Environment.Exit(1);
            }
        }

        private void ExitAppOnError(object sender, MessageContainerEventArgs e)
        {
            this.renderer.PrintErrorMessage(this, new MessageContainerEventArgs(new MessageContainer("Error Ip Adress couldnt be parsed or is wrong.")));
            Environment.Exit(1);
        }

        public void Start()
        {
            this.inputWatcher.Start();

            while (true)
            {
            }
        }

        private void SendSnakeMovement(object sender, ClientSnakeMovementEventArgs e)
        {
            try
            {
               this.player.SendMessage(NetworkSerealizer.SerealizeMoveSnake(e.Container));
            }
            catch (Exception exception)
            {
                this.renderer.PrintErrorMessage(this, new MessageContainerEventArgs(new MessageContainer(exception.Message)));
                TaskFactory ts = new TaskFactory();
                ts.StartNew(() => this.player.Stop());
                ts.StartNew(() => this.inputWatcher.Stop());
                ts.StartNew(() => this.windowWatcher.Stop());
                Environment.Exit(1);
            }
        }

        private void CatchDisconnect(object sender, EventArgs e)
        {
            this.renderer.PrintErrorMessage(this, new MessageContainerEventArgs(new MessageContainer("Error server has disconnected.")));
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.player.Stop()); 
            ts.StartNew(() => this.inputWatcher.Stop()); 
            ts.StartNew(() => this.windowWatcher.Stop()); 
            Environment.Exit(1);
        }

        public void Exit()
        {
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.player.Stop());
            ts.StartNew(() => this.inputWatcher.Stop());
            ts.StartNew(() => this.windowWatcher.Stop());
            Environment.Exit(0);
        }
    }
}