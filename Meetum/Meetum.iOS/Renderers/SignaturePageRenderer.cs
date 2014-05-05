using System;
using Xamarin.QuickUI;

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

