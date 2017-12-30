using Xamarin.Forms;

namespace StudyGroupFinder
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MasterPage()
        {
            InitializeComponent();
        }
    }
}
