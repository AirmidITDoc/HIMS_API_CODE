namespace HIMS.Core.Domain.Logging
{
    /// <summary>
    /// Represents a log type
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Audit
        /// </summary>
        Audit = 10,
        /// <summary>
        /// Error
        /// </summary>
        Error = 20,
        /// <summary>
        /// Warning
        /// </summary>
        Warning = 30,
        /// <summary>
        /// Information
        /// </summary>
        Information = 40
    }
}
