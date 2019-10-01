//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="FH Wiener Neustadt">
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
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// The <see cref="MainWindow"/> class.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            MainVM main = new MainVM();
            this.OnKeyPressed += main.RedirectInput;
            this.DataContext = main;
        }

        /// <summary>
        /// The event should fire when a key was pressed.
        /// </summary>
        public event EventHandler<KeyEventArgs> OnKeyPressed;

        /// <summary>
        /// This method fires the <see cref="OnKeyPressed"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="KeyEventArgs"/>. </param>
        protected virtual void FireOnKeyPressed(KeyEventArgs e)
        {
            this.OnKeyPressed?.Invoke(this, e);
        }

        /// <summary>
        /// This method is called when the stack panel receives an key input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="KeyEventArgs"/>. </param>
        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            this.FireOnKeyPressed(e);
        }
    }
}
