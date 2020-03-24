using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Bid
{
    public class BidModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? FormId { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public List<BidDetailModel> BidDetails { get; set; }
    }
}
