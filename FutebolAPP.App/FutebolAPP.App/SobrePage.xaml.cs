using FutebolAPP.App.ViewModels;

namespace FutebolAPP.App
{
    public partial class SobrePage
    {
        private SobreViewModel ViewModel => BindingContext as SobreViewModel;

        public SobrePage()
        {
            InitializeComponent();
        }
    }
}