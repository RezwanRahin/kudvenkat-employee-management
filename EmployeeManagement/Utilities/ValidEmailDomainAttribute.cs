using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Utilities;

public class ValidEmailDomainAttribute : ValidationAttribute
{
    private readonly string _allowedDomain;

    public ValidEmailDomainAttribute(string allowedDomain)
    {
        _allowedDomain = allowedDomain;
    }
}
