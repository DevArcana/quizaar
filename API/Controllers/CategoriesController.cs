using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Categories.Queries.ListCategories;
using Application.Common.ErrorHandling;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await ExecuteCommand(new ListCategoriesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post([FromBody] CreateCategoryCommand command)
        {
            return await ExecuteCommand(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            return await ExecuteCommand(new DeleteCategoryCommand(id));
        }
    }
}
