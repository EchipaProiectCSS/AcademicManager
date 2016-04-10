using System;
using System.Collections.Generic;
using System.Linq;
using Database.Implementations.Internal.Domain;

namespace ProcessManagement.Helper
{
    public class AutoMapper
    {
        private readonly object dataDo;
        private readonly Dictionary<string, string> row;

        public AutoMapper(object dataDo, Dictionary<string,string> row)
        {
            this.dataDo = dataDo;
            this.row = row;
        }

        public void Start()
        {
            foreach (var property in dataDo.GetType().GetProperties())
            {
                SetRowValueToObject(property.Name);
            }
        }

        private void SetRowValueToObject(string columnName)
        {
            //TODO:test these scenario's

            var type = dataDo.GetType().GetProperty(columnName).PropertyType;

            CheckIfProertyMatchNamingConvention(columnName);

            SetColumnValueToObjectProperty(columnName, type);
        }

        private void SetColumnValueToObjectProperty(string columnName, Type type)
        {
            var rowValue = row.Single(k => k.Key.Equals(columnName)).Value;

            dataDo.GetType().GetProperty(columnName).SetValue(dataDo, Cast(rowValue, type));
        }

        private void CheckIfProertyMatchNamingConvention(string columnName)
        {
            if (row.Count(k => k.Key.Equals(columnName)) == 0)
            {
                throw new Exception("The object " + dataDo.GetType().Name + " should respect the name convention for " +
                                    columnName);
            }
        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }
    }
}
