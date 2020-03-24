using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class User 
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActivationCode { get; set; }
        public int? UserType_Id { get; set; }

        [ForeignKey("UserType_Id")]
        public UserType UserType { get; set; }
        public int RoleID { get; set; }
        //public Role Role { get; set; }
        public bool Is_Online { get; set; }
        public bool Is_Deleted { get; set; }
        public bool Is_SProvider { get; set; }

        [NotMapped]
        public string Token { get; set; }

    }
}
