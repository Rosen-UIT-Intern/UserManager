namespace UserManager.Contract.DTOs
{
    public class GroupDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public OrganizationDTO Organization { get; set; }
    }
}