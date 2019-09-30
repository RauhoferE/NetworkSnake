//-----------------------------------------------------------------------
// <copyright file="MainVM.cs" company="FH Wiener Neustadt">
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
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// The <see cref="MainVM"/> class.
    /// </summary>
    public class MainVM
    {
        /// <summary>
        /// The player.
        /// </summary>
        private PlayerVM player;

        /// <summary>
        /// The input validator.
        /// </summary>
        private InputValidatorVM inputVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class.
        /// </summary>
        public MainVM()
        {
            this.Player = new PlayerVM();
            this.InputValidatorVM = new InputValidatorVM();
            this.inputVM.OnKeyPressed += this.player.SendInputToClient;
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value> A normal <see cref="PlayerVM"/>. </value>
        public PlayerVM Player
        {
            get
            {
                return this.player;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error value cant be null");
                }

                this.player = value;
            }
        }

        /// <summary>
        /// Gets the input validator.
        /// </summary>
        /// <value> A normal <see cref="InputValidatorVM"/>. </value>
        public InputValidatorVM InputValidatorVM
        {
            get
            {
                return this.inputVM;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error value cant be null");
                }

                this.inputVM = value;
            }
        }

        /// <summary>
        /// This method redirects the input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="KeyEventArgs"/>. </param>
        public void RedirectInput(object sender, KeyEventArgs e)
        {
            Task.Factory.StartNew(() => this.InputValidatorVM.GetInput(e));
        }
    }
}
