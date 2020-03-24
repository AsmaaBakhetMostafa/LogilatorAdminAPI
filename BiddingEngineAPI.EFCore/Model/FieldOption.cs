using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BiddingEngineAPI;
using BiddingEngineAPI.EFCore.Enum;

namespace BiddingEngineAPI.EFCore.Model
{

    public class FieldOption
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Arbic is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Name English is required")]
        public string NameEn { get; set; }
        public bool HasParent { get; set; } = false;
        public bool HasChild { get; set; } = false;

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual FieldOption Parent { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public int FormFiledId { get; set; }
        [ForeignKey("FormFiledId")]
        public virtual FormField FormFiled { get; set; }

    }
}
