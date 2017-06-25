using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class DropShadowPage : Page
    {
        public DropShadowPage()
        {
            this.InitializeComponent();
        }
        public void CreateShadow()
        {
            var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var shadow = compositor.CreateDropShadow();
            shadow.Mask = sample_Tbk.GetAlphaMask();
            var spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Shadow = shadow;
            spriteVisual.Size = new System.Numerics.Vector2((float)sample_Tbk.ActualWidth, (float)sample_Tbk.ActualHeight);
            ElementCompositionPreview.SetElementChildVisual(sample_Tbk, spriteVisual);
        }

        private void sample_Tbk_Loaded(object sender, RoutedEventArgs e)
        {
            CreateShadow();
        }
    }
}
