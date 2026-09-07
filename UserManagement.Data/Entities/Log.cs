using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class Log
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Log ID")]
    public long Id { get; set; }

    [DisplayName("User ID")]
    public long UserId { get; set; }    // For future expansion to track a logged in user effecting the entity

    public User User { get; set; } = default!;  // For future expansion to track a logged in user effecting the entity
    
    [StringLength(100)]
    [DisplayName("Log Summary")]
    public string Summary { get; set; } = default!;
    
    [DisplayName("Full Detail")]
    public string? Detail { get; set; } = default!;
    
    [DisplayName("Created")] 
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    [DisplayName("Affected User ID")]
    public long? AffectedUserId { get; set; }    // User Account affected by the change

    [DisplayName("Affected User")]
    public User? AffectedUser { get; set; } = default!;  // User Account affected by the change
}