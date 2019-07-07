using System.Collections.Generic;
using ToDoApi2.Models;

namespace ToDoApi2.Interfaces
{
	public interface IToDoRepository
	{
		bool DoesItemExist(string id);
		IEnumerable<ToDoItem> All { get; }
		ToDoItem Find(string id);
		void Insert(ToDoItem item);
		void Update(ToDoItem item);
		void Delete(string id);
	}
}