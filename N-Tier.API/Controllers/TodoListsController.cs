﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N_Tier.Application.Models.TodoItem;
using N_Tier.Application.Models.TodoList;
using N_Tier.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N_Tier.API.Controllers
{
    [Authorize]
    public class TodoListsController : ApiController
    {
        private readonly ITodoListService _todoListService;
        private readonly ITodoItemService _todoItemService;

        public TodoListsController(ITodoListService todoListService, ITodoItemService todoItemService)
        {
            _todoListService = todoListService;
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListResponseModel>>> GetAll()
        {
            return Ok(await _todoListService.GetAllAsync());
        }

        [HttpGet("{id}/todoItems")]
        public async Task<ActionResult<IEnumerable<TodoItemResponseModel>>> GetAllTodoItemsAsync(Guid id)
        {
            return Ok(await _todoItemService.GetAllByListIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAsync(CreateTodoListModel createTodoListModel)
        {
            return Ok(await _todoListService.CreateAsync(createTodoListModel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> UpdateAsync(Guid id, UpdateTodoListModel updateTodoListModel)
        {
            return Ok(await _todoListService.UpdateAsync(id, updateTodoListModel));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _todoListService.DeleteAsync(id);

            return NoContent();
        }
    }
}
