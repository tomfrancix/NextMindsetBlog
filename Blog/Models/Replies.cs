using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Replies
    {
        [Key]
        public int Reply_Id { get; set; }
        
        [ForeignKey("Comments")]
        [DisplayName("Replying to:")]
        public Nullable<int> Com_Id { get; set; }
        [Required(ErrorMessage = "Your First Name is required")]
        [StringLength(160)]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Your Last Name is required")]
        [StringLength(160)]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Your Email is required")]
        [StringLength(160)]
        public string Email { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Did you forget to write your reply? [Max:1000 Characters]")]
        [StringLength(1000)]
        public string Content { get; set; }
        
        public virtual Comments Comments { get; set; }
    }
}