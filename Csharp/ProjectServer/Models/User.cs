using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProjectServer.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Connects = new HashSet<Connect>();
            DirectMessageReceiverNavigations = new HashSet<DirectMessage>();
            DirectMessageSenderNavigations = new HashSet<DirectMessage>();
            TopicMessages = new HashSet<TopicMessage>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [InverseProperty(nameof(Connect.User))]
        public virtual ICollection<Connect> Connects { get; set; }

        [InverseProperty(nameof(DirectMessage.ReceiverNavigation))]
        public virtual ICollection<DirectMessage> DirectMessageReceiverNavigations { get; set; }

        [InverseProperty(nameof(DirectMessage.SenderNavigation))]
        public virtual ICollection<DirectMessage> DirectMessageSenderNavigations { get; set; }

        [InverseProperty(nameof(TopicMessage.User))]
        public virtual ICollection<TopicMessage> TopicMessages { get; set; }
    }
}
