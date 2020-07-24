using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSIMS.Models
{
    public class Address
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int addressID { get; set; }
        [Required, MaxLength(100)]
        public string streetNumber { get; set; }
        [Required, MaxLength(100)]
        public string street { get; set; }
        [Required, MaxLength(100)]
        public string suburb { get; set; }
        [Required, MaxLength(100)]
        public string city { get; set; }
        [Required]
        public int postcode { get; set; }
        [Required, MaxLength(100)]
        public string country { get; set; }
        public int studentID { get; set; }
        public Student student { get; set; }
    }
}
