using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RepoLayer
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            var prop = GetType().GetProperties()
                .Where(x => x.GetCustomAttributes<ToStringEntityAttribute>()
                .Any());
            foreach (var propertyInfo in prop)
            {
                var attribute = propertyInfo.GetCustomAttribute<ToStringEntityAttribute>();
                var value = propertyInfo.GetValue(this);
                if (propertyInfo.PropertyType.IsValueType)
                {
                    var d = Activator.CreateInstance(propertyInfo.PropertyType);
                    if (value.Equals(d))
                        continue;
                }
                else if (value == null)
                    continue;
                result.Append(attribute.Name == null
                    ? $"{propertyInfo.Name} : {value}\r\n"
                    : $"{attribute.Name} : {value}\r\n");
            }
            return result.ToString();
        }
    }
}
