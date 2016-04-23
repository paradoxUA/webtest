using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Prokard_Timing.Controls;

namespace Prokard_Timing
{
    public partial class NewPilot : Form
    {
       
        bool isEditMode = false; // иначе добавление
        int PilotID = -1;
        AdminControl admin;
        comboBoxItem ci;

        // для режима редактирования сохраним текущие значения
        private string pilotName;
        private string pilotLastName;
        private string pilotNick;


        public NewPilot(AdminControl ad, int PID = -1, bool Edt = false)
        {
            InitializeComponent();
            admin = ad;

            comboBox1.Enabled = admin.IS_ADMIN;

            ShowGroups();

            if (Edt)
            {
                isEditMode = Edt;
                PilotID = PID;
                LoadPilotData(PID);
                fillDiscountCards(PID);
            }
            else
            {
                fillDiscountCards();
            }
        }
        PilotFilter MainFilter;

        private void fillDiscountCards(int idPilot = -1)
        {
            cards_comboBox2.Items.Clear();

              ci = new comboBoxItem("[нет]",
                    -1);      
          
                cards_comboBox2.Items.Add(ci);
                cards_comboBox2.SelectedIndex = 0;

            IEnumerable<model.DiscountCard> cards;

            // режим редактирования
            if (idPilot > -1)
            {
                // получить все неназначенные и свою
                cards = (from ca in admin.model.getAllDiscountCards()
                         where
                             ca.owner == null || ca.idOwner == idPilot
                         select ca).OrderBy(m
                           => m.Number);
            }
            else
            {
                 // получить все неназначенные
               cards = admin.model.getAllDiscountCards().Where(m =>
                        m.owner == null).OrderBy(m => m.Number);
            }


            for (int i = 0; i < cards.Count(); i++)
            {
                ci = new comboBoxItem(cards.ElementAt(i).Number,
                    cards.ElementAt(i).Id);
                
                cards_comboBox2.Items.Add(ci);
            }

            if (idPilot != -1)
            {
                model.DiscountCard userCard =
                (from ca in admin.model.getAllDiscountCards()
                 where ca.idOwner == idPilot
                 select ca).Take(1).SingleOrDefault();

                if (userCard != null)
                {
                    ci.selectComboBoxValueById(cards_comboBox2, userCard.Id);
                }
            }
 


        }

        private void LoadPilotData(int PilotID)
        {
            Hashtable Pilot = admin.model.GetPilot(PilotID);

            this.name_textBox2.Text = Pilot["name"].ToString();
            pilotName = Pilot["name"].ToString();

            this.lastName_textBox1.Text = Pilot["surname"].ToString();
            pilotLastName = Pilot["surname"].ToString();

            this.nick_textBox3.Text = Pilot["nickname"].ToString();
            pilotNick = Pilot["nickname"].ToString();

            this.textBox6.Text = Pilot["email"].ToString();
            this.phone_textBox5.Text = Pilot["tel"].ToString();

            if (admin.model.getAllDiscountCards().Where(m =>
                    m.idOwner == PilotID).Count() > 0)
            {
                cards_comboBox2.Text =
                    admin.model.getAllDiscountCards().Where(m =>
                        m.idOwner == PilotID).Take(1).SingleOrDefault().Number;
            }
            else
            {

            }

            //      this.textBox4.Text = Pilot["barcode"].ToString();

            if (Convert.ToBoolean(Pilot["gender"]))
                radioButton1.Checked = true;
            else radioButton2.Checked = true;

            dateTimePicker1.Value = Convert.ToDateTime(Pilot["birthday"]);

            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(admin.model.GetGroupName(Pilot["gr"].ToString()));
        }

