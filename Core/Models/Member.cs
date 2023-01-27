using System.ComponentModel.DataAnnotations.Schema;

namespace Heuristics.TechEval.Core.Models
{

	public class Member
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
				
		public virtual Category Category { get; set; }
	}
}

