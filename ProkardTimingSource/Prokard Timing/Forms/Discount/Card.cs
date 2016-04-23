using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prokard_Timing.Forms.Discount.Card;
using Prokard_Timing.model;

namespace Prokard_Timing
{
    public partial class Discount_Card : Form
    {
        private AdminControl admin;

        private IEnumerable<DiscountCardGroup> cardTypies;

        //private Dictionary<int, Dictionary<string, string>> Cards = new Dictionary<int, Dictionary<string, string>>();

        
        
 //    private string filter;
      //  private string filter_field;

//        private string column;
 //       private bool sort_direction = true;

        public Discount_Card(AdminControl ad)
        {
            InitializeComponent();
            cardsList_dataGridView1.DoubleBuffered(true);

            admin = ad;

            fillCardTypies();
            showDiscountCards();

            /*
            cards = ad.model.getAllDiscountCards(filter, filter_field, column, sort_direction);
            dataGridView1.RowCount = cards.ToList().Count;
             */
        }

        private void fillCardTypies()
        {
            cardTypies = admin.model.getCardsTypies();

            cardTypies_dataGridView1.Rows.Clear();

            foreach (DiscountCardGroup item in cardTypies)
            {

                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(cardTypies_dataGridView1);

                dr.Cells[0].Value = item.Id;
                dr.Cells[1].Value = item.PercentOfDiscount;
                if( item.IsDeleted)
                {
                dr.Cells[2].Value = "да";
                    
                }
                    else
                {
                     dr.Cells[2].Value = "нет";
                }

                cardTypies_dataGridView1.Rows.Add(dr);

            }
        }

        private void showDiscountCards()
        {
            string filter = filter_TextBox1.Text;
            cardsList_dataGridView1.Rows.Clear();
            IEnumerable<DiscountCard> cards = admin.model.getAllDiscountCards(filter).OrderBy(m => m.Number);
           
            for (int i = 0; i < cards.ToList().Count; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(cardsList_dataGridView1);

                dr.Cells[0].Value = cards.ElementAt(i).Id;
                dr.Cells[1].Value = cards.ElementAt(i).Number;
                dr.Cells[2].Value = cards.ElementAt(i).DiscountCardGroup.PercentOfDiscount;
                if (cards.ElementAt(i).owner != null)
                {
                    dr.Cells[3].Value = cards.ElementAt(i).owner.name + " " + cards.ElementAt(i).owner.surname + " [" + cards.ElementAt(i).owner.nickname + "]";
                }
                dr.Cells[4].Value = cards.ElementAt(i).Created;

                if (cards.ElementAt(i).seller != null)
                {
                    dr.Cells[5].Value = cards.ElementAt(i).seller.name + " " + cards.ElementAt(i).seller.surname + " [" + cards.ElementAt(i).seller.login + "]";
                }
                if (cards.ElementAt(i).IsBlocked)
                {
                    dr.Cells[8].Value = " да";
                }
                else
                {
                    dr.Cells[8].Value = " нет";
                }

                cardsList_dataGridView1.Rows.Add(dr);

               

              



            }

        }

        private void Discount_Card_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {           
            Discount_Card_Add DCA = new Discount_Card_Add(admin);
            DCA.ShowDialog();
            showDiscountCards();
        }

        
       

       /*

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (column == dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    sort_direction = !sort_direction;
                }
                else
                {
                    column = dataGridView1.Columns[e.ColumnIndex].Name;
                    sort_direction = true;
                }

                cards = admin.model.getAllDiscountCards(filter, filter_field, column, sort_direction);
                dataGridView1.RowCount = cards.ToList().Count;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        * */

        // применить фильтр
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            showDiscountCards();
        }

        private void clearFilter_Button6_Click(object sender, EventArgs e)
        {
            filter_TextBox1.Text = "";
            showDiscountCards();
        }


        // удалить карту
        private void blockCard_Button2_Click(object sender, EventArgs e)
        {
            if (cardsList_dataGridView1.SelectedRows.Count > 0)
            {
                admin.model.blockCard(Convert.ToInt32(cardsList_dataGridView1.SelectedRows[0].Cells[0].Value));
            }
            showDiscountCards();
        }
              

        private void addDiscountcardType_toolStripButton3_Click(object sender, EventArgs e)
        {
             
            AddDiscountCard addCardTypeForm = new AddDiscountCard(admin);
            addCardTypeForm.ShowDialog();
            fillCardTypies();
        }

        private void deleteDiscountCardType_toolStripButton4_Click(object sender, EventArgs e)
        {
             if (cardTypies_dataGridView1.SelectedRows.Count > 0)
            {
                admin.model.deleteDiscountCardType(Convert.ToInt32(cardTypies_dataGridView1.SelectedRows[0].Cells[0].Value));
             }
             fillCardTypies();
        }

        private void addCartsRange_toolStripButton3_Click(object sender, EventArgs e)
        {
            AddCartsRange addCards = new AddCartsRange(admin);
            addCards.ShowDialog();
            showDiscountCards();
        }

        // редактировать карту
        private void editCard_Button3_Click(object sender, EventArgs e)
        {
            editCard();
        }

        private void editCard()
        {
             if (cardsList_dataGridView1.SelectedRows.Count > 0)
            {
                Discount_Card_Add addCardForm = 
                    new Discount_Card_Add(admin, 
                        Convert.ToInt32(cardsList_dataGridView1.SelectedRows[0].Cells[0].Value));
            addCardForm.ShowDialog();
            showDiscountCards();
            }
        }

      

        private void cardsList_dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editCard();
        }

        
    }
}
