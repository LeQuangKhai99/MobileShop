namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public long? IDCustomer { get; set; }

        [StringLength(250)]
        public string ShipName { get; set; }

        [StringLength(50)]
        public string ShipPhone { get; set; }

        [StringLength(250)]
        public string ShipAddress { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
