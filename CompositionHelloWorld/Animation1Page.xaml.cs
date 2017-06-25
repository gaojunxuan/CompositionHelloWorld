using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Composition;
using System.Numerics;
using Microsoft.Graphics.Canvas.Effects;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CompositionHelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Animation1Page : Page
    {
        Compositor _compositor;
        CompositionEffectBrush _effectBrush;
        public Animation1Page()
        {
            this.InitializeComponent();
        }
        public void CreateAnimation()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var blurEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = 5,
                BorderMode = EffectBorderMode.Hard,
                Source = new CompositionEffectSourceParameter("backdropBrush")
            };
            var animation = _compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, 0.0f);
            animation.InsertKeyFrame(1.0f, 100.0f);
            animation.Duration = TimeSpan.FromSeconds(4);

            var effectFactory = _compositor.CreateEffectFactory(blurEffect,new[] { "Blur.BlurAmount" });
            _effectBrush = effectFactory.CreateBrush();
            var backdropBrush = _compositor.CreateBackdropBrush();
            _effectBrush.SetSourceParameter("backdropBrush", backdropBrush);
            var spriteVisual = _compositor.CreateSpriteVisual();
            spriteVisual.Size = new System.Numerics.Vector2((float)Err_Image.ActualWidth, (float)Err_Image.ActualHeight);
            spriteVisual.Brush = _effectBrush;    
            ElementCompositionPreview.SetElementChildVisual(Err_Image, spriteVisual);
            spriteVisual.Brush.StartAnimation("Blur.BlurAmount", animation);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateAnimation();
        }
    }
}
