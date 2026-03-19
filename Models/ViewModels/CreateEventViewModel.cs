using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementApp.Models.ViewModels
{
    public class CreateEventViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Event Date is required")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Event Time is required")]
        [DataType(DataType.Time)]
        public TimeSpan EventTime { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(300)]
        public string Location { get; set; }
    }
}
