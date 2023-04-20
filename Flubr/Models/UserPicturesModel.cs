using Flubr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class UserPicturesModel
    {
        [Key]
        public Guid ImageId { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        [Required]
        public string Id { get; set; }
        [Column(TypeName = "nvarchar(1000)")]
        public string Image { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Id")]
        public ApplicationUser applicationUser { get; set; }
    }
}
