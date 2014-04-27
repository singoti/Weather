using Android.Content;
using Android.Views;
using Android.Widget;
using System.ComponentModel;

using AScrollView = Android.Widget.HorizontalScrollView;
using ScrollView = Xamarin.Forms.ScrollView;
using AView = Android.Views.View;
using View = Xamarin.Forms.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Meetum.Controls.Android;

[assembly: ExportRenderer (typeof (Meetum.Controls.HorizontalScrollView), typeof (HorizontalScrollViewRenderer))]

namespace Meetum.Controls.Android
{
    class HorizontalScrollViewContainer : ViewGroup
    {
        View childView;
        ScrollView parent;
        IViewRenderer renderer;

        public HorizontalScrollViewContainer (IViewRenderer renderer, ScrollView parent, Context context)
            : base (context)
        {
            this.renderer = renderer;
            this.parent = parent;
        }

        public View ChildView
        {
            get { return childView; }
            set
            {
                if (childView == value)
                    return;

                RemoveAllViews ();

                childView = value;

                if (childView == null)
                    return;

                // TODO: Initialize your custom view logic here.
                // NOTE: Wrap in a viewgroup, or load from an XML layout.
                ViewGroup view = null;



                AddView (view);
            }
        }

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing) {
                if (ChildCount > 0)
                    GetChildAt (0).Dispose ();
                RemoveAllViews ();
                childView = null;
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            SetMeasuredDimension ((int)Context.ToPixels (childView.Width + parent.Padding.HorizontalThickness), (int)Context.ToPixels (childView.Height + parent.Padding.VerticalThickness));
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            if (childView == null)
                return;

            renderer.UpdateLayout ();
        }
    }

    public class HorizontalScrollViewRenderer : AScrollView, IViewRenderer
    {
        ScrollView view;
        ViewTracker tracker;
        HorizontalScrollViewContainer container;

        public ViewTracker Tracker
        {
            get { return this.tracker; }
        }

        public HorizontalScrollViewRenderer () : base (Forms.Context)
        {

        }

        public virtual void SetModel (VisualElement View)
        {
            view = View as ScrollView;

            tracker = new ViewTracker (this);

            view.PropertyChanged += HandlePropertyChanged;

            container = new HorizontalScrollViewContainer (this, view, Forms.Context);
            AddView (container);

            LoadContent ();
        }

        private void HandlePropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content") {
                LoadContent ();
            }
        }

        void LoadContent()
        {
            container.ChildView = view.Content;
        }

        #region IViewRenderer implementation
        public ViewGroup ViewGroup { get { return this; } }

        public VisualElement Model { get { return view; } }

        public Size MinimumSize ()
        {
            return new Size (40, 40);
        }

        public void UpdateLayout ()
        {
            if (tracker != null)
                tracker.UpdateLayout ();
        }
        #endregion

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing) {
                RemoveAllViews ();
                container.Dispose ();
                container = null;
            }
        }
    }

}
