using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rentix.objects.discount
{
    /*
     * Класс скидочных карточек
     * Fields:
     *      Races - Количество бесплатных рейсов
     *      Owner - Владелец карточки
     *      Number - Номер карточки
     *      Seller - Продавец карточки
     *      Discount - % скидки на карточке
     *      Referent - Референт карточки
     *      salePlace - Место продажи карточки
     *      createDate - Дата выдачи карточки
     */

    class Object_Discount_Card : Main_Object
    {
        /*
        public Field Races = new Field("races", 0, "int", "3", 0, true, true);
        public Field Owner = new Field("owner", "", "varchar", "255", false, true);
        public Field Number = new Field("number", 0, "varchar", "255", 0, false, true);
        public Field Seller = new Field("seller", "", "varchar", "255", "", false, true);
        public Field Discount = new Field("discount", 0, "int", "3", 0, true, true);
        public Field Referent = new Field("referent", "", "varchar", "255", "", false, true);
        public Field salePlace = new Field("salePlace", "", "varchar", "255", "", false, true);
        public Field createDate = new Field("createDate", "", "datetime", "", false, true);
        */

        public int Races { get { return (int)this.getValue("races"); } set { this.setValue("races", value); } }
        public int Discount { get { return (int)this.getValue("discount"); } set { this.setValue("discount", value); } }
        public string Owner { get { return (string)this.getValue("owner"); } set { this.setValue("owner", value); } }
        public string Number { get { return (string)this.getValue("number"); } set { this.setValue("number", value); } }
        public string Seller { get { return (string)this.getValue("seller"); } set { this.setValue("seller", value); } }
        public string Referent { get { return (string)this.getValue("referent"); } set { this.setValue("referent", value); } }
        public string salePlace { get { return (string)this.getValue("salePlace"); } set { this.setValue("salePlace", value); } }
        public string createDate { get { return (string)this.getValue("createDate"); } set { this.setValue("createDate", value); } }
        
        public Object_Discount_Card(ProkardModel model)
        {
            try
            {
                this._model = model;
                this._config.Add("tableName", "Discount_Card");
                this._fields.Add("races", 0);
                this._fields.Add("discount", 0);
                this._fields.Add("owner", String.Empty);
                this._fields.Add("number", String.Empty);
                this._fields.Add("seller", String.Empty);
                this._fields.Add("referent", String.Empty);
                this._fields.Add("salePlace", String.Empty);
                this._fields.Add("createDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}
