using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_V_0_3
{
    using System.Threading;
    using System.Threading.Tasks;

    public class Application
    {
        private CollisionManager collisionManager;
        private PowerupManager powerupManager;
        private PlayingField field;
        private ObjectCreationThread objectCreationThread;
        private ObjectPlacementChecker objectPlacementChecker;
        private SnakeMover mover;
        private ScoreBoard scoreBoard;
        private ObjectContainer objectContainer;
        private TaskFactory taskFactory;

        public Application()
        {
            this.taskFactory = new TaskFactory();
            collisionManager = new CollisionManager();
            powerupManager = new PowerupManager();
            field = new PlayingField(29, 119, new Icon('~'));
            objectCreationThread = new ObjectCreationThread();
            objectPlacementChecker = new ObjectPlacementChecker();
            mover = new SnakeMover();
            scoreBoard = new ScoreBoard();
            objectContainer = new ObjectContainer();
        }

        public event EventHandler<StringEventArgs> OnMessageReceived;

        public event EventHandler<StringEventArgs> OnErrorMessageReceived;

        public event EventHandler<GameOBjectListEventArgs> OnContainerCreated;

        public event EventHandler<FieldEventArgs> OnFieldPrint;

        public event EventHandler OnGameOver;

        public event EventHandler OnGameStart;

        public void Start(object sender, EventArgs e)
        {
            collisionManager = new CollisionManager();
            powerupManager = new PowerupManager();
            field = new PlayingField(29, 119, new Icon('~'));
            objectCreationThread = new ObjectCreationThread();
            objectPlacementChecker = new ObjectPlacementChecker();
            mover = new SnakeMover();
            scoreBoard = new ScoreBoard();
            objectContainer = new ObjectContainer();

            this.FireOnGameStart();
            mover.OnSnakeMoved += CheckCollision;
            mover.OnSnakeMoved += scoreBoard.GetMovePoints;
            mover.OnSpeedChanged += scoreBoard.ChangeMultiplicator;

            mover.OnLastSegmentPassed += powerupManager.RemovePowerup;
            collisionManager.OnObsatcleCollided += GameOver;
            collisionManager.OnPowerUpCollided += powerupManager.CollisionHandler;
            objectCreationThread.Factory.OnObjectCreated += CheckPlacement;
            objectPlacementChecker.OnPlacementFound += powerupManager.AddPowerup;

            //Factory 
            powerupManager.OnAppleTouched += mover.AddSegment;
            powerupManager.OnAppleTouched += scoreBoard.ChangeScore;
            mover.OnLastSegmentPassed += scoreBoard.ChangeScore;
            powerupManager.OnPowerUpAdded += objectContainer.GetNewPowerUp;
            powerupManager.OnPowerUpRemoved += objectContainer.RemoveOldPowerup;
            powerupManager.OnSegmentDestroyerTouched += mover.RemoveSegment;

            powerupManager.OnRainbowTouched += mover.ChangeColor;
            powerupManager.StopPowerUpProduction += objectCreationThread.StopAll;
            powerupManager.StartPowerUpProduction += objectCreationThread.StartAll;

            objectContainer.OnPrintGameList += this.ReturngameObject;

            mover.OnSnakeMoved += objectContainer.GetNewSnakePosition;
            scoreBoard.OnScoreChange += objectContainer.GetNewScore;

            mover.Start();
            objectCreationThread.Start();
            powerupManager.StartPowerUpWatcher();
            objectContainer.Start();
        }

        public void Stop(object sender, EventArgs e)
        {
            taskFactory.StartNew(() => mover.Stop());
            taskFactory.StartNew(() => objectCreationThread.Stop());
            taskFactory.StartNew(() => powerupManager.StopPowerUpWatcher());
            taskFactory.StartNew(() => objectContainer.Stop());
            Environment.Exit(0);
        }

        public void Resume(object sender, EventArgs e)
        {
            taskFactory.StartNew(() => mover.Start());
            taskFactory.StartNew(() => objectCreationThread.Start());
            taskFactory.StartNew(() => powerupManager.StartPowerUpWatcher());
            taskFactory.StartNew(() => objectContainer.Start());
        }

        public void Pause(object sender, EventArgs e)
        {
            taskFactory.StartNew(() => mover.Stop());
            taskFactory.StartNew(() => objectCreationThread.Stop());
            taskFactory.StartNew(() => powerupManager.StopPowerUpWatcher());
            taskFactory.StartNew(() => objectContainer.Stop());
        }

        public void GetInput(object sender, DirectionEventArgs e)
        {
            this.mover.ChangeDirection(this, e);
        }

        public PlayingField GetField()
        {
            return this.field;
        }

        public void ReturngameObject(object sender, GameOBjectListEventArgs e)
        {
            this.FireOnContainerCreated(e);
        }

        private void GameOver(object sender, EventArgs e)
        {
            taskFactory.StartNew(() => mover.Stop());
            taskFactory.StartNew(() => objectCreationThread.Stop());
            taskFactory.StartNew(() => powerupManager.StopPowerUpWatcher());
            taskFactory.StartNew(() => objectContainer.Stop());
            this.FireOnMessageReceived(new StringEventArgs("Game Over Press any Key to start new."));
            this.FireOnGameOver();
        }

        private void CheckPlacement(object sender, StaticObjectEventArgs e)
        {
            var snakeList = mover.GetCurrentSnakeSegmentList();
            var powerUpList = powerupManager.GetPowerUps();
            List<GameObjects> final = new List<GameObjects>();

            foreach (var segment in snakeList)
            {
                final.Add(segment);
            }

            foreach (var powerup in powerUpList)
            {
                final.Add(powerup);
            }

            objectPlacementChecker.CheckPlacement(field, final, e.GameObject);
        }

        private void CheckCollision(object sender, SnakeEventArgs e)
        {
            collisionManager.CheckCollision(field, powerupManager.GetPowerUps(), e.Snake);
        }

        protected virtual void FireOnMessageReceived(StringEventArgs e)
        {
            OnMessageReceived?.Invoke(this, e);
        }

        protected virtual void FireOnErrorMessageReceived(StringEventArgs e)
        {
            OnErrorMessageReceived?.Invoke(this, e);
        }

        protected virtual void FireOnContainerCreated(GameOBjectListEventArgs e)
        {
            OnContainerCreated?.Invoke(this, e);
        }

        protected virtual void FireOnFieldPrint(FieldEventArgs e)
        {
            OnFieldPrint?.Invoke(this, e);
        }

        protected virtual void FireOnGameOver()
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireOnGameStart()
        {
            OnGameStart?.Invoke(this, EventArgs.Empty);
        }
    }
}