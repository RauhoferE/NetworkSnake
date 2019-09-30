//-----------------------------------------------------------------------
// <copyright file="Application.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace SnakeServer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using NetworkLibrary;
    using Snake_V_0_3;

    /// <summary>
    /// The <see cref="Application"/> class.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Is true if the game is paused.
        /// </summary>
        private bool isGamePaused;

        /// <summary>
        /// Is true if the game is over.
        /// </summary>
        private bool isGameOver;

        /// <summary>
        /// The server host.
        /// </summary>
        private ServerHost serverHost;

        /// <summary>
        /// The game app.
        /// </summary>
        private Snake_V_0_3.Application gameApplication;

        /// <summary>
        /// The movement manager.
        /// </summary>
        private MovementManager movementManager;

        /// <summary>
        /// The renderer.
        /// </summary>
        private IRenderer renderer;

        /// <summary>
        /// The window watcher.
        /// </summary>
        private WindowWatcher windowWatcher;

        /// <summary>
        /// Is true if the server is running.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// A task factory.
        /// </summary>
        private TaskFactory taskFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="renderer"> The <see cref="IRenderer"/>. </param>
        public Application(IRenderer renderer)
        {
            this.renderer = renderer;
            this.windowWatcher = new WindowWatcher(renderer.Width, renderer.Height);
            this.serverHost = new ServerHost(80);
            this.isGamePaused = false;
            this.isGameOver = true;
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

            this.gameApplication.OnContainerCreated += this.SendGameObjectsToClients;
            this.gameApplication.OnGameStart += this.SendFieldToClients;
        }

        /// <summary>
        /// Starts the app.
        /// </summary>
        public void Start()
        {
            this.isRunning = true;

            try
            {
                this.serverHost.Start();
                this.windowWatcher.Start();

                while (this.isRunning)
                {
                }
            }
            catch (Exception e)
            {
                this.renderer.PrintMessage(e.Message);
            }
        }

        /// <summary>
        /// Stops the app.
        /// </summary>
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

        /// <summary>
        /// Pauses the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void PauseGame(object sender, EventArgs e)
        {
            if (!this.isGameOver)
            {
                this.isGamePaused = true;
                this.taskFactory.StartNew(() => this.gameApplication.Pause(this, EventArgs.Empty));
                this.taskFactory.StartNew(() => this.movementManager.ClearEntrys());
                this.taskFactory.StartNew(() => this.movementManager.Stop());
            }
        }

        /// <summary>
        /// Is called when the game is over.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void GameOver(object sender, EventArgs e)
        {
            this.isGameOver = true;
            this.taskFactory.StartNew(() => this.SendGameOverScreen());
            this.taskFactory.StartNew(() => this.movementManager.ClearEntrys());
            this.taskFactory.StartNew(() => this.movementManager.Stop());
        }

        /// <summary>
        /// This method sends a game over screen to the clients.
        /// </summary>
        private void SendGameOverScreen()
        {
            while (this.isGameOver)
            {
                this.serverHost.SendToClients(NetworkSerealizer.SerealizeMessage(new MessageContainer("Game over press any key to start a new game.")));
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        private void ResumeGame(object sender, ClientIDEventArgs e)
        {
            if (this.isGamePaused && !this.isGameOver)
            {
                this.isGamePaused = false;

                this.movementManager.ClearEntrys();
                this.movementManager.Start();
                this.gameApplication.Resume(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void RestartGame(object sender, EventArgs e)
        {
            if (this.isGameOver)
            {
                this.isGameOver = false;
                this.gameApplication.Start(this, EventArgs.Empty);
                this.movementManager.Start();
            }
        }

        /// <summary>
        /// Sends the field to specific client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientIDEventArgs"/>. </param>
        private void SendFieldToClient(object sender, ClientIDEventArgs e)
        {
            if (this.isGameOver)
            {
                this.serverHost.SendToClient(NetworkSerealizer.SerealizeMessage(new MessageContainer("Press any key to start a new game")), e.ClientID);
            }
            else
            {
                var field = this.gameApplication.GetField();

                this.serverHost.SendToClient(NetworkSerealizer.SerealizeField(new FieldPrintContainer(field.Width, field.Length, field.Icon.Character)), e.ClientID);
            }
        }

        /// <summary>
        /// Sends fields to client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void SendFieldToClients(object sender, EventArgs e)
        {
            var field = this.gameApplication.GetField();
            this.serverHost.SendToClients(NetworkSerealizer.SerealizeField(new FieldPrintContainer(field.Width, field.Length, field.Icon.Character)));
        }

        /// <summary>
        /// This method sends all the objects to the client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="GameOBjectListEventArgs"/>. </param>
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