using Flubr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class UserPostModel
    {
        [Key]
        public Guid PostId { get; set; }
        public string ReplyId { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        [Required]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(750)")]
        public string Post { get; set; }
        public DateTime Date { get; set; }
        public int Likes { get; set; }

        [ForeignKey("Id")]
        public ApplicationUser applicationUser { get; set; }

        

    }
}
