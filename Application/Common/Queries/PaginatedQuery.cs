using Application.Categories.Queries.ListCategories;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Queries
{
    public abstract class PaginatedQuery
    {
        public int Page { get; }
        public int ItemsPerPage { get; }

        public PaginatedQuery(int page, int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
