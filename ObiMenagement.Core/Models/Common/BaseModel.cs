using Microsoft.AspNetCore.Identity;

namespace ObiMenagement.Core.Models;

public class BaseModel
{
    public bool IsValid { get; set; }
    public DateTime CreatedOn { get; set; }
    public IdentityUser CreatedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
    public IdentityUser ModifiedBy { get; set; }
}

public class IdBaseModel:BaseModel
{
    public int Id { get; set; }
}
public class IdLongBaseModel:BaseModel
{
    public long Id { get; set; }
}
