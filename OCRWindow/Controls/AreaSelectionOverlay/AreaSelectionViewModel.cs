using System;
using Microsoft.Practices.Prism.Mvvm;
using OCRWindow.Interfaces;

namespace OCRWindow.Controls.AreaSelectionOverlay
{
    public class AreaSelectionViewModel : BindableBase, IRequestCloseViewModel
    {
        public event EventHandler RequestClose;

        private int _windowPosLeft;
        public int WindowPosLeft
        {
            get { return _windowPosLeft; }
            set { SetProperty(ref _windowPosLeft, value); }
        }

        private int _windowPosTop;
        public int WindowPosTop
        {
            get { return _windowPosTop; }
            set { SetProperty(ref _windowPosTop, value); }
        }

        private int _windowWidth;
        public int WindowWidth
        {
            get { return _windowWidth; }
            set { SetProperty(ref _windowWidth, value); }
        }

        private int _windowHeight;
        public int WindowHeight
        {
            get { return _windowHeight; }
            set { SetProperty(ref _windowHeight, value); }
        }
    }
}