using HospitalManagement.Views.Forms;
using HospitalManagement.Views.Forms.Patient;
using HospitalManagement.Views.Forms.Doctor;
using HospitalManagement.Views.Forms.Admin;
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
