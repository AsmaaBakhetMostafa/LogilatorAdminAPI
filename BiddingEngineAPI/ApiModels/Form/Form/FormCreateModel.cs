using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class FormCreateModel
    {
       

        [Required(ErrorMessage = "NameAr is required")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "NameEn is required")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "Descr is required")]
        public string Descr { get; set; }
        //   public bool IsActive { get; set; } = true;
        public int RequestType { get; set; }
        public int UserType { get; set; }


        public ICollection<FormFieldCreateModel> Fileds { get; set; }

    }
}
