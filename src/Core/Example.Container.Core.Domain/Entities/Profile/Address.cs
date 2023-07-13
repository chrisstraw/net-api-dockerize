using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OLT.Core;

namespace Example.Container.Core.Domain.Entities.Profile
{
    [Table(nameof(Address), Schema = DbSchemas.Profile)]
    public class Address : OltEntityIdDeletable
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = default!;

        [StringLength(50)]
        public string? Street { get; set; }

        [StringLength(50)]
        public string? Street2 { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(10)]
        public string? State { get; set; }

        [StringLength(10)]
        public string? ZipCode { get; set; }
    }
}
