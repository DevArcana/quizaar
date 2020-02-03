using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        private Category()
        {
            // Needed by EF Core
        }

        public Category(string category)
        {
            Name = category;
        }
    }
}
