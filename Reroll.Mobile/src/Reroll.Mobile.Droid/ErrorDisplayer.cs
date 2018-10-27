namespace Reroll.Mobile.Droid
{
    #region Usings

    using System;
    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Support.Design.Widget;
    using Android.Views;
    using Android.Widget;
    using Core.Services.Interfaces;
    using Android.App;
    using Android.OS;
    using Core.Models.Enums;
    using MvvmCross;
    using MvvmCross.Platforms.Android;

    #endregion

    /// <summary>
    /// ErrorDisplayer
    /// </summary>
    public class ErrorDisplayer
    {
        #region Fields

        /// <summary>
        /// The application context
        /// </summary>
        private readonly Context _applicationContext;

        /// <summary>
        /// The current diaglog
        /// </summary>
        private AlertDialog _currentDiaglog;

        /// <summary>
        /// The current snackbar
        /// </summary>
        private Snackbar _currentSnackbar;

        #endregion

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDisplayer" /> class.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        public ErrorDisplayer(Context applicationContext)
        {
            this._applicationContext = applicationContext;

            var source = Mvx.Resolve<IErrorSource>();
            source.ErrorReported +=
                (sender, args) => this.ShowError(args.Message, args.ButtonText, args.ClickAction, args.Length);
            source.SuccessReported +=
                (sender, args) => this.ShowToast(args.Message);
        }

        #endregion

        #region  Other Members

        /// <summary>
        /// Shows the toast.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ShowToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        /// <param name="length">The length.</param>
        private void ShowError(string message, string buttonText, Action<View> action, ErrorLength length)
        {
            //Depending on API Level show different error notification
            int apiLevel;
            Int32.TryParse(Build.VERSION.Sdk, out apiLevel);
            if (apiLevel >= 23)
                this.ShowSnackbar(message, buttonText, action, length);
            else
                this.ShowDialog(message, buttonText, action);
        }

        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        private void ShowSnackbar(string message, string buttonText, Action<View> action, ErrorLength length)
        {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var parentLayout = activity.FindViewById(Android.Resource.Id.Content);
            var snackbarLength = length == ErrorLength.Infinite ? Snackbar.LengthIndefinite : Snackbar.LengthLong;
            var snackbar = Snackbar.Make(parentLayout, message, snackbarLength);
            if (snackbarLength == Snackbar.LengthIndefinite && action == null)
            {
                snackbar.SetActionTextColor(this._applicationContext.GetColor(Resource.Color.primary_text));
                snackbar.SetAction("Dismiss", (v) =>
                {
                    this._currentSnackbar.Dismiss();
                });
            }
            if (buttonText != null && action != null)
            {
                snackbar.SetActionTextColor(this._applicationContext.GetColor(Resource.Color.secondary_text));
                snackbar.SetAction(buttonText, action);
            }
            this._currentSnackbar = snackbar;
            snackbar.Show();
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <param name="action">The action.</param>
        private void ShowDialog(string message, string buttonText, Action<View> action)
        {
            if (this._currentDiaglog.IsShowing)
                this._currentDiaglog.Dismiss();
            //set alert for executing the task
            var currentActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var alert = new AlertDialog.Builder(currentActivity);
            alert.SetTitle("Alert");
            alert.SetMessage(message);
            if (buttonText != null && action != null)
            {
                alert.SetPositiveButton(buttonText, (senderAlert, args) => {
                    action.Invoke(new View(Application.Context));
                });
                alert.SetNegativeButton("Cancel", (senderAlert, args) => {

                });
            }
            else
            {
                alert.SetNeutralButton("OK", (senderAlert, args) => {

                });
            }

            this._currentDiaglog = alert.Create();
            this._currentDiaglog.Show();
        }

        #endregion
    }
}
