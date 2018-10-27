using MvvmCross;
using MvvmCross.Base;
using Reroll.Mobile.Core.Models.Enums;

namespace Reroll.Mobile.Core.Models
{
    using Android.Views;
    using Services.Interfaces;
    #region Usings
    using System;
    #endregion

    /// <summary>
    /// Error Application Object
    /// </summary>
    /// <seealso cref="MvxMainThreadDispatchingObject" />
    /// <seealso cref="IErrorReporter" />
    /// <seealso cref="IErrorSource" />
    public class ErrorApplicationObject
        : MvxMainThreadDispatchingObject
        , IErrorReporter
        , IErrorSource
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorApplicationObject"/> class.
        /// </summary>
        public ErrorApplicationObject()
        {

        }

        /// <summary>
        /// Reports the success.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ReportSuccess(string message)
        {
            this.InvokeOnMainThread(() =>
            {
                var handler = this.SuccessReported;
                if (handler != null)
                    handler(this, new ErrorEventArgs(message));
            });
        }

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        /// <param name="length">The length.</param>
        public void ReportError(string error, string buttonText, Action<View> action, ErrorLength length = ErrorLength.Finite)
        {
            if (this.ErrorReported == null)
                return;

            this.InvokeOnMainThread(() =>
            {
                this.ErrorReported?.Invoke(this, new ErrorEventArgs(error, buttonText, action, length));
            });
        }

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="length">The length.</param>
        public void ReportError(string error, ErrorLength length = ErrorLength.Finite)
        {
            if (this.ErrorReported == null)
                return;

            this.InvokeOnMainThread(() =>
            {
                this.ErrorReported?.Invoke(this, new ErrorEventArgs(error, length));
            });
        }

        /// <summary>
        /// Occurs when [error reported].
        /// </summary>
        public event EventHandler<ErrorEventArgs> ErrorReported;
        /// <summary>
        /// Occurs when [error reported].
        /// </summary>
        public event EventHandler<ErrorEventArgs> SuccessReported;
    }
}
