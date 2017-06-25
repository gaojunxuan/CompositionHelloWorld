using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CompositionHelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TransparentWindowPage : Page
    {
        Compositor _compositor;
        SpriteVisual _spriteVisual;
        public TransparentWindowPage()
        {
            this.InitializeComponent();
        }
        public void UpdateEffect()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var blurEffect = new GaussianBlurEffect()
            {
                Name = "Blur",
                BlurAmount = 100.0f,
                BorderMode=EffectBorderMode.Hard,
                Source=new CompositionEffectSourceParameter("source"),
                
            };
            var fac=_compositor.CreateEffectFactory(blurEffect);
            var brush = fac.CreateBrush();
            var backdropbrush = _compositor.CreateHostBackdropBrush();
            brush.SetSourceParameter("source", backdropbrush);
            _spriteVisual = _compositor.CreateSpriteVisual();
            _spriteVisual.Brush = brush;
            _spriteVisual.Size = new System.Numerics.Vector2((float)backgroundGrid.ActualWidth, (float)backgroundGrid.ActualHeight);
            ElementCompositionPreview.SetElementChildVisual(backgroundGrid, _spriteVisual);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateEffect();
        }

        private void backgroundGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(_spriteVisual!=null)
            {
                _spriteVisual.Size = e.NewSize.ToVector2();
            }
        }
    }
}
