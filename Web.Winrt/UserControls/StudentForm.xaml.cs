using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Web.DataLayer.Repositories;
using Web.DataLayer.Util;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.Winrt.UserControls
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : UserControl
    {
        private readonly StudentRepository _studentRepo;

        public StudentForm()
        {
            InitializeComponent();
            _studentRepo = new StudentRepository();
            Loaded += StudentForm_Loaded;
        }

        private async void StudentForm_Loaded(object sender, RoutedEventArgs e)
        {
            LoadListView(await _studentRepo.SelectAll());
        }

        private async void RegisterListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = RegisterListView.SelectedItem as RegisterStudentView;
            if (result != null)
            {
                
                CreateHelper<RegisterStudentView> create = new CreateHelper<RegisterStudentView>();
                await create.CreateFolderAsync();
                await create.DeleteFileAsync("data");
                await create.WriteJsonAsync(result, "data");

                Window window = new Window
                {
                    Content = new RegisterForm {IsRegister = true},
                    Height = 400.282,
                    Width = 648,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                window.ShowDialog();

                //RegisterForm registerForm = new RegisterForm();
                //registerForm.Show();
            }
        }

        private void LoadListView(IEnumerable<Student> model)
        {
            RegisterListView.Items.Clear();
            foreach (Student student in model)
            {
                RegisterListView.Items.Add(new RegisterStudentView
                {
                    StudentId = student.StudentId,
                    FullName = student.FullName,
                    Address = student.Address,
                    Uid = student.Uid
                });
            }
        }

        private async void CmbRegisterOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbRegisterOptions.SelectedIndex == 0)
            {
                LoadListView(await _studentRepo.SelectAll());
            }
            else if (CmbRegisterOptions.SelectedIndex == 1)
            {
                LoadListView(await _studentRepo.StudentWithUid());
            }
            else if (CmbRegisterOptions.SelectedIndex == 2)
            {
                LoadListView(await _studentRepo.StudentWithoutUid());
            }
        }
    }
}
