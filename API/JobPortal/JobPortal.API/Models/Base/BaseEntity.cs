using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.API.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        //Id-ul este primary key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generates a value when a row is inserted
        // Options: None, Identity, Computed( when inserting or updating)
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? DateCreated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? DateModified { get; set; }

    }
}