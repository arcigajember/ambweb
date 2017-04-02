using System.Windows;
using Web.Winrt.UserControls;

namespace Web.Winrt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AttendanceMonitoring _attendanceForm;
        private readonly StudentForm _studentForm;
        private readonly RegisterForm _registerForm;
        public MainWindow()
        {
            _registerForm = new RegisterForm();
            _attendanceForm = new AttendanceMonitoring();
            _studentForm = new StudentForm();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _attendanceForm.IsSchedule = true;
            _registerForm.IsRegister = false;
            MainGrid.Children.Add(_attendanceForm);

        }

        private void BtnMain_OnClick(object sender, RoutedEventArgs e)
        {
            _attendanceForm.IsSchedule = true;
            _registerForm.IsRegister = false;
            MainGrid.Children.Clear();
            MainGrid.Children.Add(_attendanceForm);
        }

        private void BtnStudent_OnClick(object sender, RoutedEventArgs e)
        {
            //AttendanceMonitoring attendance = new AttendanceMonitoring {IsSchedule = false};
            _attendanceForm.IsSchedule = false;
            _registerForm.IsRegister = false;
            MainGrid.Children.Clear();
            MainGrid.Children.Add(_studentForm);
        }

    }
}
