using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Categories
    {
        [Key]
        public int Cat_Id { get; set; }
        [Required(ErrorMessage = "A Title is required")]
        [StringLength(160)]
        public string Title { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
        public string Snippet { get; set; }
        public string Summary { get; set; }
        public string Introduction { get; set; }

        public virtual ICollection<Subcategories> Subcategories { get; set; }
        public virtual ICollection<Articles> Articles { get; set; }

    }
}