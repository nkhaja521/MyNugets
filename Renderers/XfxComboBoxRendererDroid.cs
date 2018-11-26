using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Widget;
using GI.Common.XF.Android.Renderers;
using GI.Common.XF.Android.XfxComboBox;
using GI.Common.XF.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Res = Android.Resource;

[assembly: ExportRenderer(typeof(XfxComboBox), typeof(XfxComboBoxRendererDroid))]

namespace GI.Common.XF.Android.Renderers
{
    public class XfxComboBoxRendererDroid : XfxEntryRendererDroid
    {
        public XfxComboBoxRendererDroid(Context context) : base(context)
        {
        }

        private AppCompatAutoCompleteTextView AutoComplete => (AppCompatAutoCompleteTextView)Control.EditText;

        protected override TextInputLayout CreateNativeControl()
        {
            var textInputLayout = new TextInputLayout(Context);
            var autoComplete = new AppCompatAutoCompleteTextView(Context)
            {
                BackgroundTintList = ColorStateList.ValueOf(GetPlaceholderColor())
            };
            textInputLayout.AddView(autoComplete);
            return textInputLayout;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<XfxEntry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                // unsubscribe
                AutoComplete.ItemClick -= AutoCompleteOnItemSelected;
                var elm = (XF.Extensions.XfxComboBox) e.OldElement;
                elm.CollectionChanged -= ItemsSourceCollectionChanged;
            }

            if (e.NewElement != null)
            {
                // subscribe
                SetItemsSource();
                SetThreshold();
                KillPassword();
                AutoComplete.ItemClick += AutoCompleteOnItemSelected;
                var elm = (XF.Extensions.XfxComboBox) e.NewElement;
                elm.CollectionChanged += ItemsSourceCollectionChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Entry.IsPasswordProperty.PropertyName)
                KillPassword();
            if (e.PropertyName == XF.Extensions.XfxComboBox.ItemsSourceProperty.PropertyName)
                SetItemsSource();
            else if (e.PropertyName == XF.Extensions.XfxComboBox.ThresholdProperty.PropertyName)
                SetThreshold();
        }

        private void AutoCompleteOnItemSelected(object sender, AdapterView.ItemClickEventArgs args)
        {
            var view = (AutoCompleteTextView) sender;
            var selectedItemArgs = new SelectedItemChangedEventArgs(view.Text);
            var element = (XF.Extensions.XfxComboBox) Element;
            element.OnItemSelectedInternal(Element, selectedItemArgs);
            HideKeyboard();
        }

        private void ItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var element = (XF.Extensions.XfxComboBox) Element;
            ResetAdapter(element);
        }

        private void KillPassword()
        {
            if (Element.IsPassword)
                throw new NotImplementedException("Cannot set IsPassword on a XfxComboBox");
        }

        private void ResetAdapter(XF.Extensions.XfxComboBox element)
        {
            var adapter = new XfxComboBoxArrayAdapter(Context,
                Res.Layout.SimpleDropDownItem1Line,
                element.ItemsSource.ToList(),
                element.SortingAlgorithm);
            AutoComplete.Adapter = adapter;
            adapter.NotifyDataSetChanged();
        }

        private void SetItemsSource()
        {
            var element = (XF.Extensions.XfxComboBox) Element;
            if (element.ItemsSource == null) return;

            ResetAdapter(element);
        }

        private void SetThreshold()
        {
            var element = (XF.Extensions.XfxComboBox) Element;
            AutoComplete.Threshold = element.Threshold;
        }
    }
}