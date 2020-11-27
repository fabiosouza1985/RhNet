using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel;

namespace RhNetAPI.Util
{
    public class Property
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Required { get; set; }
        public bool AutoGenerateField { get; set; }

        public bool AutoGenerateFilter { get; set; }
        public Int64 Minimum { get; set; }
        public Int64 Maximum { get; set; }
        public string Type_Description { get; set; }

        public bool ReadOnly { get; set; }
        public static List<Property> GetProperties(Type type)
        {
            List<Property> properties = new List<Property>();

            List<PropertyInfo> _properties = type.GetProperties().ToList();

            for(var i = 0; i < _properties.Count(); i++)
            {
                DisplayAttribute _display = (DisplayAttribute)_properties.ElementAt(i).GetCustomAttributes(typeof (DisplayAttribute), true).FirstOrDefault();

                if (_display != null)
                {
                    Property property = new Property()
                    {                       
                        Description = _display.Description,
                        Name = _display.Name,
                        AutoGenerateField = _display.AutoGenerateField,
                        Order = _display.Order   ,
                        Minimum = 0,
                        Maximum = Int64.MaxValue,
                        Required = false,
                        Type_Description = "",
                        AutoGenerateFilter = _display.AutoGenerateFilter
                    };

                    property.Name = property.Name[0].ToString().ToLower() + property.Name.Substring(1);
                    RequiredAttribute required = (RequiredAttribute)_properties.ElementAt(i).GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault();

                    if (required != null)
                    {
                        property.Required = true;
                    }
                    else
                    {
                        property.Required = false;
                    }

                    StringLengthAttribute stringLength = (StringLengthAttribute)_properties.ElementAt(i).GetCustomAttributes(typeof(StringLengthAttribute), true).FirstOrDefault();

                    if(stringLength != null)
                    {
                        property.Minimum = stringLength.MinimumLength;
                        property.Maximum = stringLength.MaximumLength;
                    }

                    switch (Type.GetTypeCode(_properties.ElementAt(i).PropertyType))
                    {
                        case TypeCode.String:
                            property.Type_Description = "string";
                            break;
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:                        
                            property.Type_Description = "int";
                            break;
                        case TypeCode.Boolean:
                            property.Type_Description = "boolean";
                            break;
                        case TypeCode.Char:
                            property.Type_Description = "char";
                            break;
                        case TypeCode.DateTime:
                            property.Type_Description = "datetime";
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Single:
                            property.Type_Description = "decimal";
                            break;
                        case TypeCode.Object:
                           
                            property.Type_Description = _properties.ElementAt(i).PropertyType.ToString();
                            break;

                    }

                    ReadOnlyAttribute isreadonly = (ReadOnlyAttribute)_properties.ElementAt(i).GetCustomAttributes(typeof(ReadOnlyAttribute), true).FirstOrDefault();

                    if (isreadonly != null)
                    {
                        property.ReadOnly = isreadonly.IsReadOnly;
                    }
                    else
                    {
                        property.ReadOnly = false;
                    }
                    properties.Add(property);
                }
            }

            return properties;
        }
    }
}
