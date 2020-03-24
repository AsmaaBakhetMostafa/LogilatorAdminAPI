using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Bid
{
    public class BidDetailModel
    {
        public int Id { get; set; }
        public int BidId { get; set; }
        public int? FormFieldId { get; set; }
        public string Value { get; set; }
        public string ValueText { get; set; }
        public int FieldTypeId { get; set; }
        public string formFieldNameAr { get; set; }
        public string formFieldNameEn { get; set; }
        public int Order { get; set; }
    }
}
