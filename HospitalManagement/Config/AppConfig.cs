namespace HospitalManagement.Config
{
    /// <summary>
    /// Application configuration constants
    /// </summary>
    public static class AppConfig
    {
        // Application Info
        public const string AppName = "Hospital Management System";
        public const string AppVersion = "1.0.0";

        // Pagination
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        // Session
        public const int SessionTimeoutMinutes = 30;

        // Date/Time Formats
        public const string DateFormat = "dd/MM/yyyy";
        public const string TimeFormat = "HH:mm";
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        // Appointment Settings
        public const int AppointmentDurationMinutes = 30;
        public const int MaxAppointmentsPerSlot = 5;

        // Working Hours
        public const int WorkStartHour = 8;
        public const int WorkEndHour = 17;
    }
}
