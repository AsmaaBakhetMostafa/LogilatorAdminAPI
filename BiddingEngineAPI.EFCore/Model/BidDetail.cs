using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class BidDetail
    {
        [Key]
        public int Id { get; set; }
        public int BidId { get; set; }
        public int? FormFieldId { get; set; }
        public string Value { get; set; }
        
        [ForeignKey("FormFieldId")]
        public FormField FormField { get; set; }
        
        [ForeignKey("BidId")]
        public Bid Bid { get; set; }
    }
}
