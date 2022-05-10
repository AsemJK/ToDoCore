using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ToDoCore.Models
{
    public class ToDoDetail
    {
		public int Id { get; set; } // int
		public int? ToDoId { get; set; } // int
		public int? Slno { get; set; } // int
		public string TeamMember { get; set; } // varchar(255)
		public string Notes { get; set; } // longtext
		public DateTime? ExtraDate { get; set; }
	}
}
