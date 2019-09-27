using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeServer
{
    using System.Threading;
    using System.Threading.Tasks;
    using NetworkLibrary;

    using Snake_V_0_3;

    public class Application
    {
        private bool IsGamePaused;

        private bool IsGameOver;

        private ServerHost serverHost;

        private Snake_V_0_3.Application gameApplication;

        private MovementManager movementManager;

        private IRenderer renderer;

        private WindowWatcher windowWatcher;
        private bool isRunning;
        private TaskFactory taskFactory;

        public Application(IRenderer renderer)
        {
            this.renderer = renderer;
            this.windowWatcher = new WindowWatcher(renderer.Width, renderer.Height);
            this.serverHost = new ServerHost(80);
            this.IsGamePaused = false;
            this.IsGameOver = true;
            this.gameApplication = new Snake_V_0_3.Application();
            this.movementManager = new MovementManager();
            this.isRunning = false;
            this.taskFactory = new TaskFactory();
            this.serverHost.OnClientConnected += renderer.PrintClientConnectInfo;

            this.serverHost.OnClientConnected += this.SendFieldToClient;
            this.serverHost.OnClientConnected += this.ResumeGame;

            this.serverHost.OnClientDisconnect += renderer.PrintClientDisConnectInfo;
            this.serverHost.OnSnakeMovementReceived += renderer.PrintSnakeMovementReceived;
            this.serverHost.OnSnakeMovementReceived += this.movementManager.GetMovement;

            this.movementManager.OnAnyKeyReceived += this.RestartGame;
            this.movementManager.OnSendMovement += this.gameApplication.GetInput;
            this.serverHost.OnNoClientConnected += this.PauseGame;

            this.gameApplication.OnGameOver += this.GameOver;
            this.gameApplication.OnGameOver += this.renderer.PrintGameOver;
            this.gameApplication.OnErrorMessageReceived += this.renderer.PrintErrorMessage;

            this.gameApplication.OnContainerCreated += this.SendGameObjectsToClients;
            this.gameApplication.OnGameStart += this.SendFieldToClients;
        }

        private void PauseGame(object sender, EventArgs e)
        {
            this.IsGamePaused = true;
            this.taskFactory.StartNew(() => this.gameApplication.Pause(this, EventArgs.Empty));
            this.taskFactory.StartNew(() => this.movementManager.ClearEntrys());
            this.taskFactory.StartNew(() => this.movementManager.Stop());
        }

        private void GameOver(object sender, EventArgs e)
        {
            this.IsGameOver = true;
            this.taskFactory.StartNew(() => this.SendGameOverScreen());
            this.taskFactory.StartNew(() => this.movementManager.ClearEntrys());
            this.taskFactory.StartNew(() => this.movementManager.Stop());
        }

        private void SendGameOverScreen()
        {
            while (this.IsGameOver)
            {
                this.serverHost.SendToClients(NetworkSerealizer.SerealizeMessage(new MessageContainer("Game over press any key to start a new game.")));
                Thread.Sleep(500);
            }
        }

        private void ResumeGame(object sender, ClientIDEventArgs e)
        {
            if (this.IsGamePaused && !this.IsGameOver)
            {
                this.IsGamePaused = false;

                this.movementManager.ClearEntrys();
                this.movementManager.Start();
                this.gameApplication.Resume(this, EventArgs.Empty);
            }
            //else if (!this.IsGamePaused && this.IsGameOver)
            //{
            //    this.IsGameOver = false;
            //    this.movementManager.Start();
            //    this.gameApplication.Start(this, EventArgs.Empty);
            //}
        }

        private void RestartGame(object sender, EventArgs e)
        {
            if (this.IsGameOver)
            {
                this.IsGameOver = false;
                this.gameApplication.Start(this, EventArgs.Empty);
                this.movementManager.Start();
            }
        }

        public void Start()
        {
            this.isRunning = true;

            try
            {
                this.serverHost.Start();
                this.windowWatcher.Start();

                while (isRunning)
                {
                    
                }
            }
            catch (Exception e)
            {
                this.renderer.PrintMessage(e.Message);
            }
        }

        public void Stop()
        {
            try
            {
                this.isRunning = false;
                this.taskFactory.StartNew(() => this.serverHost.Stop());
                this.taskFactory.StartNew(() => this.movementManager.Stop());
                this.taskFactory.StartNew(() => this.windowWatcher.Stop());
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                this.renderer.PrintMessage(e.Message);
            }
        }

        private void SendFieldToClient(object sender, ClientIDEventArgs e)
        {
            if (this.IsGameOver)
            {
                this.serverHost.SendToClient(NetworkSerealizer.SerealizeMessage(new MessageContainer("Press any key to start a new game")), e.ClientID);
            }
            else
            {
                var field = this.gameApplication.GetField();
                this.serverHost.SendToClient(NetworkSerealizer.SerealizeField(new FieldPrintContainer(field.Width, field.Length, field.Icon.Character)), e.ClientID);

            }
        }

        private void SendFieldToClients(object sender, EventArgs e)
        {
            var field = this.gameApplication.GetField();
            this.serverHost.SendToClients(NetworkSerealizer.SerealizeField(new FieldPrintContainer(field.Width, field.Length, field.Icon.Character)));
        }

        private void SendErrorMessageToClients(object sender, StringEventArgs e)
        {
            this.serverHost.SendToClients(NetworkSerealizer.SerealizeErrorMessage(new MessageContainer(e.Text)));
        }

        private void SendMessageToClients(object sender, StringEventArgs e)
        {
            this.serverHost.SendToClients(NetworkSerealizer.SerealizeMessage(new MessageContainer(e.Text)));
        }

        private void SendGameObjectsToClients(object sender, GameOBjectListEventArgs e)
        {
            List<ObjectPrintContainer> newContainer = new List<ObjectPrintContainer>();
            List<ObjectPrintContainer> oldContainer = new List<ObjectPrintContainer>();

            foreach (var gameObjectse in e.NewObjects)
            {
                newContainer.Add(new ObjectPrintContainer(gameObjectse.Icon.Character, new NetworkLibrary.Position(gameObjectse.Pos.X, gameObjectse.Pos.Y), gameObjectse.Color.ForeGroundColor));
            }

            foreach (var gameObjectse in e.OldObjects)
            {
                oldContainer.Add(new ObjectPrintContainer(gameObjectse.Icon.Character, new NetworkLibrary.Position(gameObjectse.Pos.X, gameObjectse.Pos.Y), gameObjectse.Color.ForeGroundColor));
            }

            this.serverHost.SendToClients(NetworkSerealizer.SerealizeGameObjects(new ObjectListContainer(oldContainer, newContainer, new GameInformationContainer(e.SnakeLength, e.Score))));
        }
    }
}