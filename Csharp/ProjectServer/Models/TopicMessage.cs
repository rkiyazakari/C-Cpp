using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProjectServer.Models
{
    [Table("TopicMessage")]
    public partial class TopicMessage
    {
        [Key]
        public int TopicMessageId { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTimeOffset CreatedAt { get; set; }
        
        public string Text { get; set; }
        
        public int UserId { get; set; }

        [Required]
        public int TopicsId { get; set; }

        [ForeignKey(nameof(TopicsId))]
        [InverseProperty(nameof(Topic.TopicMessages))]
        public virtual Topic Topics { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("TopicMessages")]
        public virtual User User { get; set; }
    }
}
