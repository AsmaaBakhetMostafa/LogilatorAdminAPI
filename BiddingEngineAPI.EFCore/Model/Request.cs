using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? FormId { get; set; }
        public int UserId { get; set; }
        public int? StatusId { get; set; }
        
        [ForeignKey("FormId")]
        public Form Form { get; set; }
        public List<RequestDetail> RequestDetails { get; set; }
        public List<Bid> Bids { get; set; }
        
        [ForeignKey("StatusId")]
        public Statu Status { get; set; }
    }
}
