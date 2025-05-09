using System;
using System.Reflection;
using CoreGraphics;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using UXDivers.Grial;

namespace Ubi.iOS
{
	/// <summary>
	/// This renderer fixes an issue with the carousel page and iOS 11 where the carousel page 
	/// has vertical scrolling besides the horizontal expected horizontal swipe.
	/// This render patches that behavior to prevent the vertical scroll.
	/// </summary>
	public class CarouselPageVerticalScrollFixRenderer : CarouselPageRenderer
	{
		private readonly FieldInfo _scrollField;
		private UIScrollView _scrollView;

		public CarouselPageVerticalScrollFixRenderer()
		{
			_scrollField = typeof(CarouselPageRenderer)
				.GetField("_scrollView", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_scrollView = _scrollField.GetValue(this) as UIScrollView;
			if (_scrollView != null)
			{
				_scrollView.ShowsVerticalScrollIndicator = false;
				_scrollView.Scrolled -= OnScrollViewScrolled;
				_scrollView.Scrolled += OnScrollViewScrolled;
			}
		}

		public override void ViewDidUnload()
		{
			base.ViewDidUnload();

			if (_scrollView != null)
			{
				_scrollView.Scrolled -= OnScrollViewScrolled;
			}
		}

		private void OnScrollViewScrolled(object sender, EventArgs e)
		{
			if (_scrollView.ContentOffset.Y != 0)
			{
				_scrollView.SetContentOffset(new CGPoint(_scrollView.ContentOffset.X, 0), false);
			}
		}
	}
}
