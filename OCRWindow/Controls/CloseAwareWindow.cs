using System.Windows;
using OCRWindow.Interfaces;

namespace OCRWindow.Controls
{
    public class CloseAwareWindow : Window
    {
        public CloseAwareWindow()
        {
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is IRequestCloseViewModel)
            {
                ((IRequestCloseViewModel)args.NewValue).RequestClose += (s, e) => this.Close();
            }
        }
    }
}
