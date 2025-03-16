namespace AuthAPI.Domain.Enums
{
    /// <summary>
    /// Defines the standard roles available in the application
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Administrator role with full system access
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// Standard user role with basic access
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// Manager role with elevated access
        /// </summary>
        public const string Manager = "Manager";
    }
}