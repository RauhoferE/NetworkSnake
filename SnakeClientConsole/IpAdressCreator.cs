using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    public class IpAdressCreator
    {
        public event System.EventHandler OnDeleteKeyPressed;
        public event System.EventHandler<CharEventArgs> OnCharPressed;
        public event System.EventHandler OnEnterPressed;

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
                //default:
                //    this.FireOnStringInput(new StringEventArgs(e.ConsoleChar.ToString()));
                //    break;
            }
        }

        protected virtual void FireOnDeleteKeyPressed()
        {
            OnDeleteKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireOnCharPressed(CharEventArgs e)
        {
            OnCharPressed?.Invoke(this, e);
        }

        protected virtual void FireOnEnterPressed()
        {
            OnEnterPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}