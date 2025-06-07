using System;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StartMenuBackupTool.ViewModels;
using StartMenuBackupTool.Properties;

namespace StartMenuBackupTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // ViewModel 설정 (Title 바인딩을 위해 먼저 설정)
            DataContext = new MainViewModel();
            
            InitializeComponent();
            
            // 관리자 권한 확인
            if (!IsRunAsAdministrator())
            {
                var result = MessageBox.Show(
                    Properties.Resources.AdminRequiredMessage,
                    Properties.Resources.AdminRequired,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    RestartAsAdministrator();
                }
            }
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
                MessageBox.Show($"{Properties.Resources.Error}: {ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
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