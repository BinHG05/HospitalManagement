using HospitalManagement.Models.Entities;

namespace HospitalManagement.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticate user with username and password
        /// </summary>
        Users Login(string username, string password);

        /// <summary>
        /// Register a new user
        /// </summary>
        (bool Success, string Message) Register(string username, string password, string email, string phone, string fullName, string role);

        /// <summary>
        /// Check if username already exists
        /// </summary>
        bool UsernameExists(string username);

        /// <summary>
        /// Check if email already exists
        /// </summary>
        bool EmailExists(string email);
    }
}
