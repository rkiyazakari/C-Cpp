using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProjectServer.Models
{
    [Table("DirectMessage")]
    public partial class DirectMessage
    {
        [Key]
        public int DirectMessageId { get; set; }

        [Required]
        public string Text { get; set; }

        [Column(TypeName = "time with time zone")]
        public DateTimeOffset CreatedAt { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        [ForeignKey(nameof(Receiver))]
        [InverseProperty(nameof(User.DirectMessageReceiverNavigations))]
        public virtual User ReceiverNavigation { get; set; }

        [ForeignKey(nameof(Sender))]
        [InverseProperty(nameof(User.DirectMessageSenderNavigations))]
        public virtual User SenderNavigation { get; set; }
    }
}
