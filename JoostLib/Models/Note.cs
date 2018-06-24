using JoostLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JoostLib
{
	class Note
	{
		[Key]
		public int Id { get; set; }

		public DateTime DTCreate { get; set; }

		public DateTime DTStart { get; set; }

		public DateTime? DTFinish { get; set; }

		[StringLength(2048)]
		public string Message { get; set; }

		[StringLength(64)]
		public string Event { get; set; }

		[StringLength(64)]
		public string Source { get; set; }

		[StringLength(64)]
		public string UserName { get; set; }

		[StringLength(64)]
		public string DeviceName { get; set; }

		public virtual IList<Tag> GetTags { get; set; }

		public Note()
		{
			DTCreate = DateTime.UtcNow;
		}
	}
}
