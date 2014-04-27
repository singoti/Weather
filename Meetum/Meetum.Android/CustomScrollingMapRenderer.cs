using System;
using Xamarin.Forms.Maps.Android;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Meetum.Android;
using AndroidView = Android.Views.View;
using Meetum.Controls;
using Android.Graphics;
using Android.Gms.Maps;

//[assembly: ExportRenderer (typeof (MyMap), typeof (CustomScrollingMapRenderer))]

namespace Meetum.Android
{
    public class CustomScrollingMapRenderer : MapRenderer, AndroidView.IOnTouchListener
    {
        public override bool DispatchTouchEvent (MotionEvent e)
        {
            return base.DispatchTouchEvent(e);
        }

        public override bool OnInterceptTouchEvent (MotionEvent e)
        {
            return base.OnInterceptTouchEvent(e);
        }

        public override bool OnTouchEvent (MotionEvent e)
        {
            return base.OnTouchEvent(e);
        }

        public bool OnTouch (AndroidView v, MotionEvent e)
        {
            return false;
        }
    }
}

