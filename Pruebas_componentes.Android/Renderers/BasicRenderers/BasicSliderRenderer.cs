using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using OneyApp.Controls.BasicControls;
using OneyApp.Droid.Renderers.BasicRenderers;
using XamarinForms = Xamarin.Forms;

[assembly: ExportRenderer(typeof(BasicSlider), typeof(BasicSliderRenderer))]
namespace OneyApp.Droid.Renderers.BasicRenderers
{
    public class BasicSliderRenderer : SliderRenderer
    {
        public BasicSliderRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var slider = (Slider)e.NewElement;

                Control.SplitTrack = false;

                var sliderThickness = 14;//(int)Application.Current.Resources["SliderThickness"];


                SetThumb(sliderThickness, slider.ThumbColor);

                var layeredDrawableReference = (LayerDrawable)Control.ProgressDrawable;

                SetProgressBackground(sliderThickness, layeredDrawableReference, slider.MinimumTrackColor);

                SetBackground(sliderThickness, layeredDrawableReference, slider.MaximumTrackColor);

            }
        }

        private void SetThumb(int sliderThickness, XamarinForms.Color color)
        {
            var outsideShape = new ShapeDrawable(new OvalShape());
            outsideShape.SetIntrinsicWidth(sliderThickness * 3);
            outsideShape.SetIntrinsicHeight(sliderThickness * 3);
            outsideShape.Paint.StrokeWidth = sliderThickness * 2;
            outsideShape.Paint.SetStyle(Paint.Style.Stroke);
            outsideShape.Paint.Color = color.ToAndroid();

            var insideShape = new ShapeDrawable(new OvalShape());
            insideShape.SetIntrinsicWidth(sliderThickness / 2);
            insideShape.SetIntrinsicHeight(sliderThickness / 2);
            insideShape.Paint.SetStyle(Paint.Style.Fill);
            insideShape.Paint.Color = Android.Graphics.Color.White;

            var finalDrawable = new LayerDrawable(new Drawable[] { outsideShape, insideShape });


            Control.SetThumb(finalDrawable);
        }

        private static void SetBackground(int sliderThickness, LayerDrawable layeredDrawableReference, XamarinForms.Color color) 
        {
            var background = new PaintDrawable(color.ToAndroid());
            background.SetCornerRadius(sliderThickness / 2);
            background.SetIntrinsicHeight(sliderThickness * 2);

            layeredDrawableReference.SetDrawableByLayerId(Android.Resource.Id.Background, background);
        }

        private static void SetProgressBackground(int sliderThickness, LayerDrawable layeredDrawableReference, XamarinForms.Color color)
        {
            var progress = new PaintDrawable(color.ToAndroid());
            progress.SetCornerRadius(10);
            progress.SetIntrinsicHeight(sliderThickness * 2);
            var progressClip = new ClipDrawable(progress, GravityFlags.Left, ClipDrawableOrientation.Horizontal);

            layeredDrawableReference.SetDrawableByLayerId(Android.Resource.Id.Progress, progressClip);
        }
    }
}