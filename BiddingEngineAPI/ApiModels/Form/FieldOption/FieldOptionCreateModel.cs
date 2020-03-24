using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class FieldOptionCreateModel
    {
        [Required(ErrorMessage = "NameAr is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "NameEn is required")]
        public string NameEn { get; set; }

        public int OptionIndex { get; set; }
        public bool IsActive { get; set; } = true;
        public bool HasParent { get; set; }
        public bool HasChild { get; set; }

        //public int RelatedFiledIndex { get; set; }
        public int parentOptionIndex { get; set; }
        public int FiledID { get; set; }
        public int ParentID { get; set; }
        public int ID { get; set; }
        public int ParentFiledID { get; set; }



    }
}
