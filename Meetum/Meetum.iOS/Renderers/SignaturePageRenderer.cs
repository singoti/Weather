using System;
using Xamarin.QuickUI;
using Meetup.Shared.Pages;
using Meetum.iOS;


[assembly: ExportCell(typeof(CustomerDetailsPage), typeof(SignaturePageRenderer))]
namespace Meetum.iOS
{
	class SignaturePageRenderer : StoryBoardRenderer<SignatureViewController>
	{
		public SignaturePageRenderer () : base("SignatureStoryboard")
		{
		}

		#region implemented abstract members of StoryBoardRenderer

		VisualElement model;
		public override void SetModel (VisualElement model)
		{
			this.model = model;
		}

		public override VisualElement Model {
			get {
				return model;
			}
		}

		#endregion
	}
}

