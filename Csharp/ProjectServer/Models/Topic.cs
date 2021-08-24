using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProjectServer.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Connects = new HashSet<Connect>();
            TopicMessages = new HashSet<TopicMessage>();
        }

        [Key]
        public int TopicsId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        [InverseProperty(nameof(Connect.Topics))]
        public virtual ICollection<Connect> Connects { get; set; }

        [InverseProperty(nameof(TopicMessage.Topics))]
        public virtual ICollection<TopicMessage> TopicMessages { get; set; }
    }
}
