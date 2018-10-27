namespace Reroll.Mobile.Core.Models
{
    using Android.Views;
    using Enums;
    using System;
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the button text.
        /// </summary>
        /// <value>
        /// The button text.
        /// </value>
        public string ButtonText { get; private set; }

        /// <summary>
        /// Gets the click action.
        /// </summary>
        /// <value>
        /// The click action.
        /// </value>
        public Action<View> ClickAction { get; private set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public ErrorLength Length { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        /// <param name="length">The length.</param>
        public ErrorEventArgs(string message, string buttonText, Action<View> action, ErrorLength length = ErrorLength.Finite)
        {
            this.Message = message;
            this.ClickAction = action;
            this.ButtonText = buttonText;
            this.Length = length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="length">The length.</param>
        public ErrorEventArgs(string message, ErrorLength length = ErrorLength.Finite)
        {
            this.Message = message;
            this.Length = length;
        }
    }
}
