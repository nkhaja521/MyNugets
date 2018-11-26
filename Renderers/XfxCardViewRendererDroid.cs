﻿using Android.Support.V7.Widget;
using Android.Views;
using GI.Common.XF.Android.Renderers;
using GI.Common.XF.Extensions;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AContext = Android.Content.Context;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(XfxCardView), typeof(XfxCardViewRendererDroid))]

namespace GI.Common.XF.Android.Renderers
{
    public class XfxCardViewRendererDroid : CardView, IVisualElementRenderer
    {
        private float _defaultElevation;
        private float _defaultCornerRadius;
        private XfxVisualElementManager _visualElementManager;

        public XfxCardViewRendererDroid(AContext context) : base(context)
        {
        }

        [Obsolete("ViewGroup is obsolete as of version 2.3.5. Please use View instead.")]
        public ViewGroup ViewGroup => this;
        public AView View => this;
        VisualElementTracker IVisualElementRenderer.Tracker => _visualElementManager.Tracker;
        public VisualElement Element { get; private set; }
        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;
        public void SetLabelFor(int? id) { }
        public void UpdateLayout() => _visualElementManager?.UpdateLayout();
        protected XfxCardView CardView => (XfxCardView)Element;
        protected virtual void OnElementChanged(object sender, VisualElementChangedEventArgs args) { }
        protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args) { }
        private void SetCardElevation() => Elevation = CardView.Elevation < 0 ? _defaultElevation : CardView.Elevation;
        private void SetCardRadius() => Radius = CardView.CornerRadius < 0 ? _defaultCornerRadius : CardView.CornerRadius;
        private void SetCardBackgroundColor() => SetCardBackgroundColor(CardView.BackgroundColor.ToAndroid());

        void IVisualElementRenderer.SetElement(VisualElement element)
        {
            var oldElement = Element;

            if (oldElement != null)
            {
                oldElement.PropertyChanged -= HandlePropertyChanged;
            }

            Element = element;
            if (Element != null)
            {
                Element.PropertyChanged += HandlePropertyChanged;
            }

            //sizes to match the forms view
            //updates properties, handles visual element properties
            _visualElementManager = new XfxVisualElementManager();
            _visualElementManager.Init(this);
            UseCompatPadding = true;

            _defaultElevation = Elevation;
            _defaultCornerRadius = Radius;
            SetContentPadding();
            SetCardRadius();
            SetCardBackgroundColor();
            SetCardElevation();
            RaiseElementChanged(new VisualElementChangedEventArgs(oldElement, Element));
        }



        public SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            View.Measure(widthConstraint, heightConstraint);
            return new SizeRequest(new Size(View.MeasuredWidth, View.MeasuredHeight));
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ContentView.ContentProperty.PropertyName)
            {
                ((IVisualElementRenderer)this).Tracker.UpdateLayout();
            }
            else if (e.PropertyName == Xamarin.Forms.Layout.PaddingProperty.PropertyName)
            {
                SetContentPadding();
            }
            else if (e.PropertyName == XfxCardView.CornerRadiusProperty.PropertyName)
            {
                SetCardRadius();
            }
            else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                SetCardBackgroundColor();
            }
            else if (e.PropertyName == XfxCardView.ElevationProperty.PropertyName)
            {
                SetCardElevation();
            }

            RaiseElementPropertyChanged(e);
        }

        private void SetContentPadding()
        {
            SetContentPadding((int)CardView.Padding.Left,
                    (int)CardView.Padding.Top,
                    (int)CardView.Padding.Right,
                    (int)CardView.Padding.Bottom);
        }

        private void RaiseElementChanged(VisualElementChangedEventArgs args)
        {
            ElementChanged?.Invoke(this, args);
            OnElementChanged(this, args);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return _visualElementManager.OnTouchEvent(e) || base.OnTouchEvent(e);
        }

        private void RaiseElementPropertyChanged(PropertyChangedEventArgs args)
        {
            ElementPropertyChanged?.Invoke(this, args);
            OnElementPropertyChanged(this, args);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _visualElementManager?.Dispose();
        }
    }
}