using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using D2CFL.Database.Interfaces;

namespace D2CFL.Database.Repository
{
    public class Entity<TType> : IEntity<TType>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TType Id { get; set; }

        public bool Equals(IEntity<TType> other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            // ReSharper disable once UsePatternMatching
            var item = obj as Entity<TType>;

            if (item == null)
            {
                return false;
            }

            return Equals(item);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }
    }
}
