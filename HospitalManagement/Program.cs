using HospitalManagement.Views.Forms;
using HospitalManagement.Views.Forms.Patient;
using HospitalManagement.Views.Forms.Doctor;
using HospitalManagement.Views.Forms.Admin;
using HospitalManagement.Views.Forms.Pharmacist;
using System;
using System.Windows.Forms;

namespace HospitalManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize database connection
            // NOTE: Set to true ONLY when you need to reset database completely
            HospitalManagement.Infrastructure.DatabaseInitializer.Initialize(false);

            // Login loop
            while (true)
            {
                var loginForm = new LoginForm();
                var loginResult = loginForm.ShowDialog();

                if (loginResult == DialogResult.OK && loginForm.LoggedInUser != null)
                {
                    Form dashboard = null;
                    var user = loginForm.LoggedInUser;

                    // Route to role-specific dashboard
                    switch (user.Role?.ToLower())
                    {
                        case "patient":
                            dashboard = new PatientDashboard(user);
                            break;
                        case "doctor":
                            dashboard = new DoctorDashboard(user);
                            break;
                        case "admin":
                            dashboard = new AdminDashboard(user);
                            break;
                        case "pharmacist":
                            dashboard = new PharmacistDashboard(user);
                            break;
                        default:
                            // Default to patient dashboard
                            dashboard = new PatientDashboard(user);
                            break;
                    }

                    var dashResult = dashboard.ShowDialog();

                    if (dashResult != DialogResult.Retry)
                    {
                        // User closed the app
                        break;
                    }
                    // If Retry, loop back to login
                }
                else
                {
                    // User cancelled login
                    break;
                }
            }
        }
    }
}
