namespace UserManagement.Web;

public static class SystemLogEntry
{
    // Application Logs
    public const string ApplicationStarted = "Application Started";
    
    public const string LogViewerOpened = "Log Viewer Opened";
    
    // User Logs
    public const string UserListViewed = "User List Viewed (All Users)";
    public const string UserAccountViewed = "User Account Viewed";
    public const string UserAccountDeleted = "User Account Deleted";
    public const string UserAccountOpenedToEdit = "User Account Edit Screen Opened";
    public const string UserAccountEdited = "User Account Edited";
    public const string UserAccountCreated = "User Account Created";
    public const string ActiveUsersFilterApplied = "Active User Filter Applied";
    public const string NonActiveUsersFilterApplied = "Non-active User Filter Applied";
    public const string DeleteUserAccountOpened = "Delete User Account Screen Opened";
}