using Xamarin.Forms;

namespace StudyGroupFinder
{
    public partial class StudyGroupFinderPage : MasterDetailPage
    {
        public StudyGroupFinderPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new Account());
            IsPresented = false;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new Account());
            IsPresented = false;


        }

        void Handle_Clicked2(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new Page2());
            IsPresented = false;


        }
    }
}
