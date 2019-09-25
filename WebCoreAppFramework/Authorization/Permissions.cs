namespace WebCoreAppFramework.Authorization
{
    public static class Permissions
    {
        
        // this is the permitions class, each controler has a counterpart subclass here
        // each subclass has at least 4 standard permitions settings
        // this can be used to map Permitions to Users with the ApplicationRole/ApplicationRoleClaims list
        // On the Controlers decorate with [Authorize(Policy = Permissions.AdminUser.Create)]
        public static class AdminUser
        {
            public const string Create = "AdminUserCreate";
            public const string Read = "AdminUserRead";
            public const string Update = "AdminUserUpdate";
            public const string Delete = "AdminUserDelete";
        }

        public static class Home
        {
            public const string Create = "HomeCreate";
            public const string Read = "HomeRead";
            public const string Update = "HomeUpdate";
            public const string Delete = "HomeDelete";
        }
    }


    
}
