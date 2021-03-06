﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiDex.Models.Deck_Models
{
    public class DeckCreate
    {
        [Required]
        [MinLength(1, ErrorMessage ="Deck must have a title.")]
        [MaxLength(100, ErrorMessage ="Deck title is too long; please keep the title to less than 100 characters.")]
        [Display(Name ="Title")]
        public string DeckTitle { get; set; }

        [MaxLength(800, ErrorMessage ="Deck description is too long; please keep description to less than 800 characters.")]
        [Display(Name ="Description")]
        public string DeckDescription { get; set; }
        
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }

    }
}
