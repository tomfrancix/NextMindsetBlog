using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Imagemap
    {
        [Key]
        public int Imagemap_Id { get; set; }
        [ForeignKey("Images")]
        public Nullable<int> Img_Id { get; set; }
        [ForeignKey("Articles")]
        public Nullable<int> Art_Id { get; set; }
        
        public virtual Images Images { get; set; }
        public virtual Articles Articles { get; set; }
    }
}