using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reroll.Mobile.Core.ViewModels
{
    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
    {
        protected bool IsEditMode;
        public abstract void Prepare(TParameter parameter);
    }
}
