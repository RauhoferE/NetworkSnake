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
    using System.Threading.Tasks;
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
            InitializeComponent();
            MainVM main = new MainVM();
            this.OnKeyPressed += main.RedirectInput;
            this.DataContext = main;
        }

        public event EventHandler<KeyEventArgs> OnKeyPressed;

        protected virtual void FireOnKeyPressed(KeyEventArgs e)
        {
            OnKeyPressed?.Invoke(this, e);
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            //TODO: Durch keyboardwatcehr ersetzten
            Task.Factory.StartNew(() => this.FireOnKeyPressed(e));
        }
    }
}
