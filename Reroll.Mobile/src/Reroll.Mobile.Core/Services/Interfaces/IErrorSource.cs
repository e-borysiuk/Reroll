namespace Reroll.Mobile.Core.Services.Interfaces
{
    using System;
    using Models;
    /// <summary>
    /// Error handler
    /// </summary>
    public interface IErrorSource
    {
        /// <summary>
        /// Occurs when [error reported].
        /// </summary>
        event EventHandler<ErrorEventArgs> ErrorReported;

        /// <summary>
        /// Occurs when [error reported].
        /// </summary>
        event EventHandler<ErrorEventArgs> SuccessReported;
    }
}
