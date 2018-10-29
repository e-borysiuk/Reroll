using System;
using Android.Views;
using MvvmCross;
using Reroll.Mobile.Core.Interfaces;
using Reroll.Mobile.Core.Models.Enums;

namespace Reroll.Mobile.Core.Services
{
    public class ErrorReportingService : IErrorReportingService
    {
        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        /// <param name="length">The length.</param>
        public static void ReportError(string error, string buttonText, Action<View> action, ErrorLength length = ErrorLength.Finite)
        {
            Mvx.Resolve<IErrorReporter>().ReportError(error, buttonText, action, length);
        }

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="length">The length.</param>
        public static void ReportError(string error, ErrorLength length = ErrorLength.Finite)
        {
            Mvx.Resolve<IErrorReporter>().ReportError(error, length);
        }

        /// <summary>
        /// Reports the success.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void ReportSuccess(string message)
        {
            Mvx.Resolve<IErrorReporter>().ReportSuccess(message);
        }
    }
}
