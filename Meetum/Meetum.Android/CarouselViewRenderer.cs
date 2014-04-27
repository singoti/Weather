using System.ComponentModel;
using Android.Support.V4.View;
using Android.Views;
using Meetum.Controls;

namespace Xamarin.QuickUI.Platform.Android
{
    public class CarouselViewRenderer : BaseViewRenderer, ViewPager.IOnPageChangeListener
    {
        ViewPager viewPager;
        ViewTracker tracker;

        protected override void OnModelChanged (VisualElement oldModel, VisualElement newModel)
        {
            base.OnModelChanged (oldModel, newModel);

            if (this.viewPager != null) {
                RemoveView (this.viewPager);
                this.viewPager.SetOnPageChangeListener (null);
                this.viewPager.Dispose();
            }

            this.viewPager = new ViewPager (Context);
            this.viewPager.SetOnPageChangeListener (this);

            AddView (viewPager);

            if (this.tracker == null) {
                SetTracker (this.tracker = new ViewTracker (this));
            } else {
                this.tracker.SetModel (oldModel, newModel);
            }

            this.viewPager.OffscreenPageLimit = int.MaxValue;
        }

        protected CarouselView Carousel
        {
            get { return (CarouselView) Model; }
        }

        void UpdateCurrentView ()
        {
            var carouselView = (CarouselView) Model;
            //            carouselView.CurrentView = viewPager.CurrentItem > 0 && viewPager.CurrentItem < carouselView.LogicalChildren.Count ? carouselView.LogicalChildren[viewPager.CurrentItem] as View : null;
        }

        protected override void OnHandlePropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            base.OnHandlePropertyChanged (sender, e);

            if (e.PropertyName == "CurrentView")
                viewPager.CurrentItem =  CarouselView.GetIndex (Carousel);
        }

        protected override void OnLayout (bool changed, int l, int t, int r, int b)
        {
            base.OnLayout (changed, l, t, r, b);
            if (viewPager != null) {
                viewPager.Measure (MeasureSpec.MakeMeasureSpec (r - l, MeasureSpecMode.Exactly),
                    MeasureSpec.MakeMeasureSpec (b - t, MeasureSpecMode.Exactly));
                viewPager.Layout (0, 0, r - l, b - t);
            }
        }

        protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
        {
            viewPager.Measure (widthMeasureSpec, heightMeasureSpec);
            SetMeasuredDimension (viewPager.MeasuredWidth, viewPager.MeasuredHeight);
        }

        protected override void OnAttachedToWindow ()
        {
            base.OnAttachedToWindow ();
//            var adapter = new CarouselViewAdapter (Model as CarouselView, Context);
//            viewPager.Adapter = adapter;
//            ((Page) Model).SendAppearing ();
        }

        protected override void OnDetachedFromWindow ()
        {
            base.OnDetachedFromWindow ();
//            ((Page) Model).SendDisappearing ();
        }

        void ViewPager.IOnPageChangeListener.OnPageScrollStateChanged (int state)
        {
        }

        void ViewPager.IOnPageChangeListener.OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
        {
        }

        void ViewPager.IOnPageChangeListener.OnPageSelected (int position)
        {
            UpdateCurrentView();
        }
    }
}