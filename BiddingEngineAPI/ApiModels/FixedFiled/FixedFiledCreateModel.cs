using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class FixedFiledModel
    {
       

        [Required(ErrorMessage = "NameAr is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "NameEn is required")]
        public string NameEn { get; set; }


        //public ICollection<FormFieldCreateModel> Fileds { get; set; }

    }
}
