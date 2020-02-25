using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories.Commands;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.RenameCategory;
using Application.Categories.Queries.CategoryDetails;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Categories.Queries.ListCategories;
using Application.Common.ErrorHandling;
using Application.Common.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<ListCategoriesViewModel>>> ListCategories(int page = 1, int itemsPerPage = 10, string sort = null)
        {
            return await ExecuteCommand(new ListCategoriesQuery(page, itemsPerPage, sort));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetailsViewModel>> GetCategoryDetails(long id)
        {
            return await ExecuteCommand(new CategoryDetailsQuery {Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<CreateCategoryViewModel>> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            return await ExecuteCommand(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> RenameCategory(long id, [FromBody] RenameCategoryCommand command)
        {
            command.CategoryId = id;
            return await ExecuteCommand(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(long id)
        {
            return await ExecuteCommand(new DeleteCategoryCommand {Id = id});
        }
    }
}
