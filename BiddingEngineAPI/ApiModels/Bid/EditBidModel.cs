using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Bid
{
    public class EditBidModel
    {
        public int? FormId { get; set; }
        public int UserId { get; set; }
        public List<EditBidDetailModel> BidDetails { get; set; }
    }
}
