namespace Reroll.Mobile.Core.Services.Interfaces
{
    using Android.Views;
    using Models.Enums;
    using System;
    /// <summary>
    /// Error reporting service
    /// </summary>
    public interface IErrorReporter
    {
        /// <summary>
        /// Reports the success.
        /// </summary>
        /// <param name="message">The message.</param>
        void ReportSuccess(string message);

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="length">The length.</param>
        void ReportError(string error, ErrorLength length = ErrorLength.Finite);

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        /// <param name="length">The length.</param>
        void ReportError(string error, string buttonText, Action<View> action, ErrorLength length = ErrorLength.Finite);
    }
}
