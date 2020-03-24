using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BiddingEngineAPI;
using BiddingEngineAPI.EFCore.Enum;

namespace BiddingEngineAPI.EFCore.Model
{
    public class FormField
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Arbic is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Name English is required")]
        public string NameEn { get; set; }
        public string Hint { get; set; }

        public bool IsRequired { get; set; } = false;
        public bool HasParent { get; set; } = false;
        public bool HasChild { get; set; } = false;

        [Required(ErrorMessage = "Name Arbic is required")]
        public FieldType FiledType { get; set; }
        public int Order { get; set; }

        public int FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual FormField Parent { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> HasPredefined { get; set; }
        public int?  PredfinedID { get; set; }

        public virtual ICollection<FieldOption> FiledOptions { get; set; }


    }
}
