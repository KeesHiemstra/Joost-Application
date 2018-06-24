using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JoostLib.Models
{
	class Tag
	{
		[Key]
		public int Id { get; set; }

		[StringLength(64)]
		public string Name { get; set; }

	}
}
