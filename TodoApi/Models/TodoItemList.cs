using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoItemList
    {
        //public Guid OwnerId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TodoItem> TodoItems { get; set; }
    }
}
