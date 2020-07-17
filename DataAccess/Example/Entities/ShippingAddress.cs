using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
	public class ShippingAddress : BaseEntity
	{
        public int ShippingAddressId { get; set; }

		[StringLength(150)]
		public string Addr1Text { get; set; }

		[StringLength(150)]
		public string Addr2Text { get; set; }

		[StringLength(100)]
		public string CityName { get; set; }

		[StringLength(2)]
		public string StateCode { get; set; }

		[StringLength(10)]
		public string ZipCode { get; set; }

		[StringLength(2)]
		public string CountryCode { get; set; }
		public Order Order { get; set; }
	}
}
