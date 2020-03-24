using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Bid
{
    public class CreateBidModel
    {
        public int? FormId { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int RequestId { get; set; }
        public List<CreateBidDetailModel> BidDetails { get; set; }
    }
}
