namespace TomadaStore.Models.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Status { get; set; }

        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = true;
        }

        public Customer(string firstName, string lastName, string email, string? phoneNumber) : this(firstName, lastName, email)
        {
            PhoneNumber = phoneNumber;
        }

        public Customer(int id,
                        string firstName,
                        string lastName,
                        string email,
                        string? phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Customer() { }
    }
}
