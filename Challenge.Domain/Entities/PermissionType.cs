using Challenge.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities {

    [Table("PermissionTypes")]
    public class PermissionType : BaseEntity<int> {

        [Required]
        public string Description { get; set; } = string.Empty; 

    }
}
