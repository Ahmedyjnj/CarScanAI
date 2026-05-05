using Microsoft.AspNetCore.Identity;
using Scan.DAL.Models.Car;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
   

    [Required, MaxLength(100)]
    public string Name { get; set; }


    //[Required, EmailAddress]
    //public string Email { get; set; }

    //[MaxLength(20)]
    //public string Phone { get; set; }  //identity has phone number

    //[Required]
    //public string PasswordHash { get; set; }


    public string ProfileImage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Active";

    // Navigation
    public ICollection<Car> Cars { get; set; }
    public ICollection<Detection> Detections { get; set; }
}
