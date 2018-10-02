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
    public sealed partial class InteractionTrackerPage2 : Page
    {
        public InteractionTrackerPage2()
        {
            this.InitializeComponent();
            Loaded += InteractionTrackerPage2_Loaded;
        }

        private void InteractionTrackerPage2_Loaded(object sender, RoutedEventArgs e)
        {
            SetupInput();
            SetupAnimation();
            SetupRestingPoints();
        }

        private Compositor _compositor;
        private InteractionTracker _tracker;
        private VisualInteractionSource _source;
        private Visual _root;
        private CompositionPropertySet _props;
        private void SetupInput()
        {
            _root = ElementCompositionPreview.GetElementVisual(main);
            _compositor = _root.Compositor;
            _props = _compositor.CreatePropertySet();
            _tracker = InteractionTracker.Create(_compositor);
            _tracker.MaxPosition = new Vector3((float)main.ActualHeight);
            _source = VisualInteractionSource.Create(_root);
            _source.PositionYSourceMode = InteractionSourceMode.EnabledWithInertia;
            _tracker.InteractionSources.Add(_source);
        }
        private void SetupAnimation()
        {
            var scrollExp = _compositor.CreateExpressionAnimation("-tracker.Position.Y");
            scrollExp.SetReferenceParameter("tracker", _tracker);
            ElementCompositionPreview.GetElementVisual(popup).StartAnimation("Offset.Y", scrollExp);

            ExpressionAnimation progressAnimation = _compositor.CreateExpressionAnimation("tracker.Position.Y / tracker.MaxPosition.Y");
            progressAnimation.SetReferenceParameter("tracker", _tracker);
            _props.InsertScalar("progress", 0);
            _props.StartAnimation("progress", progressAnimation);

            ExpressionAnimation opacityAnimation = _compositor.CreateExpressionAnimation("lerp(0, 1, props.progress)");
            opacityAnimation.SetReferenceParameter("props", _props);
            ElementCompositionPreview.GetElementVisual(popup).StartAnimation("Opacity", opacityAnimation);

        }
        private void SetupRestingPoints()
        {
            var endpoint1 = InteractionTrackerInertiaRestingValue.Create(_compositor);
            endpoint1.Condition = _compositor.CreateExpressionAnimation("this.target.NaturalRestingPosition.y < (this.target.MaxPosition.y - this.target.MinPosition.y) /2");
            endpoint1.RestingValue = _compositor.CreateExpressionAnimation("this.target.MinPosition.y");

            var endpoint2 = InteractionTrackerInertiaRestingValue.Create(_compositor);
            endpoint2.Condition = _compositor.CreateExpressionAnimation("this.target.NaturalRestingPosition.y >= (this.target.MaxPosition.y - this.target.MinPosition.y) /2");
            endpoint2.RestingValue = _compositor.CreateExpressionAnimation("this.target.MaxPosition.y");

            _tracker.ConfigurePositionYInertiaModifiers(new InteractionTrackerInertiaModifier[] { endpoint1, endpoint2 });
        }
        float _direction = 1;
        private void root_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
            {
                _source.TryRedirectForManipulation(e.GetCurrentPoint(main));
                _direction = -_direction;
            }
            else
            {
                _tracker.TryUpdatePositionWithAdditionalVelocity(new Vector3(0f, 1000f * _direction, 0f));
                _direction = -_direction;
            }
        }
    }
}
