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
using Microsoft.Graphics;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CompositionHelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Animation2Page : Page
    {
        Compositor _compositor;
        public Animation2Page()
        {
            this.InitializeComponent();
        }
        public void CreateAnimation()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var visual1 = _compositor.CreateSpriteVisual();
            var visual2 = _compositor.CreateSpriteVisual();
            visual1.Brush = _compositor.CreateColorBrush(Colors.Red);
            visual1.Size = new Vector2(80, 80);
            visual2.Brush = _compositor.CreateColorBrush(Colors.Blue);
            visual2.Size = new Vector2(80, 80);
            var animation1 = _compositor.CreateScalarKeyFrameAnimation();
            animation1.InsertKeyFrame(0.0f, 0);
            animation1.InsertKeyFrame(1.0f, 360f);
            animation1.Duration = TimeSpan.FromSeconds(5);
            animation1.IterationBehavior = AnimationIterationBehavior.Forever;
            var animation2 = _compositor.CreateExpressionAnimation("-visual1.RotationAngleInDegrees");
            animation2.SetReferenceParameter("visual1", visual1);
            ElementCompositionPreview.SetElementChildVisual(canvas1, visual1);
            ElementCompositionPreview.SetElementChildVisual(canvas2, visual2);
            visual1.StartAnimation("RotationAngleInDegrees", animation1);  
            visual2.StartAnimation("RotationAngleInDegrees", animation2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateAnimation();
        }
    }
}
