using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeClientConsole
{
    using NetworkLibrary;

    public class InputValidatorForIPInput
    {
        public event System.EventHandler<MessageContainerEventArgs> OnKeyInput;
        public event System.EventHandler OnDeleteKeyPressed;
        public event System.EventHandler<MessageContainerEventArgs> OnEnterPressed;
        public event EventHandler<MessageContainerEventArgs> OnErrorMessagePrint;

        private string ipAdress;

        public InputValidatorForIPInput()
        {
            this.ipAdress = string.Empty;
        }

        public void DeleteLastEntry(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ipAdress))
            {
                this.ipAdress = this.ipAdress.Substring(0, this.ipAdress.Length - 1);
                this.FireOnDeleteKeyPressed();
            }
        }

        public void AddChar(object sender, CharEventArgs e)
        {
            this.ipAdress = this.ipAdress + e.Character.ToString();
            this.FireOnKeyInput(new MessageContainerEventArgs(new MessageContainer(this.ipAdress)));
        }

        public void SendIpAdress(object sender, EventArgs e)
        {

            if (!IPHelper.IsIPAdress(this.ipAdress))
            {
                this.FireOnErrorMessagePrint(new MessageContainerEventArgs(new MessageContainer("Error Ip Adress is wrong.")));
            }
            else
            {
                this.FireOnEnterPressed(new MessageContainerEventArgs(new MessageContainer(this.ipAdress)));
            }
        }

        protected virtual void FireOnDeleteKeyPressed()
        {
            OnDeleteKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FireOnEnterPressed(MessageContainerEventArgs e)
        {
            OnEnterPressed?.Invoke(this, e);
        }

        protected virtual void FireOnKeyInput(MessageContainerEventArgs e)
        {
            OnKeyInput?.Invoke(this, e);
        }

        protected virtual void FireOnErrorMessagePrint(MessageContainerEventArgs e)
        {
            OnErrorMessagePrint?.Invoke(this, e);
        }
    }
}