﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiDex.Models.Deck_Models
{
    public class DeckListItem
    {
        public int DeckId { get; set; }
        [Display(Name ="Title")]
        public string DeckTitle { get; set; }
        public string Category { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
