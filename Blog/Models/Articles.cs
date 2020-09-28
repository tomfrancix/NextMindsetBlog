using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Articles
    {
        [Key]
        public int Art_Id { get; set; }
        [ForeignKey("Categories")]
        public Nullable<int> Cat_Id { get; set; }
        [ForeignKey("Subcategories")]
        public Nullable<int> Sub_Id { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "A Title is required")]
        [StringLength(160)]
        public string Title { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile { get; set; }
        [Required(ErrorMessage = "Your Name is required")]
        [StringLength(160)]
        public string Author_Name { get; set; }
        public string Author_Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageFile2 { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "A Leading line is required")]
        [StringLength(1000)]
        public string Lead { get; set; }
        [Required(ErrorMessage = "A Snippet is required")]
        [StringLength(2500)]
        public string Snippet { get; set; }
        [Required(ErrorMessage = "Content is required")]
        [AllowHtml]
        public string Content { get; set; }

        public virtual Categories Categories { get; set; }
        public virtual Subcategories Subcategories { get; set; }
        public virtual ICollection<Tagmap> Tagmap { get; set; }
        public virtual ICollection<Imagemap> Imagemap { get; set; }
    }
}