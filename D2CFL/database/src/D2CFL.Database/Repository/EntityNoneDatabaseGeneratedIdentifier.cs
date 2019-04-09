using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D2CFL.Database.Repository {
    public class EntityNoneDatabaseGeneratedIdentifier<TType> : Entity<TType>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override TType Id { get; set; }
    }
}
