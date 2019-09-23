namespace WebCoreAppFramework.Options
{
    public class AppSetupOptions: IAppSetupOptions
    {
        public string AdminRoleName { get; set; }
        public string ManagerRoleName { get; set; }
        public string NormalRoleName { get; set; }

        public string AdminUserName { get; set; }
        public string AdminUserPass { get; set; }

        public string DefaultTenantName { get; set; }
    }
}