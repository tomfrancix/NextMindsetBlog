using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Tags
    {
        [Key]
        public int Tag_Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Tagmap> Tagmap { get; set; }
    }
}