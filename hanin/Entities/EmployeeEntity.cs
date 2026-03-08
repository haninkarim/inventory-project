namespace hanin.Entities
{
    public class EmployeeEntity : EntityBase
    {
        public string Name { get; set; } 
        public string Position { get; set; }
        public int DepartmentId { get; set; }
    }
}
