using System;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StartMenuBackupTool.ViewModels;

namespace StartMenuBackupTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // 관리자 권한 확인
            if (!IsRunAsAdministrator())
            {
                var result = MessageBox.Show(
                    "이 프로그램은 관리자 권한이 필요합니다.\n관리자 권한으로 다시 실행하시겠습니까?",
                    "관리자 권한 필요",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RestartAsAdministrator();
                }
            }

            // ViewModel 설정
            DataContext = new MainViewModel();
        }

        private static bool IsRunAsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RestartAsAdministrator()
        {
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName,
                    Verb = "runas"
                };

                System.Diagnostics.Process.Start(processInfo);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"관리자 권한으로 실행할 수 없습니다: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // ListBox의 선택된 항목이 있고, ViewModel의 EditBackupCommand가 실행 가능한 경우
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {
                if (DataContext is MainViewModel viewModel && viewModel.EditBackupCommand.CanExecute(null))
                {
                    viewModel.EditBackupCommand.Execute(null);
                }
            }
        }
    }
}