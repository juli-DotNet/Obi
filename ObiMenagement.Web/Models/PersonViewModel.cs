namespace ObiMenagement.Web.Models;

public class PersonViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public DateTime PassportExpiringDate { get; set; }
    public DateTime DrivingLicenceExpiringDate { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
