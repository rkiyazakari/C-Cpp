using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProjectServer.Models
{
    [Table("Connect")]
    public partial class Connect
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int TopicsId { get; set; }

        [ForeignKey(nameof(TopicsId))]
        [InverseProperty(nameof(Topic.Connects))]
        public virtual Topic Topics { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Connects")]
        public virtual User User { get; set; }
    }
}
