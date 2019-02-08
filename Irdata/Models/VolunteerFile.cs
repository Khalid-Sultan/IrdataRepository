using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Irdata.Models
{
    public class VolunteerFile
    {
        public int VolunteerFileId { get; set; }
        [StringLength(255, ErrorMessage = "Invalid File Given.")]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public virtual VolunteeringEvents VolunteeringEvents { get; set; }
    }
    public class VolunteeringEvents
    {
        public int VolunteeringEventsId { get; set; }
        public string Date { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Title { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Organization { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 50)]
        public string Description { get; set; }
        public int Likes { get; set; }
        public virtual ICollection<VolunteerFile> VolunteerFiles { get; set; }
    }
    public class VolunteerDetails
    {
        public int VolunteerDetailsId { get; set; }
        public virtual VolunteeringEvents VolunteeringEvents { get; set; }
        public string ApplicationUsersAndLikes { get; set; }
    }
}