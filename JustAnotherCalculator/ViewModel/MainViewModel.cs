using GalaSoft.MvvmLight;
using System;

namespace JustAnotherCalculator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                HelloWorld = "Yeap, MVVM works..";
            }
        }

        #region Property HelloWorld
        private String _helloWorld;
        public String HelloWorld
        {
            get
            {
                return _helloWorld;
            }
            set
            {
                _helloWorld = value;
                RaisePropertyChanged("HelloWorld");
            }
        }
        #endregion
    }
}