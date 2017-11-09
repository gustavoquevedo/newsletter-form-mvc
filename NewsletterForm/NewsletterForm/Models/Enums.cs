
using System.ComponentModel.DataAnnotations;

namespace NewsletterForm.Models
{
    public enum Source
    {
        Advert = 0,
        [Display(Name = "Word of Mouth")]
        WordOfMouth = 1,
        Other = 2
    }
}