using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Movie
{
    internal class MainWindowViewModel: INotifyPropertyChanged
    {

    #region Declare Definition

    #endregion

    #region Declare Field
        
    #endregion

    #region Property

        public View1 View1 { get; private set; }
        public View2 View2 { get; private set; }


    #endregion

    #region Constructor && Destructor

        public MainWindowViewModel()
        {
            ViewModel vm1 = new ViewModel();
            ViewModel vm2 = vm1;

            this.View1 = new View1(vm1);
            this.View2 = new View2(vm2);
        }

    #endregion

    #region Method
  
    #endregion

    #region Interface : INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    }
}
