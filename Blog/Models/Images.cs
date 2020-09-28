using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Images
    {
        [Key]
        public int Img_Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
        public string Alt { get; set; }


        public virtual ICollection<Imagemap> Imagemap { get; set; }
    }
}