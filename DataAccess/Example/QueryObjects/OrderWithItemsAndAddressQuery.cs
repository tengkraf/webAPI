using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.QueryObjects
{
    public class OrderWithItemsAndAddressQuery
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int OrderedItemId { get; set; }
        public int ItemId { get; set; }
        public int ItemCnt { get; set; }
        public int ShippingAddressId { get; set; }
        public string Addr1Text { get; set; }
        public string Addr2Text { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
    }
}
