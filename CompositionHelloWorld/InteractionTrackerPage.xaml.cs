using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Composition.Interactions;
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
    public sealed partial class InteractionTrackerPage : Page
    {
        public InteractionTrackerPage()
        {
            this.InitializeComponent();
            Loaded += InteractionTrackerPage_Loaded;
        }

        private void InteractionTrackerPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetupInput();
            SetupAnimation();
        }

        private Compositor _compositor;
        private InteractionTracker _tracker;
        private VisualInteractionSource _source;
        private Visual _root;
        private CompositionPropertySet _props;
        private void SetupInput()
        {
            _root = ElementCompositionPreview.GetElementVisual(rootGrid);
            _compositor = _root.Compositor;
            _props = _compositor.CreatePropertySet();
            _tracker = InteractionTracker.Create(_compositor);
            _tracker.MaxPosition = new Vector3((float)rootGrid.ActualHeight);
            _source = VisualInteractionSource.Create(_root);
            _source.PositionYSourceMode = InteractionSourceMode.EnabledWithInertia;
            _tracker.InteractionSources.Add(_source);

        }
        private void SetupAnimation()
        {
            ExpressionAnimation progressAnimation = _compositor.CreateExpressionAnimation("tracker.Position.Y / tracker.MaxPosition.Y");
            progressAnimation.SetReferenceParameter("tracker", _tracker);
            _props.InsertScalar("progress", 0);
            _props.StartAnimation("progress", progressAnimation);

            ExpressionAnimation opacityAnimation = _compositor.CreateExpressionAnimation("lerp(0, 1, props.progress)");
            opacityAnimation.SetReferenceParameter("props", _props);
            ElementCompositionPreview.GetElementVisual(draggableRect).StartAnimation("Opacity", opacityAnimation);

            ExpressionAnimation scaleAnimation = _compositor.CreateExpressionAnimation("Vector3(1,1,1) * lerp(1, 2, props.progress)");
            scaleAnimation.SetReferenceParameter("props", _props);
            ElementCompositionPreview.GetElementVisual(draggableRect).StartAnimation("Scale", scaleAnimation);
        }

        private void rootGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
            {
                _source.TryRedirectForManipulation(e.GetCurrentPoint(rootGrid));
            }
            
        }
    }
}
