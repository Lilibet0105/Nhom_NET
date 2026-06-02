namespace QuanLyCongViec.DTO
{
    public static class UserSession
    {
        // Stores the currently logged-in username
        public static string Username { get; set; } = string.Empty;

        // Stores the role/permission of the current user (e.g., "Admin", "User")
        public static string Role { get; set; } = string.Empty;
    }
}
