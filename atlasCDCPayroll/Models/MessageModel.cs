using System;
using System.ComponentModel.DataAnnotations;

namespace atlasCDCPayroll.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageID { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string ReceiverName { get; set; }

        [Required(ErrorMessage = "Cannot send an empty text message.")]
        public string MessageText { get; set; }

        public DateTime DateTimeSent { get; set; } = DateTime.Now;
    }
}