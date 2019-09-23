namespace WebCoreAppFramework.Options
{
    public interface IAppSetupOptions
    {
        string AdminRoleName { get; set; }
        string ManagerRoleName { get; set; }
        string NormalRoleName { get; set; }

        string AdminUserName { get; set; }
        string AdminUserPass { get; set; }
        string DefaultTenantName { get; set; }
        

    }
}