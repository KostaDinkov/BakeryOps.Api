using System.Reflection;

namespace BakeryOps.API.Security
{
    public static class SecurityUtils
    {
        public static HashSet<string> GetApiPermissions()
        {
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == "BakeryOps.API.Controllers"
                from m in t.GetMethods()
                where m.GetCustomAttributes(typeof(PermissionAttribute), false).Length > 0
                select new
                {

                    Permission = m.GetCustomAttribute(typeof(PermissionAttribute), false)
                };
            return [..q.Select(x => $"{((PermissionAttribute)x.Permission).Permission}")];
        }
    }
}
