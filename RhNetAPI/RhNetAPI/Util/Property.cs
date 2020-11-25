using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RhNetAPI.Util
{
    public class Property
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Required { get; set; }
        public bool AutoGenerateField { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public Type type { get; set; }
        public string Type_Description { get; set; }

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
                        type = _properties.ElementAt(i).PropertyType,
                        Description = _display.Description,
                        Name = _display.Name,
                        AutoGenerateField = _display.AutoGenerateField,
                        Order = _display.Order                        
                    };

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
                   
                }
            }

            return properties;
        }
    }
}
