using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Security;

public class SuperAdminHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
{
    
}
