using System.Reflection;

namespace EmailNotificationService;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
