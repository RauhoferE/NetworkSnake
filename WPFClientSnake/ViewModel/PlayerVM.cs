//-----------------------------------------------------------------------
// <copyright file="PlayerVM.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace WPFClientSnake
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="PlayerVM"/> class.
    /// </summary>
    public class PlayerVM : INotifyPropertyChanged
    {
        /// <summary>
        /// The current dispatcher.
        /// </summary>
        private readonly Dispatcher current;

        /// <summary>
        /// The client.
        /// </summary>
        private PlayerClient player;

        /// <summary>
        /// The IP endpoint.
        /// </summary>
        private IPEndPoint ipadress;

        /// <summary>
        /// The <see cref="GameObject"/> list.
        /// </summary>
        private ObservableCollection<ObservableCollection<GameObject>> textBoxChars;

        /// <summary>
        /// The status.
        /// </summary>
        private string status;

        /// <summary>
        /// The color of the status.
        /// </summary>
        private Brush color;

        /// <summary>
        /// The snake length.
        /// </summary>
        private int snakeLength;

        /// <summary>
        /// The snake points.
        /// </summary>
        private int points;

        /// <summary>
        /// Is true if the client is connected.
        /// </summary>
        private bool isConnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerVM"/> class.
        /// </summary>
        public PlayerVM()
        {
            this.current = App.Current.Dispatcher;
            this.Status = string.Empty;
            this.MessageColor = Brushes.White;
            this.textBoxChars = new ObservableCollection<ObservableCollection<GameObject>>();
            this.IsConnected = false;
        }

        /// <summary>
        /// This event fires when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the list of the list with the <see cref="GameObject"/>.
        /// </summary>
        /// <value> A list of a list. </value>
        public ObservableCollection<ObservableCollection<GameObject>> TextBoxList
        {
            get
            {
                return this.textBoxChars; 
            }

            private set
            {
                this.textBoxChars = value; 
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the text of the textbox.
        /// </summary>
        /// <value> A normal string. </value>
        public string IPAdress
        {
            get
            {
                if (this.ipadress == null)
                {
                    return string.Empty;
                }

                return this.ipadress.Address.ToString();
            }

            set
            {
                if (!IPHelper.IsIPAdress(value))
                {
                    throw new ArgumentException("Error please put in an IPAdress.");
                }

                this.ipadress = IPHelper.GetIPAdress(value);
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the status of the game.
        /// </summary>
        /// <value> A normal string. </value>
        public string Status
        {
            get
            {
                return this.status;
            }

            private set
            {
                this.status = value;
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the client is connected or not.
        /// </summary>
        /// <value> Is true if the client is connected. </value>
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }

            private set
            {
                this.isConnected = value;
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the color of the message.
        /// </summary>
        /// <value> A normal <see cref="Brush"/>. </value>
        public Brush MessageColor
        {
            get
            {
                return this.color;
            }

            private set
            {
                this.color = value;
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the snake length.
        /// </summary>
        /// <value> A normal integer. </value>
        public int SnakeLength
        {
            get
            {
                return this.snakeLength; 
            }

            private set
            {
                this.snakeLength = value;
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the game points.
        /// </summary>
        /// <value> A normal integer. </value>
        public int Points
        {
            get
            {
                return this.points; 
            }

            private set
            {
                this.points = value;
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a command that fires when the disconnect button is pressed.
        /// </summary>
        /// <value> A normal <see cref="ICommand"/>. </value>
        public ICommand Disconnect
        {
            get
            {
                return new Command(obj =>
                {
                    if (this.player == null)
                    {
                        MessageBox.Show("Already disconnected.");
                        return;
                    }

                    try
                    {
                        this.player.Stop();
                        this.player = null;
                        this.Status = string.Empty;
                        this.SnakeLength = 0;
                        this.Points = 0;
                        this.IsConnected = false;
                        this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
                        MessageBox.Show("Successfully Disconnected.");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        /// <summary>
        /// Gets a command that fires when the connect button is pressed.
        /// </summary>
        /// <value> A normal <see cref="ICommand"/>. </value>
        public ICommand ConnectToServer
        {
            get
            {
                return new Command(obj =>
                {
                    if (this.ipadress == null)
                    {
                        MessageBox.Show("Error put in a valid IPAdress.");
                        return;
                    }

                    if (this.player != null && this.player.IsAlive)
                    {
                        MessageBox.Show("Error already connected.");
                        return;
                    }

                    this.player = new PlayerClient(this.ipadress);
                    this.player.OnServerDisconnect += this.GetDisconnectFromClient;
                    this.player.OnNormalTextReceived += this.GetMessage;
                    this.player.OnErrorMessageReceived += this.GetErrorMessage;
                    this.player.OnFieldMessageReceived += this.GetField;
                    this.player.OnObjectListReceived += this.GetObjects;

                    try
                    {
                        this.player.Start();
                        this.IsConnected = true;
                        MessageBox.Show("Successufully connected.");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        /// <summary>
        /// This method prints the error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        public void GetErrorMessage(object sender, MessageContainerEventArgs e)
        {
            this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
            this.Status = e.MessageContainer.Message;
            this.MessageColor = Brushes.Red;
        }

        /// <summary>
        /// This method prints the message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        public void GetMessage(object sender, MessageContainerEventArgs e)
        {
            this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
            this.Status = e.MessageContainer.Message;
            this.MessageColor = Brushes.Green;
        }

        /// <summary>
        /// This method gets the field from the server.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="FieldMessageEventArgs"/>. </param>
        public void GetField(object sender, FieldMessageEventArgs e)
        {
            this.Status = string.Empty;
            ObservableCollection<ObservableCollection<GameObject>> finalList = new ObservableCollection<ObservableCollection<GameObject>>();
            
            for (int i = 0; i < e.FieldPrintContainer.Height; i++)
            {
                ObservableCollection<GameObject> temp = new ObservableCollection<GameObject>();

                for (int j = 0; j < e.FieldPrintContainer.Width; j++)
                {
                    if (i == 0 || i == e.FieldPrintContainer.Height - 1)
                    {
                        temp.Add(new GameObject(new ObjectPrintContainer(e.FieldPrintContainer.Symbol, new Position(j, i), ConsoleColor.White)));
                    }
                    else if (j == 0 || j == e.FieldPrintContainer.Width - 1)
                    {
                        temp.Add(new GameObject(new ObjectPrintContainer(e.FieldPrintContainer.Symbol, new Position(j, i), ConsoleColor.White)));
                    }
                    else
                    {
                        temp.Add(new GameObject(new ObjectPrintContainer(' ', new Position(j, i), ConsoleColor.White)));
                    }
                }

                finalList.Add(temp);
            }

            this.current.Invoke(new Action(() => { this.TextBoxList = finalList; }));
        }

        /// <summary>
        /// This method gets the objects from the server.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ObjectPrintEventArgs"/>. </param>
        public void GetObjects(object sender, ObjectPrintEventArgs e)
        {
            if (e.ObjectPrintContainer.Information == null && e.ObjectPrintContainer.NewItems == null && e.ObjectPrintContainer.OldItems == null)
            {
                return;
            }

            this.Points = e.ObjectPrintContainer.Information.Points;
            this.SnakeLength = e.ObjectPrintContainer.Information.SnakeLength;

            foreach (var element in e.ObjectPrintContainer.OldItems)
            {
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetColor(ConsoleColor.Black);
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetCharacter(' ');
            }

            foreach (var element in e.ObjectPrintContainer.NewItems)
            {
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetColor(element.Color);
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetCharacter(element.ObjectChar);
            }
        }

        /// <summary>
        /// This method sends the input to the server.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ClientSnakeMovementEventArgs"/>. </param>
        public void SendInputToClient(object sender, ClientSnakeMovementEventArgs e)
        {
            if (this.player == null)
            {
                return;
            }

            try
            {
                this.player.SendMessage(NetworkSerealizer.SerealizeMoveSnake(e.Container));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// This method fires the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"> The property name. </param>
        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// This method catches the disconnect.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        private void GetDisconnectFromClient(object sender, EventArgs e)
        {
            try
            {
                this.player.Stop();
                this.player = null;
                this.Status = string.Empty;
                this.SnakeLength = 0;
                this.Points = 0;
                this.IsConnected = false;
                this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
                MessageBox.Show("Disconnected");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}