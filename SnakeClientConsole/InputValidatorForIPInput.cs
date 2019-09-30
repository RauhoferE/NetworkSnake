//-----------------------------------------------------------------------
// <copyright file="InputValidatorForIPInput.cs" company="FH Wiener Neustadt">
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
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="InputValidatorForIPInput"/> class.
    /// </summary>
    public class InputValidatorForIPInput
    {
        /// <summary>
        /// The IP address as a string.
        /// </summary>
        private string ipAdress;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValidatorForIPInput"/> class.
        /// </summary>
        public InputValidatorForIPInput()
        {
            this.ipAdress = string.Empty;
        }

        /// <summary>
        /// This event fires when a key has been input.
        /// </summary>
        public event EventHandler<MessageContainerEventArgs> OnKeyInput;

        /// <summary>
        /// This event fires when the delete key has been pressed.
        /// </summary>
        public event EventHandler OnDeleteKeyPressed;

        /// <summary>
        /// This event fires when the enter key has been pressed.
        /// </summary>
        public event EventHandler<MessageContainerEventArgs> OnEnterPressed;

        /// <summary>
        /// This event fires when a error message should be printed.
        /// </summary>
        public event EventHandler<MessageContainerEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// This method deletes the last character of the string.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void DeleteLastEntry(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ipAdress))
            {
                this.ipAdress = this.ipAdress.Substring(0, this.ipAdress.Length - 1);
                this.FireOnDeleteKeyPressed();
            }
        }

        /// <summary>
        /// This method adds a character to the string.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="CharEventArgs"/>. </param>
        public void AddChar(object sender, CharEventArgs e)
        {
            this.ipAdress = this.ipAdress + e.Character.ToString();
            this.FireOnKeyInput(new MessageContainerEventArgs(new MessageContainer(this.ipAdress)));
        }

        /// <summary>
        /// This method sends the finished string.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
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

        /// <summary>
        /// This method fires the <see cref="OnDeleteKeyPressed"/> event.
        /// </summary>
        protected virtual void FireOnDeleteKeyPressed()
        {
            this.OnDeleteKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEnterPressed"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        protected virtual void FireOnEnterPressed(MessageContainerEventArgs e)
        {
            this.OnEnterPressed?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyInput"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        protected virtual void FireOnKeyInput(MessageContainerEventArgs e)
        {
            this.OnKeyInput?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnErrorMessagePrint"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="MessageContainerEventArgs"/>. </param>
        protected virtual void FireOnErrorMessagePrint(MessageContainerEventArgs e)
        {
            this.OnErrorMessagePrint?.Invoke(this, e);
        }
    }
}