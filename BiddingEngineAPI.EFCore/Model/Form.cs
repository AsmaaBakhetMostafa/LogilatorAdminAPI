using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class Form
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Arbic is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Name English is required")]
        public string NameEn { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int FormType { get; set; }
        public int UserType { get; set; }
        public int RequestType { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<FormField> Fields { get; set; }

    }
}
