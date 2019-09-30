//-----------------------------------------------------------------------
// <copyright file="IpAdressCreator.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="IpAdressCreator"/> class.
    /// </summary>
    public class IpAdressCreator
    {
        /// <summary>
        /// This event should fire when a delete key is pressed.
        /// </summary>
        public event EventHandler OnDeleteKeyPressed;

        /// <summary>
        /// This event should fire when a char has been pressed.
        /// </summary>
        public event EventHandler<CharEventArgs> OnCharPressed;

        /// <summary>
        /// This event should fire when a enter key is pressed.
        /// </summary>
        public event EventHandler OnEnterPressed;

        /// <summary>
        /// This method gets gets the input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void GetInput(object sender, ConsoleKeyEventArgs e)
        {
            switch (e.Key)
            {
                case ConsoleKey.Enter:
                    this.FireOnEnterPressed();
                    break;
                case ConsoleKey.Backspace:
                    this.FireOnDeleteKeyPressed();
                    break;
                case ConsoleKey.D1:
                    this.FireOnCharPressed(new CharEventArgs('1'));
                    break;
                case ConsoleKey.D2:
                    this.FireOnCharPressed(new CharEventArgs('2'));
                    break;
                case ConsoleKey.D3:
                    this.FireOnCharPressed(new CharEventArgs('3'));
                    break;
                case ConsoleKey.D4:
                    this.FireOnCharPressed(new CharEventArgs('4'));
                    break;
                case ConsoleKey.D5:
                    this.FireOnCharPressed(new CharEventArgs('5'));
                    break;
                case ConsoleKey.D6:
                    this.FireOnCharPressed(new CharEventArgs('6'));
                    break;
                case ConsoleKey.D7:
                    this.FireOnCharPressed(new CharEventArgs('7'));
                    break;
                case ConsoleKey.D8:
                    this.FireOnCharPressed(new CharEventArgs('8'));
                    break;
                case ConsoleKey.D9:
                    this.FireOnCharPressed(new CharEventArgs('9'));
                    break;
                case ConsoleKey.D0:
                    this.FireOnCharPressed(new CharEventArgs('0'));
                    break;
                case ConsoleKey.OemPeriod:
                    this.FireOnCharPressed(new CharEventArgs('.'));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method fire the <see cref="OnDeleteKeyPressed"/> event.
        /// </summary>
        protected virtual void FireOnDeleteKeyPressed()
        {
            this.OnDeleteKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnCharPressed"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="CharEventArgs"/>. </param>
        protected virtual void FireOnCharPressed(CharEventArgs e)
        {
            this.OnCharPressed?.Invoke(this, e);
        }

        /// <summary>
        /// This method fire the <see cref="OnEnterPressed"/> event.
        /// </summary>
        protected virtual void FireOnEnterPressed()
        {
            this.OnEnterPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}