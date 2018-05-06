using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rentix.Controls;
using Rentix;
using Rentix.model;

namespace Rentix.Forms.Discount.Card
{
    public partial class Discount_Card_Add : Form
    {
        AdminControl admin = null;
        comboBoxItem ci;
        bool isEditMode = false;
        DiscountCard currentCardForEditMode;

        public Discount_Card_Add(AdminControl ad)
        {
            InitializeComponent();

            admin = ad;
            fillDiscountCardTypies();
            fillOwners(false);
        }

        // edit mode
         public Discount_Card_Add(AdminControl ad, int idCard)
        {
            InitializeComponent();

            this.Text = "Редактирование скидочной карты";
            admin = ad;
            fillDiscountCardTypies();
          

            DiscountCard someCard = admin.model.getAllDiscountCards().Where(m => m.Id == idCard).Take(1).SingleOrDefault();

            if (someCard == null)
            {
                return;
            }

            fillOwners(true, Convert.ToInt32( someCard.idOwner));

            currentCardForEditMode = someCard;

             isEditMode = true;
             cardNumber_textBox1.Enabled = false;

            cardNumber_textBox1.Text = someCard.Number;
             
             if (someCard.idOwner != null)
            {
				comboBoxItem.selectComboBoxValueById(owner_comboBox1, 
                     Convert.ToInt32(someCard.idOwner));
            }

            if (someCard.IdDiscountCardGroup != null)
            {
				comboBoxItem.selectComboBoxValueById(cardGroup_comboBox1, 
                     Convert.ToInt32(someCard.IdDiscountCardGroup));
            }
        }

        private void fillDiscountCardTypies()
        {
            IEnumerable<DiscountCardGroup> cardGroups = admin.model.getCardsTypies().Where(m => m.IsDeleted != true).OrderBy(m => m.PercentOfDiscount);
            cardGroup_comboBox1.Items.Clear();
            foreach(DiscountCardGroup item in cardGroups)
            {
                ci = new comboBoxItem(item.PercentOfDiscount.ToString(), item.Id);
                cardGroup_comboBox1.Items.Add(ci);
            }
            
        }

         private void fillOwners(bool showAll, int idUser = -1)
        {
             // добавить карту можно только пользователю, не имеющему карты
            IEnumerable<users> users;

            if (showAll)
            {
                users = (from u in admin.model.getUsers()
                        where u.id == idUser ||
                            u.DiscountCards.Count == 0
                        select u).OrderBy(m => m.nickname);
            }
            else
            {
                users = admin.model.getUsers().Where(m => m.DiscountCards.Count == 0).OrderBy(m => m.nickname);

            }
             
             owner_comboBox1.Items.Clear();
            
            foreach(users item in users)
            {
                string name = item.name + " " + item.surname + " [" + item.nickname + "]";
                ci = new comboBoxItem(name, item.id);
                owner_comboBox1.Items.Add(ci);
            }
            
        }

       



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Discount_Card_Add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) this.Close();
        }

        // добавим карту
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxItem.getSelectedValue(cardGroup_comboBox1) == -1)
            {
                statusStrip1.Text = "Необходимо указать тип скидочной карты";
                return;
            }

            if (isEditMode)
            {
                currentCardForEditMode.idOwner = comboBoxItem.getSelectedValue(owner_comboBox1);
            //    currentCardForEditMode.Number = cardNumber_textBox1.Text;
                currentCardForEditMode.idSeller = admin.USER_ID;
                currentCardForEditMode.IdDiscountCardGroup = comboBoxItem.getSelectedValue(cardGroup_comboBox1);
               admin.model.editDiscountCard(currentCardForEditMode);
               this.Close();
            }
            else
            {
                DiscountCard someCard = new DiscountCard
                {
                    idOwner = comboBoxItem.getSelectedValue(owner_comboBox1),
                    Number = cardNumber_textBox1.Text,
                    Created = assigned_dateTimePicker1.Value,
                    idSeller = admin.USER_ID,
                    ValidUntil = DateTime.Now.AddYears(100),
                    IsBlocked = false,
                    SalePlace = "",
                    IdDiscountCardGroup = comboBoxItem.getSelectedValue(cardGroup_comboBox1)

                };

                bool isAdded = admin.model.assignDiscountCard(someCard);
                if (isAdded == false)
                {
                    MessageBox.Show("Не удалось добавить карту. Возможно, карта с таким номером уже выдана другому пилоту");
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
