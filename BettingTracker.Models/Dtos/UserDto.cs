using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingTracker.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public decimal Profit { get; set; }
        public decimal Roi { get; set; }
    }
}
