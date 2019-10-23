//-----------------------------------------------------------------------
// <copyright file="App.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeClientConsole
{
    using System;
    using System.Threading.Tasks;
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="App"/> class.
    /// </summary>
    public class App
    {
        /// <summary>
        /// The player client.
        /// </summary>
        private PlayerClient player;

        /// <summary>
        /// The input watcher.
        /// </summary>
        private IInputWatcher inputWatcher;

        /// <summary>
        /// The renderer.
        /// </summary>
        private IRenderer renderer;

        /// <summary>
        /// The input validator.
        /// </summary>
        private InputValidator validator;

        /// <summary>
        /// The window watcher.
        /// </summary>
        private WindowWatcher windowWatcher;

        /// <summary>
        /// The input validator for IP input.
        /// </summary>
        private InputValidatorForIPInput inputValidatorForIpInput;

        /// <summary>
        /// The IP address creator.
        /// </summary>
        private IpAdressCreator ipAdressCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="keyInputWatcher"> The <see cref="IInputWatcher"/>. </param>
        /// <param name="renderer"> The <see cref="IRenderer"/>. </param>
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

        /// <summary>
        /// This method starts the client.
        /// </summary>
        public void Start()
        {
            this.inputWatcher.Start();

            while (true)
            {
            }
        }

        /// <summary>
        /// This method exits the console.
        /// </summary>
        public void Exit()
        {
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.player.Stop());
            ts.StartNew(() => this.inputWatcher.Stop());
            ts.StartNew(() => this.windowWatcher.Stop());
            Environment.Exit(0);
        }

        /// <summary>
        /// This method starts the client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        private void StartClient(object sender, MessageContainerEventArgs e)
        {
            var adress = IPHelper.GetIPAdress(e.MessageContainer.Message);
            this.player = new PlayerClient(adress);
            this.player.OnErrorMessageReceived += this.renderer.PrintErrorMessage;
            this.player.OnErrorMessageReceived += this.FatalErrorExit;
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

        /// <summary>
        /// This method exits the client on error.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        private void ExitAppOnError(object sender, MessageContainerEventArgs e)
        {
            this.renderer.PrintErrorMessage(this, new MessageContainerEventArgs(new MessageContainer("Error Ip Adress couldnt be parsed or is wrong.")));
            Environment.Exit(1);
        }

        /// <summary>
        /// This method exits the client on error.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        private void FatalErrorExit(object sender, MessageContainerEventArgs e)
        {
            Environment.Exit(1);
        }

        /// <summary>
        /// This method sends the input to the server.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientSnakeMovementEventArgs"/>. </param>
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

        /// <summary>
        /// This method catches an disconnect.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void CatchDisconnect(object sender, EventArgs e)
        {
            this.renderer.PrintErrorMessage(this, new MessageContainerEventArgs(new MessageContainer("Error server has disconnected.")));
            TaskFactory ts = new TaskFactory();
            ts.StartNew(() => this.player.Stop()); 
            ts.StartNew(() => this.inputWatcher.Stop()); 
            ts.StartNew(() => this.windowWatcher.Stop()); 
            Environment.Exit(1);
        }
    }
}