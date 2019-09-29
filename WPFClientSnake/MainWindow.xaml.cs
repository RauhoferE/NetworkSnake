using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFClientSnake
{
    using NetworkLibrary;


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainVM main = new MainVM();
            this.OnKeyPressed += main.RedirectInput;
            this.DataContext = main;
        }

        public event EventHandler<KeyEventArgs> OnKeyPressed;

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            //TODO: Durch keyboardwatcehr ersetzten
            Task.Factory.StartNew(() => this.FireOnKeyPressed(e));
        }

        protected virtual void FireOnKeyPressed(KeyEventArgs e)
        {
            OnKeyPressed?.Invoke(this, e);
        }
    }
}
