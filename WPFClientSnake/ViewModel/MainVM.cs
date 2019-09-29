using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClientSnake
{
    using System.Threading;
    using System.Windows.Input;

    public class MainVM
    {
        public MainVM()
        {
            this.Player = new PlayerVM();
            this.InputValidatorVM = new InputValidatorVM();
        }

        public PlayerVM Player
        {
            get;
            private set;
        }

        public InputValidatorVM InputValidatorVM
        {
            get;
            private set;
        }

        public void RedirectInput(object sender, KeyEventArgs e)
        {
            Task.Factory.StartNew(() => this.InputValidatorVM.GetInput(e));
        }
        
    }
}
