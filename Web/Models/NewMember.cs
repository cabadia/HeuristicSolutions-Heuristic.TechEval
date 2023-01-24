using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heuristics.TechEval.Web.Models {

	/// <summary>
	/// DTO representing the creation of a new Member
	/// </summary>
	public class NewMember
	{
		[Required(ErrorMessage = "Please enter name.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter email.")]
		[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }
	}
}