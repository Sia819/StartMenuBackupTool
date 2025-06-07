using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StartMenuBackupTool.Models;
using StartMenuBackupTool.Properties;
using StartMenuBackupTool.Services;

namespace StartMenuBackupTool.Views
{
    public partial class EditBackupDialog : Window, INotifyPropertyChanged
    {
        public BackupInfo BackupInfo { get; private set; }
        
        // UI Text Properties
        public string EditBackupInfoTitle => Properties.Resources.EditBackupInfo;
        public string BackupNameLabel => Properties.Resources.BackupName;
        public string DescriptionLabel => Properties.Resources.Description;
        public string CancelText => Properties.Resources.Cancel;
        public string SaveText => Properties.Resources.Save;
        public string BackupNameRequiredMessage => Properties.Resources.BackupNameRequired;
        public string ErrorTitle => Properties.Resources.Error;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public EditBackupDialog(BackupInfo backupInfo)
        {
            InitializeComponent();
            DataContext = this;
            BackupInfo = backupInfo;
            
            // 기존 값 설정
            NameTextBox.Text = backupInfo.Name;
            DescriptionTextBox.Text = backupInfo.Description;
            
            // 포커스 설정
            NameTextBox.Focus();
            NameTextBox.SelectAll();
            
            // Subscribe to language change event
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }
        
        private void OnLanguageChanged(object? sender, System.Globalization.CultureInfo e)
        {
            // Update all UI text properties
            OnPropertyChanged(nameof(EditBackupInfoTitle));
            OnPropertyChanged(nameof(BackupNameLabel));
            OnPropertyChanged(nameof(DescriptionLabel));
            OnPropertyChanged(nameof(CancelText));
            OnPropertyChanged(nameof(SaveText));
            OnPropertyChanged(nameof(BackupNameRequiredMessage));
            OnPropertyChanged(nameof(ErrorTitle));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 이름이 비어있으면 경고
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show(BackupNameRequiredMessage, ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
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
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            // Unsubscribe from language change event
            LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
        }
    }
} 