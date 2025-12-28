using HospitalManagement.Views.Interfaces;

namespace HospitalManagement.Presenters
{
    public class DashboardPresenter
    {
        private readonly IDashboardView _view;

        public DashboardPresenter(IDashboardView view)
        {
            _view = view;
        }

        public void NavigateTo(string menuItem)
        {
            _view.UpdateHeaderTitle(menuItem);
            
            if (menuItem == "Trang chủ")
            {
                _view.LoadHomeContent();
            }
            else
            {
                _view.LoadContent(menuItem);
            }
        }

        public void Logout()
        {
            _view.ShowLogoutConfirmation();
        }
    }
}
