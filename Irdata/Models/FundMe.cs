using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Irdata.Models
{
    public class FundMeFile
    {
        public int FundMeFileId { get; set; }
        [StringLength(255 , ErrorMessage = "Invalid File Given.")]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public virtual FundMe FundMe { get; set; }
    }
    public class Funder
    {
        public int FunderId { get; set; }
        public virtual FundMe FundMe { get; set; }
        public string ApplicationUsersAndPledge { get; set; }
    }
    public class FundMe
    {
        public int FundMeId { get; set; }
        public string date { get; set; }
        [Range(1,4)]
        public int duration { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.",MinimumLength = 10)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 50)]
        public string Description { get; set; }
        [Range(1000,1000000)]
        public int TargetFunds { get; set; }
        public int CurrentFunds { get; set; }
        public int status { get; set; }
        public virtual ICollection<FundMeFile> FundMeFiles { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}