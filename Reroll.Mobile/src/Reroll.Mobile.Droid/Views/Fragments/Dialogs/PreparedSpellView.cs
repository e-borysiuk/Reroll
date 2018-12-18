using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Newtonsoft.Json;
using Reroll.Mobile.Core.Models;
using Reroll.Mobile.Core.ViewModels.Dialogs;

namespace Reroll.Mobile.Droid.Views.Fragments.Dialogs
{
    public class PreparedSpellView : MvxDialogFragment<PreparedSpellViewModel>
    {
        IList<RootObject> spells;
        View view;
        public override Dialog OnCreateDialog(Bundle savedState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(Resource.Layout.prepared_spell_dialog, null);
            var numPicker = view.FindViewById<NumberPicker>(Resource.Id.numberPicker);
            if (ViewModel.CastQuantity != 0)
                numPicker.Value = ViewModel.CastQuantity;
            numPicker.ValueChanged += NumPicker_ValueChanged;

            AssetManager assets = this.Context.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("spells.json")))
            {
                var content = sr.ReadToEnd();
                spells = JsonConvert.DeserializeObject<IList<RootObject>>(content);
            }

            var spellNames = spells.Select(x => x.name).ToList();

            AutoCompleteTextView textView = view.FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_spell);
            var adapter = new ArrayAdapter<String>(Context, Resource.Layout.autocomplete_row, spellNames);
            textView.ItemClick += TextView_ItemClick;
            textView.Adapter = adapter;

            var dialog = new AlertDialog.Builder(Activity);
            dialog.SetTitle("Prepared spell Dialog");
            dialog.SetView(view);
            if (ViewModel.IsEditMode)
                dialog.SetNeutralButton("Delete",
                    (s, a) => { ViewModel.DeleteCommand.Execute(); });
            dialog.SetNegativeButton("Cancel", (s, a) => { });
            dialog.SetPositiveButton("OK", (s, a) =>
                ViewModel.SaveCommand.Execute()
            );
            return dialog.Create();
        }

        private void NumPicker_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            ViewModel.SetCastQuantityValue(e.NewVal);
        }

        private void TextView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AutoCompleteTextView autoText = (AutoCompleteTextView)sender;
            var name = autoText.Text;
            var spell = spells.FirstOrDefault(s => s.name == name);
            this.ViewModel.SpellName = name;
        }
    }
}
