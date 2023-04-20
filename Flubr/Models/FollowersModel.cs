using Flubr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class FollowersModel
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        [Required]
        public string FollowingId { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey("FollowingId")]
        public ApplicationUser applicationUser { get; set; }

       
    }
}
