using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Categories.Queries.ListCategories
{
    public class ListCategoriesViewModel : IMapFrom<Category>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
