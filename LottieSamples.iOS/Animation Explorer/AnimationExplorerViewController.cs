using System;
using System.Threading.Tasks;
using Airbnb.Lottie;
using CoreGraphics;
using UIKit;

namespace LottieSamples.iOS
{
    public partial class AnimationExplorerViewController : UIViewController
    {
        //private ViewBackgroundColor currentBGColor;
        private UIToolbar toolbar;
        private UISlider slider;
        private LAAnimationView laAnimation;


        public AnimationExplorerViewController() : base()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.toolbar = new UIToolbar(CGRect.Empty);


            this.slider = new UISlider(CGRect.Empty);
            this.slider.MinValue = 0f;
            this.slider.MaxValue = 1f;
            this.View.AddSubview(this.slider);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            CGRect b = this.View.Bounds;
            this.toolbar.Frame = new CGRect(0, b.Size.Height - 44, b.Size.Width, 44);
            CGSize sliderSize = this.slider.SizeThatFits(b.Size);

            sliderSize.Height += 12;

            this.slider.Frame = new CGRect(10, this.toolbar.Frame.GetMinY() - sliderSize.Height,
                                           b.Size.Width - 20, sliderSize.Height);
            this.laAnimation.Frame = new CGRect(0, 0, b.Size.Width, this.slider.Frame.GetMinY());
        }


        private void ResetAllButtons()
        {
            this.slider.Value = 0f;
            foreach (UIBarButtonItem button in this.toolbar.Items)
            {
                ResetButton(button, highlighted: false);
            }
        }

        private void ResetButton(UIBarButtonItem button, bool highlighted)
        {
            button.TintColor = highlighted ? UIColor.Red : new UIColor(red: 50f / 255f,
                                                                       green: 207f / 255f,
                                                                       blue: 193f / 255f,
                                                                       alpha:1f);
        }


        private async Task ShowMessageAsync(string message)
        {
            UILabel messageLabel = new UILabel(CGRect.Empty);
            messageLabel.TextAlignment = UITextAlignment.Center;
            messageLabel.BackgroundColor = UIColor.Black.ColorWithAlpha(0.3f);
            messageLabel.TextColor = UIColor.White;
            messageLabel.Text = message;

            CGSize messageSize = messageLabel.SizeThatFits(this.View.Bounds.Size);
            messageSize.Width += 14;
            messageSize.Height += 14;
            messageLabel.Frame = new CGRect(10, 30, messageSize.Width, messageSize.Height);
            messageLabel.Alpha = 0f;
            this.View.AddSubview(messageLabel);

            await UIView.AnimateAsync(0.3f, () => messageLabel.Alpha = 1f);
            UIView.Animate(0.3f, 1f, UIViewAnimationOptions.CurveEaseInOut, 
                           ()=> messageLabel.Alpha=0f, 
                           () => messageLabel.RemoveFromSuperview());
        }

    }
}

