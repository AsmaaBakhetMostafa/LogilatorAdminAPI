using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? FormId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int RequestId { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        [ForeignKey("FormId")]
        public Form Form { get; set; }
        public List<BidDetail> BidDetails { get; set; }

        [ForeignKey("RequestId")]
        public Request Request { get; set; }
    }
}
