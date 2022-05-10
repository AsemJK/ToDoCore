using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ToDoCore.Models
{
    public class ToDo
    {
		public int Id { get; set; } // int
		public string ToDoSubject { get; set; } // longtext
		public string TeamMember { get; set; } // varchar(255)
		public DateTime? CreationDate { get; set; } // datetime
		public string LastStatus { get; set; } // varchar(255)
		public decimal? CompanyId { get; set; }
		public string ImageFileName { get; set; }
	}
}
