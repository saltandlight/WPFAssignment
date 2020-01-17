using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    class WindowViewModel: INotifyPropertyChanged
    {

    #region Declare Definition

    #endregion

    #region Declare Field

    #endregion

    #region Property

        public View1 View1 { get; set; }
        public View2 View2 { get; set; }
        public View3 View3 { get; set; }
        public View4 View4 { get; set; }

    #endregion

    #region Constructor && Destructor

        public WindowViewModel()
        {
            ViewModel vm1 = new ViewModel(1);
            ViewModel vm2 = new ViewModel(2);
            ViewModel vm3 = new ViewModel(3);
            ViewModel vm4 = new ViewModel(4);

            this.View1 = new View1(vm1);
            this.View2 = new View2(vm2);
            this.View3 = new View3(vm3);
            this.View4 = new View4(vm4);
        }

    #endregion

    #region Method

    #endregion

    #region Interface : INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    }
}
