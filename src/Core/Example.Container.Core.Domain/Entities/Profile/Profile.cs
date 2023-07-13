using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OLT.Core;

namespace Example.Container.Core.Domain.Entities.Profile
{
    [Table("Record", Schema = DbSchemas.Profile)]
    public class Profile : OltEntityIdDeletable
    {
        [Column("ProfileId")]
        public override int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = default!;

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; } = default!;

        [StringLength(10)]
        public string? NameSuffix { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
