using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Subcategories
    {
        [Key]
        public int Sub_Id { get; set; }
        [ForeignKey("Categories")]
        public Nullable<int> Cat_Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
        public string Snippet { get; set; }
        public string Summary { get; set; }
        public string Introduction { get; set; }

        public virtual Categories Categories { get; set; }
        public virtual ICollection<Articles> Articles { get; set; }
    }
}