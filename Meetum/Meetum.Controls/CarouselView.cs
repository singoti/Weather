using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Meetum.Controls
{
    public class CarouselView : View
    {
        public static readonly BindableProperty IndexProperty = BindableProperty.Create ("Index", typeof(int), typeof(Page), -1, BindingMode.OneWay, null, null, null, null);

        public static readonly BindableProperty FooProperty = 
            BindableProperty.Create(
                propertyName: "Foo", 
                returnType: typeof (string), 
                declaringType: typeof(CarouselView), 
                defaultValue: String.Empty, 
                defaultBindingMode: BindingMode.OneWay, 
                validateValue: (bindable, val) => !val.Equals("foo"), 
                propertyChanged: null, 
                propertyChanging: null, 
                coerceValue: null
            );

        public event EventHandler<CarouselEventArgs> ViewChanged;

        public int  CurrentView { get; set; }
        public IEnumerable<View> Views { get; set; }
        public bool WrapAround { get; set; }

        public static int GetIndex (CarouselView page)
        {
            if (page == null)
            {
                throw new ArgumentNullException ("page");
            }
            return (int)page.GetValue (CarouselView.IndexProperty);
        }

        public void MoveNext() { }
        public void MovePrevious() { }
        public void MoveFirst() { }
        public void MoveLast() { }
    }
}

