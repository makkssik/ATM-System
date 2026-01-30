using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Http.Models;

public class CreateAccountRequest
{
    [Required]
    public Guid SessionKey { get; set; }

    [Required]
    public int AccountNumber { get; set; }

    [NotNull]
    [Required]
    [MinLength(4)]
    public string? PinCode { get; set; }
}