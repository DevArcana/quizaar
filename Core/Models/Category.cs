using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        private readonly List<Subcategory> _subcategories = new List<Subcategory>();
        public IReadOnlyCollection<Subcategory> Subcategories => _subcategories.AsReadOnly();

        private Category()
        {

        }

        public Category(string name, List<Subcategory> subcategories)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name can not be null, empty or whitespace.", nameof(name));

            Name = name;
            _subcategories = subcategories ?? throw new ArgumentNullException(nameof(subcategories), "You must provide a list of subcategories, it can be empty!");
        }
    }
}
