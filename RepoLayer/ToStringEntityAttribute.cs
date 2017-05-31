using System;

namespace RepoLayer
{
    public class ToStringEntityAttribute : Attribute
    {
        public string Name { get; set; }
        public ToStringEntityAttribute(string name)
        {
            Name = name;
        }

        public ToStringEntityAttribute()
        {
        }
    }
}