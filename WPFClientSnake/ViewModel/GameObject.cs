//-----------------------------------------------------------------------
// <copyright file="GameObject.cs" company="FH Wiener Neustadt">
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
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="GameObject"/> class.
    /// </summary>
    public class GameObject : INotifyPropertyChanged
    {
        /// <summary>
        /// The x position.
        /// </summary>
        private int posX;

        /// <summary>
        /// The y position.
        /// </summary>
        private int posY;

        /// <summary>
        /// The icon of the object.
        /// </summary>
        private string icon;

        /// <summary>
        /// The color of the object.
        /// </summary>
        private Brush color;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="element"> The <see cref="ObjectPrintContainer"/>. </param>
        public GameObject(ObjectPrintContainer element)
        {
            this.PosX = element.PosInField.X;
            this.PosY = element.PosInField.Y;
            this.Icon = element.ObjectChar.ToString();
            this.Color = this.ReturnColor(element.Color);
        }

        /// <summary>
        /// This event fires when a property has been changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets x position.
        /// </summary>
        /// <value> A normal integer. </value>
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

        /// <summary>
        /// Gets y position.
        /// </summary>
        /// <value> A normal integer. </value>
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

        /// <summary>
        /// Gets object icon.
        /// </summary>
        /// <value> A normal string. </value>
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

        /// <summary>
        /// Gets object color.
        /// </summary>
        /// <value> A normal color. </value>
        public Brush Color
        {
            get
            {
                return this.color;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error");
                }

                this.color = value;
                this.FirePropertyChanged();
            }
        }

        /// <summary>
        /// This method sets the character.
        /// </summary>
        /// <param name="character"> The character to be set. </param>
        public void SetCharacter(char character)
        {
            this.Icon = character.ToString();
        }

        /// <summary>
        /// This method sets the color.
        /// </summary>
        /// <param name="consoleColor"> The color to be set. </param>
        public void SetColor(ConsoleColor consoleColor)
        {
            this.Color = this.ReturnColor(consoleColor);
        }

        /// <summary>
        /// This event fires the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"> The property name. </param>
        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Converts console color to color.
        /// </summary>
        /// <param name="consoleColor"> The <see cref="ConsoleColor"/>. </param>
        /// <returns> It returns a new <see cref="Brush"/>. </returns>
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
    }
}
