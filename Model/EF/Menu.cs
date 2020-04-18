namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public long ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool? Status { get; set; }

        public long? TypeID { get; set; }
    }
}
