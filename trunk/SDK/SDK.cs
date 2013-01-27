using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

[assembly: CLSCompliant(true)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true)]
[assembly: PermissionSet(SecurityAction.RequestOptional, Name = "Nothing")]
namespace LanExchange.Sdk
{
}
