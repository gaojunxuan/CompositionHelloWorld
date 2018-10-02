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
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI;
using System.Numerics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompositionHelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }
        CompositionEffectBrush _effectBrush;
        Compositor comp;
        public void Do()
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(Err_Image);
            comp = ElementCompositionPreview.GetElementVisual(this).Compositor;
            var blurEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = (float)blur_Slider.Value,
                BorderMode = EffectBorderMode.Hard,
                Source = new CompositionEffectSourceParameter("backdropBrush")
                //Source = new ArithmeticCompositeEffect
                //{
                //    MultiplyAmount = 0,
                //    Source1Amount = 0.5f,
                //    Source2Amount = 0.5f,
                //    Source1 = new CompositionEffectSourceParameter("backdropBrush"),
                //    Source2 = new ColorSourceEffect
                //    {
                //        Color = Color.FromArgb(255,255,255,255)
                //    }
                //}
            };
            var effectFactory = comp.CreateEffectFactory(blurEffect,new[] { "Blur.BlurAmount" });
            _effectBrush = effectFactory.CreateBrush();
            var backdropBrush = comp.CreateBackdropBrush();
            _effectBrush.SetSourceParameter("backdropBrush", backdropBrush);
            var blurVisual = comp.CreateSpriteVisual();
            blurVisual.Size = new System.Numerics.Vector2((float)Err_Image.ActualWidth, (float)Err_Image.ActualHeight);
            blurVisual.Brush = _effectBrush;
            ElementCompositionPreview.SetElementChildVisual(Err_Image, blurVisual);
            
        }

        private void blur_Btn_Click(object sender, RoutedEventArgs e)
        {
            Do();
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(_effectBrush!=null)
            _effectBrush.Properties.InsertScalar("Blur.BlurAmount", (float)e.NewValue);
        }

        private void nav1_Btn_Click(object sender, RoutedEventArgs e)
        {
            //nav to TransparentWindowPage
            Frame.Navigate(typeof(TransparentWindowPage));
        }

        private void nav2_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DropShadowPage));
        }

        private void nav3_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LightPage));
        }

        private void nav4_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Animation1Page));
        }

        private void Err_Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SpriteVisual blurVisual = (SpriteVisual)ElementCompositionPreview.GetElementChildVisual(Err_Image);
            if (blurVisual != null)
            {
                blurVisual.Size = e.NewSize.ToVector2();
            }
        }

        private void nav5_Btn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InteractionTrackerPage2));
        }
    }
}
