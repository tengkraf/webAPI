using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
	public class BaseEntity
	{
		public DateTime CreateDate { get; set; }

		[StringLength(100)]
		public string CreateLogonId { get; set; }

		public DateTime? LastUpdateDate { get; set; }

		[StringLength(100)]
		public string LastUpdateLogonId { get; set; }
	}
}
