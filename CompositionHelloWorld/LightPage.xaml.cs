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
using Windows.UI;
using Microsoft.Graphics.Canvas;

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
        DistantLight _distantLight;
        SpotLight _spotLight;
        public LightPage()
        {
            this.InitializeComponent();
        }
        public void CreateLight()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _pointLight = _compositor.CreatePointLight();
            _distantLight = _compositor.CreateDistantLight();
            _spotLight = _compositor.CreateSpotLight();
            _spotLight.InnerConeAngle = (float)(Math.PI / 4);
            _spotLight.OuterConeAngle = (float)(Math.PI / 3.5);
            //var amblighting = _compositor.CreateAmbientLight();  

            var lightRoot = ElementCompositionPreview.GetElementVisual(sample_img);

            //_distantLight.CoordinateSpace = lightRoot;
            //_distantLight.Targets.Add(lightRoot);
            //_spotLight.CoordinateSpace = lightRoot;
            //_spotLight.Targets.Add(lightRoot);

            //amblighting.Targets.Add(lightRoot);
            _pointLight.CoordinateSpace = lightRoot;
            _pointLight.Targets.Add(lightRoot);
        }
        private void sample_img_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if(_pointLight!=null)
            {
                var offset = e.GetCurrentPoint(sample_img).Position.ToVector2();
                //Vector3 position = new Vector3((float)sample_img.ActualWidth / 2, (float)sample_img.ActualHeight / 2, 200);
                //Vector3 lookAt = new Vector3((float)sample_img.ActualWidth - offset.X, (float)sample_img.ActualHeight - offset.Y, 0);
                _pointLight.Offset = new Vector3(offset.X, offset.Y, 200);
                //_distantLight.Direction= Vector3.Normalize(lookAt - position);
                //_spotLight.Offset = new Vector3(offset.X, offset.Y, 200);
            }
        }

        private void sample_img_Loaded(object sender, RoutedEventArgs e)
        {
            CreateLight();
        }
    }
}
