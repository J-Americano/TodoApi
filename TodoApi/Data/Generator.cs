using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class Generator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //Get context to begin adding defaults to in memory database
            using (var context = new TodoContext(
                serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>()))
            {
                // Look for any item types.
                if (context.TodoItemTypes.Any())
                {
                    return;   // Data already seeded.
                }

                var workType = context.Add(new TodoItemType { Name = "Work", Description = "Tasks for work" }).Entity;
                var choreType = context.Add(new TodoItemType { Name = "Chore", Description = "Tasks for around the house" }).Entity;
                var hobbyType = context.Add(new TodoItemType { Name = "Hobby", Description = "Tasks for hobbies" }).Entity;
                var groceryType = context.Add(new TodoItemType { Name = "Grocery", Description = "Item needed when shopping" }).Entity;

                var ChoreList = new TodoItemList
                {
                    Name = DateTime.Today.DayOfWeek + " Chores",
                    Description = "Tracking chores that aren't finished",
                    DueDate = DateTime.Today,
                    TodoItems = new List<TodoItem>()
                };

                ChoreList.TodoItems.Add(new TodoItem()
                {
                    Name = "Walk the dog",
                    IsComplete = false,
                    OrderPosition = 0,
                    SelectedTodoItemType = choreType
                });

                ChoreList.TodoItems.Add(new TodoItem()
                {
                    Name = "Clean up yard",
                    IsComplete = false,
                    OrderPosition = 1,
                    SelectedTodoItemType = choreType
                });

                ChoreList.TodoItems.Add(new TodoItem()
                {
                    Name = "Vacuum basement",
                    IsComplete = false,
                    OrderPosition = 2,
                    SelectedTodoItemType = choreType
                });

                var WorkList = new TodoItemList
                {
                    Name = "Day Job",
                    Description = "Tracking day to day work tasks",
                    DueDate = DateTime.Today.AddDays(10),
                    TodoItems = new List<TodoItem>()
                };

                WorkList.TodoItems.Add(new TodoItem()
                {
                    Name = "Fillout Form",
                    IsComplete = false,
                    OrderPosition = 0,
                    SelectedTodoItemType = workType
                });
                
                WorkList.TodoItems.Add(new TodoItem()
                {
                    Name = "Peer Review",
                    IsComplete = false,
                    OrderPosition = 1,
                    SelectedTodoItemType = workType
                });
                
                WorkList.TodoItems.Add(new TodoItem()
                {
                    Name = "File documentation",
                    IsComplete = false,
                    OrderPosition = 2,
                    SelectedTodoItemType = workType
                });

                context.TodoItemLists.Add(ChoreList);
                context.TodoItemLists.Add(WorkList);

                context.SaveChanges();
            }
        }
    }
}