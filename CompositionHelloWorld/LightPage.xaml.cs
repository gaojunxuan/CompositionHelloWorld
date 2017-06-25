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
using Windows.UI.Composition;
using Windows.UI.Xaml.Hosting;
using Windows.Graphics.Effects;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI.Composition.Effects;
using System.Numerics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CompositionHelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LightPage : Page
    {
        Compositor _compositor;
        CompositionEffectFactory _factory;
        PointLight _pointLight;
        public LightPage()
        {
            this.InitializeComponent();
        }
        public void CreateLight()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _pointLight = _compositor.CreatePointLight();
            //IGraphicsEffect lightEffect = new SceneLightingEffect()
            //{
            //    AmbientAmount = 0,
            //    DiffuseAmount = .75f,
            //    SpecularAmount = 0
                
            //};
            //_factory = _compositor.CreateEffectFactory(lightEffect);
            var lightRoot = ElementCompositionPreview.GetElementVisual(sample_img);
            _pointLight.CoordinateSpace = lightRoot;
            _pointLight.Targets.Add(lightRoot);
        }
        private void sample_img_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if(_pointLight!=null)
            {
                var offset = e.GetCurrentPoint(sample_img).Position.ToVector2();
                _pointLight.Offset = new Vector3(offset.X, offset.Y, 75);
            }
        }

        private void sample_img_Loaded(object sender, RoutedEventArgs e)
        {
            CreateLight();
        }
    }
}
