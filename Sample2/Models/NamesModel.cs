using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample2.Models
{
    public class NamesModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s,.\-_']+$", ErrorMessage = "Names can only contain letters, spaces, and the characters ,.-_'")]
        public string Names { get; set; }

        [Required]
        [Range(2, 10, ErrorMessage = "Number of teams must be between 2 and 10.")]
        public int NumberOfTeams { get; set; }
    }
}