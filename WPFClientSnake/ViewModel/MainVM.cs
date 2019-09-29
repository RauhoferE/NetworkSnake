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
        private PlayerVM player;
        private InputValidatorVM inputVM;

        public MainVM()
        {
            this.Player = new PlayerVM();
            this.InputValidatorVM = new InputValidatorVM();
            this.inputVM.OnKeyPressed += this.player.SendInputToClient;
        }

        public PlayerVM Player
        {
            get { return this.player; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error value cant be null");
                }

                this.player = value;

            }
        }

        public InputValidatorVM InputValidatorVM
        {
            get { return this.inputVM; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error value cant be null");
                }

                this.inputVM = value;

            }
        }

        public void RedirectInput(object sender, KeyEventArgs e)
        {
            Task.Factory.StartNew(() => this.InputValidatorVM.GetInput(e));
        }
        
    }
}
