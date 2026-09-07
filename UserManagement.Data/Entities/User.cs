using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required(ErrorMessage = "Forename is required")]
    [StringLength(50, ErrorMessage =  "Forename must be less than 50 characters")]
    public string Forename { get; set; } = default!;
    
    [Required(ErrorMessage = "Surname is required")]
    [StringLength(50, ErrorMessage =  "Surname must be less than 50 characters")]
    public string Surname { get; set; } = default!;
    
    [Required(ErrorMessage = "A valid email address is required")]
    [DataType(DataType.EmailAddress, ErrorMessage =  "A valid email address is required")]
    public string Email { get; set; } = default!;
    
    public bool IsActive { get; set; }
    
    [Required(ErrorMessage = "Please provide a Date of Birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    
    [DisplayName("Date of Birth")]
    public DateTime DateOfBirth { get; set; } = new DateTime(DateTime.Now.AddYears(-16).Year, 1, 1); // Start age around 16 years old to work in UK
    
    [Required(ErrorMessage = "Organisation is required")]
    [StringLength(50, ErrorMessage =  "Organisation must be less than 50 characters")]
    public string Organisation { get; set; } = default!;
    
    [Required(ErrorMessage = "Job Title is required")]
    [StringLength(50, ErrorMessage =  "Job Title must be less than 50 characters")]
    public string JobTitle { get; set; } = default!;
}
