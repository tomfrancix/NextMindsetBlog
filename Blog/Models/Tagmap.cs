using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Tagmap
    {
        [Key]
        public int Tagmap_Id { get; set; }
        [ForeignKey("Tags")]
        public Nullable<int> Tag_Id { get; set; }
        [ForeignKey("Articles")]
        public int? Art_Id { get; set; }

        public virtual Tags Tags { get; set; }
        public virtual Articles Articles { get; set; }
    }
}