using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Request
{
    public class RequestModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? FormId { get; set; }
        public int UserId { get; set; }
        public string FormNameAr { get; set; }
        public string FormNameEn { get; set; }
        public int? StatusId { get; set; }
        public int RequestType { get; set; }
        public List<RequestDetailModel> RequestDetails { get; set; }
    }
}
