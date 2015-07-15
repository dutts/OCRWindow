using System;
using System.Windows;

namespace OCRWindow.Controls.AreaSelectionOverlay
{
    public static class DesiredWindowPosition
    {
        #region DesiredTop
        public static int GetDesiredTop(DependencyObject d)
        {
            return (int)d.GetValue(DesiredTopProperty);
        }

        public static void SetDesiredTop(DependencyObject d, int value)
        {
            d.SetValue(DesiredTopProperty, value);
        }

        public static readonly DependencyProperty DesiredTopProperty =
            DependencyProperty.RegisterAttached("DesiredTop", typeof(int), typeof(DesiredWindowPosition), new FrameworkPropertyMetadata(0, OnDesiredTopPropertyChanged));

        private static void OnDesiredTopPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window == null) return;

            double dblVal;
            if (Double.TryParse(e.NewValue.ToString(), out dblVal))
                window.Top = dblVal;
        }
        #endregion

        #region DesiredLeft
        public static int GetDesiredLeft(DependencyObject d)
        {
            return (int)d.GetValue(DesiredLeftProperty);
        }

        public static void SetDesiredLeft(DependencyObject d, int value)
        {
            d.SetValue(DesiredLeftProperty, value);
        }

        public static readonly DependencyProperty DesiredLeftProperty =
            DependencyProperty.RegisterAttached("DesiredLeft", typeof(int), typeof(DesiredWindowPosition), new FrameworkPropertyMetadata(0, OnDesiredLeftPropertyChanged));

        private static void OnDesiredLeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window == null) return;

            double dblVal;
            if (Double.TryParse(e.NewValue.ToString(), out dblVal))
                window.Left = dblVal;
        }
        #endregion

        #region DesiredWidth
        public static int GetDesiredWidth(DependencyObject d)
        {
            return (int)d.GetValue(DesiredWidthProperty);
        }

        public static void SetDesiredWidth(DependencyObject d, int value)
        {
            d.SetValue(DesiredWidthProperty, value);
        }

        public static readonly DependencyProperty DesiredWidthProperty =
            DependencyProperty.RegisterAttached("DesiredWidth", typeof(int), typeof(DesiredWindowPosition), new FrameworkPropertyMetadata(0, OnDesiredWidthPropertyChanged));

        private static void OnDesiredWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window == null) return;

            double dblVal;
            if (Double.TryParse(e.NewValue.ToString(), out dblVal))
                window.Width = dblVal;
        }
        #endregion

        #region DesiredHeight
        public static int GetDesiredHeight(DependencyObject d)
        {
            return (int)d.GetValue(DesiredHeightProperty);
        }

        public static void SetDesiredHeight(DependencyObject d, int value)
        {
            d.SetValue(DesiredHeightProperty, value);
        }

        public static readonly DependencyProperty DesiredHeightProperty =
            DependencyProperty.RegisterAttached("DesiredHeight", typeof(int), typeof(DesiredWindowPosition), new FrameworkPropertyMetadata(0, OnDesiredHeightPropertyChanged));

        private static void OnDesiredHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window == null) return;

            double dblVal;
            if (Double.TryParse(e.NewValue.ToString(), out dblVal))
                window.Height = dblVal;
        }
        #endregion

    }
}
