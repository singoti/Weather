using System;
using MonoTouch.UIKit;
using Xamarin.QuickUI.Platform.iOS;
using Xamarin.QuickUI;

namespace Meetum.iOS
{
	public abstract class StoryBoardRenderer<T> : UIViewController, IViewRenderer where T : UIViewController
	{
		string storyboardName;

		string viewControllerStoryBoardId;

		public StoryBoardRenderer (string storyboard, string viewControllerStoryBoardId = "")
		{
			this.viewControllerStoryBoardId = viewControllerStoryBoardId;
			this.storyboardName = storyboard;
		}

		public abstract void SetModel (VisualElement model);

		public SizeRequest GetSizeRequest (double widthConstraint, double heightConstraint)
		{
			return NativeView.GetSizeRequest (widthConstraint, heightConstraint);
		}

		public void SetModelSize (Size size)
		{
			Model.Layout (new Rectangle (Model.X, Model.Y, size.Width, size.Height));
		}


		public abstract VisualElement Model { get; }

		public UIView NativeView {
			get { return StoryboardViewController.View; }
		}

		public UIViewController ViewController { get { return StoryboardViewController; } }

		T storyboardViewController;
		public T StoryboardViewController {
			get {
				return storyboardViewController ?? (storyboardViewController = CreateViewController());
			}
		}

		protected T CreateViewController()
		{
			var storyboard = UIStoryboard.FromName (storyboardName,null);
			return string.IsNullOrEmpty(viewControllerStoryBoardId) ? (T)storyboard.InstantiateInitialViewController () : (T) storyboard.InstantiateViewController(viewControllerStoryBoardId);
		}

	}
}

