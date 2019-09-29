using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFClientSnake
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Commands;
    using NetworkLibrary;

    public class PlayerVM : INotifyPropertyChanged
    {
        private PlayerClient player;
        private IPEndPoint ipadress;
        private ObservableCollection<ObservableCollection<GameObject>> textBoxChars;
        private string status;
        private Brush color;
        private int snakeLength;
        private int points;

        /// <summary>
        /// The current dispatcher.
        /// </summary>
        private readonly Dispatcher current;

        private bool isConnected;

        public PlayerVM()
        {
            this.current = App.Current.Dispatcher;
            this.Status = string.Empty;
            this.MessageColor = Brushes.White;
            this.textBoxChars = new ObservableCollection<ObservableCollection<GameObject>>();
            this.IsConnected = false;

        }

        public ObservableCollection<ObservableCollection<GameObject>> TextBoxList
        {
            get { return this.textBoxChars; }
            private set
            {
                this.textBoxChars = value; 
                this.FirePropertyChanged();
            }
        }

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

        public int SnakeLength
        {
            get { return this.snakeLength; }
            private set
            {
                this.snakeLength = value;
                this.FirePropertyChanged();
            }
        }

        public int Points
        {
            get { return this.points; }
            private set
            {
                this.points = value;
                this.FirePropertyChanged();
            }
        }

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
                        this.ipadress = null;
                        MessageBox.Show("Successfully Disconnected.");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

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

                    if (this.player != null)
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
                        MessageBox.Show("Successufully connected.");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        private void GetDisconnectFromClient(object sender, EventArgs e)
        {
            try
            {
                this.player.Stop();
                this.player = null;
                this.ipadress = null;
                this.status = string.Empty;
                this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
                MessageBox.Show("Disconnected");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void GetErrorMessage(object sender, MessageContainerEventArgs e)
        {
            this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
            this.Status = e.MessageContainer.Message;
            this.MessageColor = Brushes.Red;
        }

        public void GetMessage(object sender, MessageContainerEventArgs e)
        {
            this.current.Invoke(new Action(() => { this.TextBoxList.Clear(); }));
            this.Status = e.MessageContainer.Message;
            this.MessageColor = Brushes.Green;
        }

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
                        temp.Add(new GameObject(new ObjectPrintContainer(e.FieldPrintContainer.Symbol, new Position(j, i), ConsoleColor.Black)));
                    }
                    else if (j == 0 || j == e.FieldPrintContainer.Width - 1)
                    {
                        temp.Add(new GameObject(new ObjectPrintContainer(e.FieldPrintContainer.Symbol, new Position(j, i), ConsoleColor.Black)));
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

        public void GetObjects(object sender, ObjectPrintEventArgs e)
        {
            this.Points = e.ObjectPrintContainer.Information.Points;
            this.SnakeLength = e.ObjectPrintContainer.Information.SnakeLength;

            foreach (var element in e.ObjectPrintContainer.OldItems)
            {
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetColor(ConsoleColor.White);
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetCharacter(' ');
            }

            foreach (var element in e.ObjectPrintContainer.NewItems)
            {
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetColor(element.Color);
                this.TextBoxList[element.PosInField.Y + 1][element.PosInField.X + 1].SetCharacter(element.ObjectChar);
            }
        }

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



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}