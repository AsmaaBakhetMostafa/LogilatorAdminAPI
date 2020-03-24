using BiddingEngineAPI.EFCore.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }

        public int? FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public virtual User FromUser { get; set; }

        public int? ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public virtual User ToUser { get; set; }
        public bool? Isattached { get; set; }
        public AttachedType? attchedType { get; set; }

    }
}
