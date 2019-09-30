//-----------------------------------------------------------------------
// <copyright file="Application.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace Snake_V_0_3
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// The <see cref="Application"/> class.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// The collision manager.
        /// </summary>
        private CollisionManager collisionManager;

        /// <summary>
        /// The power up manager.
        /// </summary>
        private PowerupManager powerupManager;

        /// <summary>
        /// The game field. 
        /// </summary>
        private PlayingField field;

        /// <summary>
        /// The object creation thread.
        /// </summary>
        private ObjectCreationThread objectCreationThread;

        /// <summary>
        /// The object placement thread.
        /// </summary>
        private ObjectPlacementChecker objectPlacementChecker;

        /// <summary>
        /// The snake mover.
        /// </summary>
        private SnakeMover mover;

        /// <summary>
        /// The score board.
        /// </summary>
        private ScoreBoard scoreBoard;

        /// <summary>
        /// The object container.
        /// </summary>
        private ObjectContainer objectContainer;

        /// <summary>
        /// The task factory.
        /// </summary>
        private TaskFactory taskFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application()
        {
            this.taskFactory = new TaskFactory();
            this.collisionManager = new CollisionManager();
            this.powerupManager = new PowerupManager();
            this.field = new PlayingField(29, 119, new Icon('~'));
            this.objectCreationThread = new ObjectCreationThread();
            this.objectPlacementChecker = new ObjectPlacementChecker();
            this.mover = new SnakeMover();
            this.scoreBoard = new ScoreBoard();
            this.objectContainer = new ObjectContainer();
        }

        /// <summary>
        /// This event should fire when a message has been received.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessageReceived;

        /// <summary>
        /// This event fires when an container has been created.
        /// </summary>
        public event EventHandler<GameOBjectListEventArgs> OnContainerCreated;

        /// <summary>
        /// This event fires when the game is over.
        /// </summary>
        public event EventHandler OnGameOver;

        /// <summary>
        /// This event fires when the game starts.
        /// </summary>
        public event EventHandler OnGameStart;

        /// <summary>
        /// This method starts the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void Start(object sender, EventArgs e)
        {
            this.collisionManager = new CollisionManager();
            this.powerupManager = new PowerupManager();
            this.field = new PlayingField(29, 119, new Icon('~'));
            this.objectCreationThread = new ObjectCreationThread();
            this.objectPlacementChecker = new ObjectPlacementChecker();
            this.mover = new SnakeMover();
            this.scoreBoard = new ScoreBoard();
            this.objectContainer = new ObjectContainer();

            this.FireOnGameStart();
            this.mover.OnSnakeMoved += this.CheckCollision;
            this.mover.OnSnakeMoved += this.scoreBoard.GetMovePoints;
            this.mover.OnSpeedChanged += this.scoreBoard.ChangeMultiplicator;

            this.mover.OnLastSegmentPassed += this.powerupManager.RemovePowerup;
            this.collisionManager.OnObsatcleCollided += this.GameOver;
            this.collisionManager.OnPowerUpCollided += this.powerupManager.CollisionHandler;
            this.objectCreationThread.Factory.OnObjectCreated += this.CheckPlacement;
            this.objectPlacementChecker.OnPlacementFound += this.powerupManager.AddPowerup;

            // Factory 
            this.powerupManager.OnAppleTouched += this.mover.AddSegment;
            this.powerupManager.OnAppleTouched += this.scoreBoard.ChangeScore;
            this.mover.OnLastSegmentPassed += this.scoreBoard.ChangeScore;
            this.powerupManager.OnPowerUpAdded += this.objectContainer.GetNewPowerUp;
            this.powerupManager.OnPowerUpRemoved += this.objectContainer.RemoveOldPowerup;
            this.powerupManager.OnSegmentDestroyerTouched += this.mover.RemoveSegment;

            this.powerupManager.OnRainbowTouched += this.mover.ChangeColor;
            this.powerupManager.StopPowerUpProduction += this.objectCreationThread.StopAll;
            this.powerupManager.StartPowerUpProduction += this.objectCreationThread.StartAll;

            this.objectContainer.OnPrintGameList += this.ReturngameObject;

            this.mover.OnSnakeMoved += this.objectContainer.GetNewSnakePosition;
            this.scoreBoard.OnScoreChange += this.objectContainer.GetNewScore;

            this.mover.Start();
            this.objectCreationThread.Start();
            this.powerupManager.StartPowerUpWatcher();
            this.objectContainer.Start();
        }

        /// <summary>
        /// This method stops the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void Stop(object sender, EventArgs e)
        {
            this.taskFactory.StartNew(() => this.mover.Stop());
            this.taskFactory.StartNew(() => this.objectCreationThread.Stop());
            this.taskFactory.StartNew(() => this.powerupManager.StopPowerUpWatcher());
            this.taskFactory.StartNew(() => this.objectContainer.Stop());
            Environment.Exit(0);
        }

        /// <summary>
        /// This method resumes the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void Resume(object sender, EventArgs e)
        {
            this.taskFactory.StartNew(() => this.mover.Start());
            this.taskFactory.StartNew(() => this.objectCreationThread.Start());
            this.taskFactory.StartNew(() => this.powerupManager.StartPowerUpWatcher());
            this.taskFactory.StartNew(() => this.objectContainer.Start());
        }

        /// <summary>
        /// This method pauses the game.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void Pause(object sender, EventArgs e)
        {
            this.taskFactory.StartNew(() => this.mover.Stop());
            this.taskFactory.StartNew(() => this.objectCreationThread.Stop());
            this.taskFactory.StartNew(() => this.powerupManager.StopPowerUpWatcher());
            this.taskFactory.StartNew(() => this.objectContainer.Stop());
        }

        /// <summary>
        /// This method gets the new snake direction.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="DirectionEventArgs"/>. </param>
        public void GetInput(object sender, DirectionEventArgs e)
        {
            this.mover.ChangeDirection(this, e);
        }

        /// <summary>
        /// This method gets the current field.
        /// </summary>
        /// <returns> It returns a new game field. </returns>
        public PlayingField GetField()
        {
            return this.field;
        }

        /// <summary>
        /// This method returns the new game container.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="GameOBjectListEventArgs"/>. </param>
        public void ReturngameObject(object sender, GameOBjectListEventArgs e)
        {
            this.FireOnContainerCreated(e);
        }

        /// <summary>
        /// This method fires the <see cref="OnMessageReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        protected virtual void FireOnMessageReceived(StringEventArgs e)
        {
            this.OnMessageReceived?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnContainerCreated"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="GameOBjectListEventArgs"/>. </param>
        protected virtual void FireOnContainerCreated(GameOBjectListEventArgs e)
        {
            this.OnContainerCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnGameOver"/> event.
        /// </summary>
        protected virtual void FireOnGameOver()
        {
            this.OnGameOver?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnGameStart"/> event.
        /// </summary>
        protected virtual void FireOnGameStart()
        {
            this.OnGameStart?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method is called when the game is over.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void GameOver(object sender, EventArgs e)
        {
            this.taskFactory.StartNew(() => this.mover.Stop());
            this.taskFactory.StartNew(() => this.objectCreationThread.Stop());
            this.taskFactory.StartNew(() => this.powerupManager.StopPowerUpWatcher());
            this.taskFactory.StartNew(() => this.objectContainer.Stop());
            this.FireOnMessageReceived(new StringEventArgs("Game Over Press any Key to start new."));
            this.FireOnGameOver();
        }

        /// <summary>
        /// This method is called when the placement should be checked.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StaticObjectEventArgs"/>. </param>
        private void CheckPlacement(object sender, StaticObjectEventArgs e)
        {
            var snakeList = this.mover.GetCurrentSnakeSegmentList();
            var powerUpList = this.powerupManager.GetPowerUps();
            List<GameObjects> final = new List<GameObjects>();

            foreach (var segment in snakeList)
            {
                final.Add(segment);
            }

            foreach (var powerup in powerUpList)
            {
                final.Add(powerup);
            }

            this.objectPlacementChecker.CheckPlacement(this.field, final, e.GameObject);
        }

        /// <summary>
        /// This method checks the collision.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="SnakeEventArgs"/>. </param>
        private void CheckCollision(object sender, SnakeEventArgs e)
        {
            this.collisionManager.CheckCollision(this.field, this.powerupManager.GetPowerUps(), e.Snake);
        }
    }
}