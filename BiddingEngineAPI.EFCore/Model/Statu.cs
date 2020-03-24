using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiddingEngineAPI.EFCore.Model
{
    public class Statu
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Request> Requests { get; set; }

    }
}
