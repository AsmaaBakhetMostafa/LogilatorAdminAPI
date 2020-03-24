using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserType_Id { get; set; }

        public string UserType_Name { get; set; }



    }
}
