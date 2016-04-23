using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prokard_Timing.objects
{
    class Main_Object
    {
        protected Dictionary<string, string> _config = new Dictionary<string, string>();
        protected Dictionary<string, object> _fields = new Dictionary<string, object>();
        protected ProkardModel _model;

        public Main_Object()
        {
            this._config.Add("database", "default");
        }

        public void loadObject(string id)
        {
            if (!this._config.ContainsKey("id"))
            {
                Dictionary<string, object> loadData = this._model.GetObject(this._config["tableName"], id);

                if (loadData.Count > 0)
                {
                    foreach (KeyValuePair<string, object> kvp in loadData)
                    {
                        if (this._fields.ContainsKey(kvp.Key))
                        {
                            this._fields[kvp.Key] = kvp.Value;
                        }
                    }

                    this._config.Add("id", id);
                }
            }
        }

        public bool saveObject()
        {
            string query = String.Empty;
            string tableName = this.getTableName();
                
            if (this._config.ContainsKey("id"))
            {
                query = "update " + this.getFullTableName() + " set ";
                int index = 0;
                foreach (KeyValuePair<string, object> kvp in this._fields)
                {
                    index++;
                    query += tableName + ".`" + kvp.Key + "` = \'" + kvp.Value + "\'" + (index == this._fields.Count ? "" : ",");
                }

                query += " where " + tableName + ".`id` = \'" + this.getId() + "\'";
            }
            else
            {
                query = "insert " + this.getFullTableName() + " set ";
                int index = 0;
                foreach( KeyValuePair<string, object> kvp in this._fields)
                {
                    index++;
                    query += tableName + ".`" + kvp.Key + "` = \'" + kvp.Value + "\'" + (index == this._fields.Count ? "" : ",");
                }

            }
         
            this._model.ExecuteQuery(query);
            return true;
        }

        public string getId()
        {
            if (this._config.ContainsKey("id"))
            {
                return this._config["id"];
            }
            else
            {
                return null;
            }
        }

        public object getValue(string FieldName)
        {
            if (this._fields.ContainsKey(FieldName))
            {
                return this._fields[FieldName];
            }
            else
            {
                return null;
            }
        }

        public void setValue(string FieldName, object Value)
        {
            if (this._fields.ContainsKey(FieldName))
            {
                this._fields[FieldName] = Value;
            }
        }

        public string getFullTableName()
        {
            return this.getTableName();
            //return this._config["database"] + "." + this._config["tableName"];
        }

        public string getTableName(bool shielding = true)
        {
            if (shielding)
            {
                return "`" + this._config["tableName"] + "`";
            }
            else
            {
                return this._config["tableName"];
            }
        }
    }

    public struct Field
    {
        string _name;
        string _type;
        string _length;
        object _value;
        object _defaultValue;
        bool _unsigned;
        bool _isNull;

        public Field(string Name = "", object Value = null, string Type = "varchar", string Length = "255", object DefaultValue = null, bool Unsigned = false, bool isNull = false)
        {
            this._name = Name;
            this._value = Value;
            this._type = Type;
            this._length = Length;
            this._defaultValue = DefaultValue;
            this._unsigned = Unsigned;
            this._isNull = isNull;
        }
        /*
        public static implicit operator Field(object value)
        {
            return new Field { _value = value };
        }
        */
        public static implicit operator int(Field f)
        {
            return (int)f._value;
        }
        
        public static implicit operator string(Field f)
        {
            return f._value.ToString();
        }

        public static implicit operator double(Field f)
        {
            return (double)f._value;
        }

        public string Name
        {
            get { return this._name; }
        }

        public string Length
        {
            get { return this._length; }
        }

        public string Type
        {
            get { return this._type; }
        }

        public object Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}
