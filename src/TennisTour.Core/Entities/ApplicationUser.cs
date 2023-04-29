using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TennisTour.Core.Entities;

public class ApplicationUser : IdentityUser {
    public virtual Ranking Ranking { get; set; }
    public virtual ContenderInfo ContenderInfo { get; set; }

    public virtual ICollection<TournamentRegistration> TournamentRegistrations { get; set; }
    public virtual ICollection<ApplicationUser> FavoriteContenders { get; set; }
    public virtual ICollection<ApplicationUser> FavoritedByUsers { get; set; }
}
