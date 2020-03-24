using BiddingEngineAPI.EFCore.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class FormFieldCreateModel
    {
        [Required(ErrorMessage = "NameAr is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "NameEn is required")]
        public string NameEn { get; set; }
        public string Hint { get; set; }


        public int FiledIndex { get; set; }
        public bool IsActive { get; set; } = true;
        public bool HasParent { get; set; }
        public bool IsRequried { get; set; }
        public bool HasChild { get; set; }

        public int order { get; set; }
        public int ParentIndex { get; set; }


        public int FiledType { get; set; }
        public int ID { get; set; }
        public int ParentID { get; set; }
        public bool? HasPredefined { get; set; }
        public int PredefinedID { get; set; }

        public ICollection<FieldOptionCreateModel> FiledOptions { get; set; }
    }
}
