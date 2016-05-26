using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rentix.Controls;
using Rentix.model;
using Rentix;

namespace Rentix.Forms.Discount.Card
{
    public partial class AddCartsRange : Form
    {
        AdminControl admin = null;
        comboBoxItem ci;

        public AddCartsRange(AdminControl ad)
        {
            InitializeComponent();

            admin = ad;
            fillDiscountCardTypies();         
        }

      

        private void fillDiscountCardTypies()
        {
            IEnumerable<DiscountCardGroup> cardGroups = admin.model.getCardsTypies().Where(m => m.IsDeleted != true);
            cardGroup_comboBox1.Items.Clear();
            foreach(DiscountCardGroup item in cardGroups)
            {
                ci = new comboBoxItem(item.PercentOfDiscount.ToString(), item.Id);
                cardGroup_comboBox1.Items.Add(ci);
            }
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Discount_Card_Add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }

        // добавим карту
        private void button1_Click(object sender, EventArgs e)
        {
            if (ci.getSelectedValue(cardGroup_comboBox1) == -1)
            {
                statusStrip1.Text = "Необходимо указать тип скидочной карты";
                return;
            }

            for (int i = Convert.ToInt32(rangeFrom_numericUpDown4.Value);
                i <= rangeTo_numericUpDown3.Value; i++)
            {

                DiscountCard someCard = new DiscountCard
                {
                    idOwner = -1,
                    Number = prefix_numericUpDown1.Value.ToString("0000") +
                    i.ToString("0000"),
                    Created = DateTime.Now,
                    idSeller = null,
                    ValidUntil = DateTime.Now.AddYears(100),
                    IsBlocked = false,
                    SalePlace = "",
                    IdDiscountCardGroup = ci.getSelectedValue(cardGroup_comboBox1)
                };

                bool isAdded = admin.model.assignDiscountCard(someCard);

                statusStrip1.Text = "Добавлена карта: " + someCard.Number;
                Application.DoEvents();
            }

            statusStrip1.Text = "Карты добавлены";
            this.Close();
        }
    }
}
