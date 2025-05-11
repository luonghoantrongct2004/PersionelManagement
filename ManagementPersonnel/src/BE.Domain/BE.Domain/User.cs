using Microsoft.AspNetCore.Identity;

namespace BE.Domain;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Avatar { get; set; }

    public User() { }

    public User(string fullName, string address, DateOnly dateOfBirth, string avatar)
    {
        FullName = fullName;
        Address = address;
        DateOfBirth = dateOfBirth;
        Avatar = avatar;
    }
}