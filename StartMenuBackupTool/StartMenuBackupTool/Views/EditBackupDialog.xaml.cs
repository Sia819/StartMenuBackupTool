using System.Windows;
using StartMenuBackupTool.Models;

namespace StartMenuBackupTool.Views
{
    public partial class EditBackupDialog : Window
    {
        public BackupInfo BackupInfo { get; private set; }
        
        public EditBackupDialog(BackupInfo backupInfo)
        {
            InitializeComponent();
            BackupInfo = backupInfo;
            
            // 기존 값 설정
            NameTextBox.Text = backupInfo.Name;
            DescriptionTextBox.Text = backupInfo.Description;
            
            // 포커스 설정
            NameTextBox.Focus();
            NameTextBox.SelectAll();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 이름이 비어있으면 경고
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("백업 이름을 입력해주세요.", "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.Focus();
                return;
            }

            // 변경사항 저장
            BackupInfo.Name = NameTextBox.Text.Trim();
            BackupInfo.Description = DescriptionTextBox.Text.Trim();
            
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 