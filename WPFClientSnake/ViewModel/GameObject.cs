using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientSnake
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using NetworkLibrary;

    public class GameObject : INotifyPropertyChanged
    {
        private int posX;
        private int posY;
        private string icon;
        private Brush color;

        public GameObject(ObjectPrintContainer element)
        {
            this.PosX = element.PosInField.X;
            this.PosY = element.PosInField.Y;
            this.Icon = element.ObjectChar.ToString();
            this.Color = this.ReturnColor(element.Color);
        }

        public int PosX
        {
            get
            {
                return this.posX;
            }
            private set
            {
                this.posX = value;
                this.FirePropertyChanged();
            }
        }

        public int PosY
        {
            get
            {
                return this.posY;
            }
            private set
            {
                this.posY = value;
                this.FirePropertyChanged();
            }
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }
            private set
            {
                this.icon = value;
                this.FirePropertyChanged();
            }
        }

        public Brush Color
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

        public void SetCharacter(char character)
        {
            this.Icon = character.ToString();
        }

        public void SetColor(ConsoleColor consoleColor)
        {
            this.Color = this.ReturnColor(consoleColor);
        }

        private Brush ReturnColor(ConsoleColor consoleColor)
        {
            switch (consoleColor)
            {
                case ConsoleColor.Black:
                    return Brushes.Black;
                case ConsoleColor.DarkBlue:
                    return Brushes.DarkBlue;
                case ConsoleColor.DarkGreen:
                    return Brushes.DarkGreen;
                case ConsoleColor.DarkCyan:
                    return Brushes.DarkCyan;
                case ConsoleColor.DarkRed:
                    return Brushes.DarkRed;
                case ConsoleColor.DarkMagenta:
                    return Brushes.DarkMagenta;
                case ConsoleColor.DarkYellow:
                    return Brushes.DarkKhaki;
                case ConsoleColor.Gray:
                    return Brushes.Gray;
                case ConsoleColor.DarkGray:
                    return Brushes.DarkGray;
                case ConsoleColor.Blue:
                    return Brushes.Blue;
                case ConsoleColor.Green:
                    return Brushes.Green;
                case ConsoleColor.Cyan:
                    return Brushes.Cyan;
                case ConsoleColor.Red:
                    return Brushes.Red;
                case ConsoleColor.Magenta:
                    return Brushes.Magenta;
                case ConsoleColor.Yellow:
                    return Brushes.Yellow;
                case ConsoleColor.White:
                    return Brushes.White;
                default:
                    return Brushes.Black;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
