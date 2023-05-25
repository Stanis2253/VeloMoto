namespace VeloMotoAPI.Models.DTO
{
    public class UsersDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        private string id;

        public string RoleId
        {
            get { return id = "1"; }
            set { id = value; }
        }

    }
}
