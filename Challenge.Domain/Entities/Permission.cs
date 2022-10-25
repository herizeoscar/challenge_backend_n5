using Challenge.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Domain.Entities {

    [Table("Permissions")]
    public class Permission : BaseEntity<int> {

        [Required]
        public string EmployeeForename { get; set; } = string.Empty;

        [Required]
        public string EmployeeSurname { get; set; } = string.Empty;

        [Required]
        public int PermissionTypeId { get; set; } = 0;

        [Required]
        public DateTime PermissionDate { get; set; }

        public virtual PermissionType PermissionType { get; set; } 
    }
}
