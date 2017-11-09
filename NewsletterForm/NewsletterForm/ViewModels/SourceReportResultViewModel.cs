﻿using NewsletterForm.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewsletterForm.ViewModels
{
    public class SourceReportResultViewModel
    {
        public Source Source { get; set; }

        public int Count { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double? Ratio { get; set; }
    }
}