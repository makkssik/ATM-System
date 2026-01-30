using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Http.Models;

public class LoginUserRequest
{
    [Required]
    public int AccountNumber { get; set; }

    [NotNull]
    [Required]
    public string? PinCode { get; set; }
}