using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class FixedFiledDetails
    {
        [Key]
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual FixedFiledDetails Parent { get; set; }


        public int FixedFiledId { get; set; }

        [ForeignKey("FixedFiledId")]
        public virtual FixedFiled FixedFiled { get; set; }
    }
}