        private void ShowGroups()
        {
            List<string> Groups = admin.model.GetAllGroupsName();
            for (int i = 0; i < Groups.Count; i++)
            {
                comboBox1.Items.Add(Groups[i]);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        public NewPilot(AdminControl ad, PilotFilter pf, string name = "", string surname = "", string nickname = "", string email = "", string tel = "")
        {
            admin = ad;
            InitializeComponent();
            this.name_textBox2.Text = name;
            this.lastName_textBox1.Text = surname;
            this.nick_textBox3.Text = nickname;
            this.textBox6.Text = email;
            this.phone_textBox5.Text = tel;
            MainFilter = pf;
            ShowGroups();
            comboBox1.Enabled = admin.IS_ADMIN;
            detailedPhoneComment_label.Visible = false;
            fillDiscountCards(-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        // сохраним пилота
        private void button1_Click(object sender, EventArgs e)
        {
            name_textBox2.Text = name_textBox2.Text.Trim();
            lastName_textBox1.Text = lastName_textBox1.Text.Trim();
            nick_textBox3.Text = nick_textBox3.Text.Trim().ToLower();
            phone_textBox5.Text = phone_textBox5.Text.Trim();

            if (checkPhoneNumber() == false)
            {
                return;
            }

            if (name_textBox2.Text.Trim().Length == 0)
            {
                MessageBox.Show("Необходимо указать имя пилота");
                return;
            }

             if (lastName_textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("Необходимо указать фамилию пилота");
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выбрать группу");
                return;
            }

            // не разрешаем двум пилотам иметь одинаковые ники
            if ((isEditMode == false && nick_textBox3.Text.Length > 0 
                && admin.model.isNickUnique(nick_textBox3.Text) == false) ||
                (isEditMode == true && nick_textBox3.Text != pilotNick &&
                admin.model.isNickUnique(nick_textBox3.Text) == false))
            {
                MessageBox.Show("Пилот с таким никнеймом уже зарегистрирован");
                return;
            }

            // если ник не указан, не разрешаем двум пилотам иметь одинаковое имя и фамилию
            if ((isEditMode == false &&
                nick_textBox3.Text.Length == 0 &&
                admin.model.isNameAndLastNameUnique(name_textBox2.Text, lastName_textBox1.Text) == false)
                || (isEditMode == true &&
                 nick_textBox3.Text.Length == 0 &&
                 (name_textBox2.Text != pilotName || lastName_textBox1.Text != pilotLastName) &&
                 admin.model.isNameAndLastNameUnique(name_textBox2.Text, lastName_textBox1.Text) == false
                )
                )
            {
                MessageBox.Show("Пилот с такими именем и фамилией уже зарегистрирован. Для различия пилотов, необходимо указать никнейм");
                return;
            }
/*
            if (!isEditMode && admin.model.GetNewPilot(name_textBox2.Text, lastName_textBox1.Text, nick_textBox3.Text))
            {
                MessageBox.Show("Увы, но такой пилот уже есть!");
            }
            else
            {
            */
                /*
                if (nick_textBox3.Text.Length < 3)
                {
                    MessageBox.Show("Поле 'Никнейм' обязательно для заполнения и его значение должно быть не короче 3-х символов");
                    return;
                }
                */

                Hashtable data = new Hashtable();

                data["name"] = name_textBox2.Text;
                data["surname"] = lastName_textBox1.Text;
                data["nickname"] = nick_textBox3.Text;
                data["birthday"] = dateTimePicker1.Value;
                data["gender"] = radioButton1.Checked ? 1 : 0;
                data["email"] = textBox6.Text;
                data["tel"] = phone_textBox5.Text;
                data["group"] = admin.model.GetGroupID(comboBox1.Items[comboBox1.SelectedIndex].ToString());
                data["banned"] = false;
                
            if (cards_comboBox2.SelectedIndex > 0)
                {
                    data["barcode"] = cards_comboBox2.SelectedItem;
                }
                else
                {
                    data["barcode"] = "";
                }

                if (MainFilter != null)
                {
                    MainFilter.Filter = nick_textBox3.Text; // " and name='" + textBox2.Text + "' and surname='" + textBox1.Text + "'";
                }

                if (isEditMode)
                {
                    admin.model.ChangePilot(data, PilotID);
                }
                else
                {
                   model.users newPilot = admin.model.AddNewPilot(data);
                   if (newPilot != null)
                   {
                       PilotID = newPilot.id;
                   }
                }


            // не очень красиво написано. можно всё переделать, но будет слишком дорого
                if (editCard_checkBox1.Checked && cards_comboBox2.Items.Count > 0 && cards_comboBox2.SelectedIndex > -1)
                {
                    // если указана карта, сохраним её. иначе освободим ранее выданную
                   int pilotCardId = ci.getSelectedValue(cards_comboBox2);

                    //найдём текущую карту пилота
                    model.DiscountCard someCard =
                        admin.model.getAllDiscountCards().Where(m =>
                        m.idOwner == PilotID).Take(1).SingleOrDefault();

                    if(someCard != null && pilotCardId == -1)
                    {
                       // раньше у пилота была карта, а теперь нету
                        someCard.idOwner = null;   
                        admin.model.editDiscountCard(someCard);
                    }
                    else if (someCard != null && pilotCardId != someCard.Id)
                    {
                        // раньше у пилота была одна карта, а теперь другая

                        someCard.idOwner = null;
                        admin.model.editDiscountCard(someCard);

                        someCard = admin.model.getAllDiscountCards().Where(m => m.Id == someCard.Id).Take(1).SingleOrDefault();
                        //  освободим старую
                        someCard.idOwner = -1;
                        admin.model.editDiscountCard(someCard);
                        //  и займём новую
                        someCard = admin.model.getAllDiscountCards().Where(m => m.Id == pilotCardId).Take(1).SingleOrDefault();
                        someCard.idOwner = PilotID;
                        admin.model.editDiscountCard(someCard);
                    }
                    else if(someCard == null && pilotCardId > -1)
                    {
                        // раньше у пилота не было карты, а теперь есть
                         someCard = admin.model.getAllDiscountCards().Where(m => m.Id == pilotCardId).Take(1).SingleOrDefault();
                        someCard.idOwner = PilotID;
                        admin.model.editDiscountCard(someCard);
                    }
                    
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
        //    }
        }

        private void NewPilot_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27) { this.Close(); }
        }

        private void phone_textBox5_TextChanged(object sender, EventArgs e)
        {
            checkPhoneNumber();
        }

        private bool isPhoneIsNumber(string someString)
        {
            try
            {
                Convert.ToInt64(phone_textBox5.Text);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private bool checkPhoneNumber()
        {
            bool result = true;

            if (phone_textBox5.Text.Length == 0 || phone_textBox5.Text.Length >= 10)
            {
                if (phone_textBox5.Text.Length > 0 && isPhoneIsNumber(phone_textBox5.Text) == false)
                {
                    phone_textBox5.BackColor = Color.PapayaWhip;
                    detailedPhoneComment_label.Visible = true;
                    return false;
                }

                phone_textBox5.BackColor = Color.White;
                detailedPhoneComment_label.Visible = false;
                return true;
            }

            if (phone_textBox5.Text.Length < 10)
            {
                phone_textBox5.BackColor = Color.PapayaWhip;
                detailedPhoneComment_label.Visible = true;
                return false;
            }
            
            return result;
        }

        private void editCard_checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cards_comboBox2.Enabled = editCard_checkBox1.Checked;
            editCard_checkBox1.Enabled = false;
        }

   
    }

    public class PilotFilter
    {
        public string Filter = "";
    }

}
