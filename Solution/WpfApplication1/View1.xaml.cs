using System.Windows.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// View1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class View1 : UserControl
    {
        public View1(ViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
