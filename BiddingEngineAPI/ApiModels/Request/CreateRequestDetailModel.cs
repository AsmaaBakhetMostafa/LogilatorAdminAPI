using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Request
{
    public class CreateRequestDetailModel
    {
        public int FormFieldId { get; set; }
        public string Value { get; set; }
        public int FieldTypeId { get; set; }
    }
}
