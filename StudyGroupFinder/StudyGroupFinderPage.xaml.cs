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
        void Handle_Clicked_Login(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new LoginPage());
            IsPresented = false;
        }

        void Handle_Clicked_Account(object sender, System.EventArgs e)
        {
            Detail = new NavigationPage(new Account());
            IsPresented = false;
        }
    }
}
