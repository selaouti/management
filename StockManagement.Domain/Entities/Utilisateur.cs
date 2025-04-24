using System.ComponentModel.DataAnnotations;

namespace StockManagement.Domain.Entities;

public class Utilisateur
{
    [Key]
    public int IdUtilisateur { get; set; } // Clé primaire pour Utilisateur

    [Required]
    public required string Nom { get; set; } // Nom de l'utilisateur

    [Required]
    public required string Prenom { get; set; } // Prénom de l'utilisateur

    [Required]
    [EmailAddress]
    public required string Email { get; set; } // Email de l'utilisateur

    [Required]
    public required string Role { get; set; } // Rôle de l'utilisateur (ex: Admin, Employé)

    [Required]
    [MinLength(8)]
    public required string Password { get; set; } // Mot de passe (ajouté avec validation de longueur minimale)
}