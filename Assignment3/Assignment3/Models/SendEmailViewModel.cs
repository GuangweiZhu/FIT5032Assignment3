using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_Week08A.Models
{
    public class SendEmailViewModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Invalid email")]
        public string to { get; set; }

        [Required]
        public string subject { get; set; }

        [Required]
        public string body { get; set; }

        public HttpPostedFileBase[] file { get; set; }

    }
}