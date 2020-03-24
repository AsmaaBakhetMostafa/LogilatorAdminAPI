﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.ApiModels.Request
{
    public class EditRequestModel
    {
        public int? FormId { get; set; }
        public int UserId { get; set; }
        public List<EditRequestDetailModel> RequestDetails { get; set; }
    }
}
