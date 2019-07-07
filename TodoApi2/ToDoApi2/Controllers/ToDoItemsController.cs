using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi2.Interfaces;
using ToDoApi2.Models;

namespace ToDoApi2.Controllers
{
	[Route("api/[controller]")]
	public class ToDoItemsController : Controller
	{
		private readonly IToDoRepository _toDoRepository;

		public ToDoItemsController(IToDoRepository toDoRepository)
		{
			_toDoRepository = toDoRepository;
		}

		[HttpGet]
		public IActionResult List()
		{
			return Ok(_toDoRepository.All);
		}

		[HttpPost]
		public IActionResult Create([FromBody] ToDoItem item)
		{
			try
			{
				if (item == null || !ModelState.IsValid)
				{
					return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
				}
				bool itemExists = _toDoRepository.DoesItemExist(item.ID);
				if (itemExists)
				{
					return StatusCode(StatusCodes.Status409Conflict, ErrorCode.TodoItemIDInUse.ToString());
				}
				_toDoRepository.Insert(item);
			}
			catch (Exception)
			{
				return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
			}
			return Ok(item);
		}
		public enum ErrorCode
		{
			TodoItemNameAndNotesRequired,
			TodoItemIDInUse,
			RecordNotFound,
			CouldNotCreateItem,
			CouldNotUpdateItem,
			CouldNotDeleteItem
		}

	}

}
