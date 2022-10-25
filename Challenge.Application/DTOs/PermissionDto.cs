using Challenge.Domain.Entities;

namespace Challenge.Application.DTOs {
    public class PermissionDto {

        public int Id { get; set; }

        public string EmployeeForename { get; set; } = string.Empty;

        public string EmployeeSurname { get; set; } = string.Empty;

        public int PermissionTypeId { get; set; } = 0;

        public DateTime PermissionDate { get; set; }

        public virtual PermissionType? PermissionType { get; set; }
    }
}
