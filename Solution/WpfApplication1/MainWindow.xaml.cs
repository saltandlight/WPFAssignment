using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public View1 View1 { get; set; }
        public View2 View2 { get; set; }
        public View3 View3 { get; set; }
        public View4 View4 { get; set; }

        public MainWindow()
        {
            ViewModel vm1 = new ViewModel(1);
            ViewModel vm2 = new ViewModel(2);
            ViewModel vm3 = new ViewModel(3);
            ViewModel vm4 = new ViewModel(4);

            this.View1 = new View1(vm1);
            this.View2 = new View2(vm2);
            this.View3 = new View3(vm3);
            this.View4 = new View4(vm4);

            InitializeComponent();
            this.DataContext = this;
        }
    }
}
