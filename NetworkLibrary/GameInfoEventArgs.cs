using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    public class GameInfoEventArgs
    {
        public GameInfoEventArgs(GameInformationContainer container)
        {
            this.GameInformationContainer = container;
        }

        public GameInformationContainer GameInformationContainer
        {
            get;
        }
    }
}