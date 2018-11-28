namespace StrandBookstore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [StringLength(128)]
        public string Id { get; set; }

        public int? BookID { get; set; }

        [Required]
        [StringLength(120)]
        public string Title { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(22)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(64)]
        public string Author { get; set; }

        public decimal Price { get; set; }

        public bool Purchased { get; set; }

        public int Quantity { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Book Book { get; set; }
    }
}
