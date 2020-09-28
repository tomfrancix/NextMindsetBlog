using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Shop
    {
        [Key]
        public int Shop_Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        public Nullable<double> Price { get; set; }
        public int Stock { get; set; }
        public string Image1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
        public string Image2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile2 { get; set; }
    }
}