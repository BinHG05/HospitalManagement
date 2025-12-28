using HospitalManagement.Models.Entities;
using System;

namespace HospitalManagement.Session
{
    /// <summary>
    /// Manages user session data throughout the application
    /// </summary>
    public static class UserSession
    {
        public static Users CurrentUser { get; private set; }
        public static DateTime? LoginTime { get; private set; }

        public static void SetCurrentUser(Users user)
        {
            CurrentUser = user;
            LoginTime = DateTime.Now;
        }

        public static void ClearSession()
        {
            CurrentUser = null;
            LoginTime = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;

        public static string GetUserRole()
        {
            return CurrentUser?.Role ?? string.Empty;
        }

        public static int? GetUserId()
        {
            return CurrentUser?.UserID;
        }
    }
}
