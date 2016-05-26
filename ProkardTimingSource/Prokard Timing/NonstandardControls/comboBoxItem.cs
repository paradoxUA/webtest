using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rentix.Controls
{
    #region нестандартный combobox item
    public class comboBoxItem
    {
        private string _name;
        public int value;

        public comboBoxItem(string name, int value)
        {
            _name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return _name;
        }

        /*
        public string getSelectedValue()
        {
            return Convert.ToString(this.value);
        }
        */

        // так как comboboxы содержат нестандартные items, то и выделять их надо нестандартно ;-)
        public Int32 getSelectedValue(ComboBox someComboBox)
        {
            if(someComboBox.SelectedIndex < 0)
            {
                return -1;
            }
            
           return ((comboBoxItem) someComboBox.Items[someComboBox.SelectedIndex]).value;
            
        }


        // так как comboboxы содержат нестандартные items, то и выделять их надо нестандартно ;-)
        public void selectComboBoxValueById(ComboBox someComboBox, int valueId)
        {
            for (int i = 0; i < someComboBox.Items.Count; i++)
            {
                comboBoxItem someItem = (comboBoxItem)someComboBox.Items[i];
                if (someItem.value == valueId)
                {
                    someComboBox.SelectedIndex = i;
                    break;
                }
            }
        }
        

    }



     
        
    #endregion
}
