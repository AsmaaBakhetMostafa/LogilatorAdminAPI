using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class RequestType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestType_Id { get; set; }

        public string RequestType_Name { get; set; }

    }
}
