using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFClientSnake
{
    using System.ComponentModel;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Commands;
    using NetworkLibrary;

    public class PlayerVM : INotifyPropertyChanged
    {
        private PlayerClient player;
        private IPEndPoint ipadress;
        private List<List<char>> textBoxChars;
        private string status;
        private Color color;
        private int snakeLength;
        private int points;
        private string textBoxText;

        public PlayerVM()
        {
            this.Status = string.Empty;
            this.MessageColor = Colors.White;
            this.textBoxChars = new List<List<char>>();
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
                if (IPHelper.IsIPAdress(value))
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

        public Color MessageColor
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

        public string TextBoxText
        {
            get { return this.textBoxText; }
            set
            {
                this.textBoxText = value;
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
                MessageBox.Show("Disconnected");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void GetErrorMessage(object sender, MessageContainerEventArgs e)
        {
            this.textBoxChars.Clear();
            this.TextBoxText = string.Empty;
            this.Status = e.MessageContainer.Message;
            this.MessageColor = Colors.Green;
        }

        public void GetMessage(object sender, MessageContainerEventArgs e)
        {
            this.textBoxChars.Clear();
            this.TextBoxText = string.Empty;
            this.Status = e.MessageContainer.Message;
            this.MessageColor = Colors.Red;
        }

        public void GetField(object sender, FieldMessageEventArgs e)
        {
            this.Status = string.Empty;
            //TODO: Mit Canvas Propbieren
            for (int i = 0; i < e.FieldPrintContainer.Height; i++)
            {
                List<char> temp = new List<char>();
                for (int j = 0; j < e.FieldPrintContainer.Width; j++)
                {
                    if (i == 0 || i == e.FieldPrintContainer.Height - 1)
                    {
                        temp.Add(e.FieldPrintContainer.Symbol);
                    }
                    else if (j == 0 || j== e.FieldPrintContainer.Width - 1)
                    {
                        temp.Add(e.FieldPrintContainer.Symbol);
                    }
                    else
                    {
                        temp.Add(' ');
                    }
                }

                this.textBoxChars.Add(temp);
            }

            foreach (var list in this.textBoxChars)
            {
                foreach (var element in list)
                {
                    this.TextBoxText = this.TextBoxText + element.ToString();
                }

                this.TextBoxText = this.TextBoxText + "\n";
            }
        }

        public void GetObjects(object sender, ObjectPrintEventArgs e)
        {
            //TODO: Mit Canvas Propbieren
            foreach (var element in e.ObjectPrintContainer.OldItems)
            {
                this.textBoxChars[element.PosInField.Y + 1][element.PosInField.X + 1] = ' ';
            }

            foreach (var element in e.ObjectPrintContainer.NewItems)
            {
                this.textBoxChars[element.PosInField.Y + 1][element.PosInField.X + 1] = ' ';
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