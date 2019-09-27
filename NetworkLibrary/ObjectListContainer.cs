using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLibrary
{
    [Serializable]
    public class ObjectListContainer
    {
        private List<ObjectPrintContainer> oldItems;
        private List<ObjectPrintContainer> newItems;
        private GameInformationContainer information;

        public ObjectListContainer(List<ObjectPrintContainer> oldItems, List<ObjectPrintContainer> newItems, GameInformationContainer information)
        {
            this.OldItems = oldItems;
            this.NewItems = newItems;
            this.Information = information;
        }

        public List<ObjectPrintContainer> OldItems
        {
            get
            {
                return this.oldItems;
            }

           private set
            {
                if (value  == null)
                {
                    throw new ArgumentNullException("Error value cant be null.");
                }

                this.oldItems = value;
            }
        }

        public List<ObjectPrintContainer> NewItems
        {
            get
            {
                return this.newItems;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error value cant be null.");
                }

                this.newItems = value;
            }
        }

        public GameInformationContainer Information
        {
            get
            {
                return this.information;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error field cant be null.");
                }

                this.information = value;
            }
        }
    }
}