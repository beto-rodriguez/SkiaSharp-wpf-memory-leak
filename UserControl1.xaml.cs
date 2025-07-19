using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private bool _isDisposed;

        public UserControl1()
        {
            InitializeComponent();

            Unloaded += UserControl1_Unloaded;
        }

        private void UserControl1_Unloaded(object sender, RoutedEventArgs e) => Teardown();

        private void SKGLElement_PaintSurface(object? sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            
            using var paint = new SkiaSharp.SKPaint
            {
                Color = SkiaSharp.SKColors.Red,
                IsAntialias = true,
                TextSize = 24
            };

            canvas.DrawText("hi there", new(100, 100), paint);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            // If we just got removed from the tree, clean up
            if (oldParent != null && VisualParent == null)
                Teardown();
        }

        private void Teardown()
        {
            if (_isDisposed) return;

            skiaControl.PaintSurface -= SKGLElement_PaintSurface;
            skiaControl.Dispose();

            _isDisposed = true;
        }
    }
}
