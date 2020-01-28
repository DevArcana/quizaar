using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

        private Category()
        {
            // Needed by EF Core
        }

        public Category(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Category name cannot be null, whitespace or empty!", nameof(name));
            Name = name;
        }
    }
}
