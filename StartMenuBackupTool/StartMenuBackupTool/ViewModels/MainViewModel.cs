using StartMenuBackupTool.Commands;
using StartMenuBackupTool.Models;
using StartMenuBackupTool.Services;
using StartMenuBackupTool.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Globalization;

namespace StartMenuBackupTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly StartMenuBackupService _backupService;

        private ObservableCollection<BackupInfo> _backups;
        private BackupInfo? _selectedBackup;
        private string _newBackupName = string.Empty;
        private string _newBackupDescription = string.Empty;
        private bool _isProcessing;
        private string _statusMessage = Properties.Resources.Ready;
        private bool _isKoreanSelected;
        private bool _isEnglishSelected;
        private string _selectedLanguage = "ko-KR";

        public MainViewModel()
        {
            _backupService = new StartMenuBackupService();
            _backups = new ObservableCollection<BackupInfo>();
            
            // Initialize language items
            AvailableLanguages = new ObservableCollection<LanguageItem>
            {
                new LanguageItem("ko-KR", "한국어", "🇰🇷"),
                new LanguageItem("en-US", "English", "🇺🇸")
            };

            // 명령 초기화
            CreateBackupCommand = new RelayCommand(async _ => await CreateBackupAsync(), _ => !IsProcessing && !string.IsNullOrWhiteSpace(NewBackupName));
            RestoreBackupCommand = new RelayCommand(async _ => await RestoreBackupAsync(), _ => !IsProcessing && SelectedBackup != null);
            RefreshBackupsCommand = new RelayCommand(async _ => await LoadBackupsAsync(), _ => !IsProcessing);
            DeleteBackupCommand = new RelayCommand(async _ => await DeleteBackupAsync(), _ => !IsProcessing && SelectedBackup != null);
            EditBackupCommand = new RelayCommand(async _ => await EditBackupAsync(), _ => !IsProcessing && SelectedBackup != null);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);

            // 언어 설정 초기화
            InitializeLanguage();

            // 초기 로드
            _ = LoadBackupsAsync();
        }

        public ObservableCollection<BackupInfo> Backups
        {
            get => _backups;
            set => SetProperty(ref _backups, value);
        }

        public BackupInfo? SelectedBackup
        {
            get => _selectedBackup;
            set => SetProperty(ref _selectedBackup, value);
        }

        public string NewBackupName
        {
            get => _newBackupName;
            set
            {
                SetProperty(ref _newBackupName, value);
                (CreateBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string NewBackupDescription
        {
            get => _newBackupDescription;
            set => SetProperty(ref _newBackupDescription, value);
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                SetProperty(ref _isProcessing, value);
                (CreateBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (RestoreBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (RefreshBackupsCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (EditBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public ICommand CreateBackupCommand { get; }
        public ICommand RestoreBackupCommand { get; }
        public ICommand RefreshBackupsCommand { get; }
        public ICommand DeleteBackupCommand { get; }
        public ICommand EditBackupCommand { get; }
        public ICommand ChangeLanguageCommand { get; }

        public bool IsKoreanSelected
        {
            get => _isKoreanSelected;
            set => SetProperty(ref _isKoreanSelected, value);
        }

        public bool IsEnglishSelected
        {
            get => _isEnglishSelected;
            set => SetProperty(ref _isEnglishSelected, value);
        }

        // UI Text Properties for dynamic language switching
        public string AppTitle => Properties.Resources.AppTitle;
        public string AppDescription => Properties.Resources.AppDescription;
        public string NewBackup => Properties.Resources.NewBackup;
        public string BackupName => Properties.Resources.BackupName;
        public string Description => Properties.Resources.Description;
        public string BackupList => Properties.Resources.BackupList;
        public string CreateBackupText => Properties.Resources.CreateBackup;
        public string Refresh => Properties.Resources.Refresh;
        public string Edit => Properties.Resources.Edit;
        public string Restore => Properties.Resources.Restore;
        public string Delete => Properties.Resources.Delete;
        public string EditBackupInfo => Properties.Resources.EditBackupInfo;
        public string Cancel => Properties.Resources.Cancel;
        public string Save => Properties.Resources.Save;
        public string Language => Properties.Resources.Language;
        public string Korean => Properties.Resources.Korean;
        public string English => Properties.Resources.English;
        
        // Language Selection Properties
        public ObservableCollection<LanguageItem> AvailableLanguages { get; }
        
        public LanguageItem? SelectedLanguageItem
        {
            get => AvailableLanguages?.FirstOrDefault(l => l.Code == _selectedLanguage);
            set
            {
                if (value != null && value.Code != _selectedLanguage)
                {
                    _selectedLanguage = value.Code;
                    OnPropertyChanged();
                    ChangeLanguage(value.Code);
                }
            }
        }

        private async Task LoadBackupsAsync()
        {
            try
            {
                IsProcessing = true;
                StatusMessage = Properties.Resources.LoadingBackups;

                var backups = await _backupService.GetBackupListAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Backups.Clear();
                    foreach (var backup in backups)
                    {
                        Backups.Add(backup);
                    }
                });

                StatusMessage = string.Format(Properties.Resources.BackupsFound, backups.Count);
            }
            catch (Exception ex)
            {
                StatusMessage = $"{Properties.Resources.Error}: {ex.Message}";
                MessageBox.Show($"{Properties.Resources.LoadingBackups}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async Task CreateBackupAsync()
        {
            try
            {
                IsProcessing = true;
                StatusMessage = Properties.Resources.CreatingBackup;

                var backup = await _backupService.CreateBackupAsync(NewBackupName, NewBackupDescription);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Backups.Insert(0, backup);
                    NewBackupName = string.Empty;
                    NewBackupDescription = string.Empty;
                });

                StatusMessage = Properties.Resources.BackupCreated;
                MessageBox.Show(Properties.Resources.BackupCreated, Properties.Resources.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                StatusMessage = $"{Properties.Resources.Error}: {ex.Message}";
                MessageBox.Show($"{Properties.Resources.BackupCreationError}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async Task RestoreBackupAsync()
        {
            if (SelectedBackup == null) return;

            var result = MessageBox.Show(
                Properties.Resources.ConfirmRestore,
                Properties.Resources.Restore,
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                IsProcessing = true;
                StatusMessage = Properties.Resources.RestoringBackup;

                await _backupService.RestoreBackupAsync(SelectedBackup.BackupPath);

                StatusMessage = Properties.Resources.BackupRestored;
                MessageBox.Show(
                    Properties.Resources.BackupRestored,
                    Properties.Resources.AppTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                StatusMessage = $"{Properties.Resources.Error}: {ex.Message}";
                MessageBox.Show($"{Properties.Resources.RestoreError}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async Task EditBackupAsync()
        {
            if (SelectedBackup == null) return;

            try
            {
                // 백업 정보의 복사본 생성
                var backupCopy = new BackupInfo
                {
                    Id = SelectedBackup.Id,
                    Name = SelectedBackup.Name,
                    Description = SelectedBackup.Description,
                    BackupPath = SelectedBackup.BackupPath,
                    BackupDate = SelectedBackup.BackupDate,
                    FileSize = SelectedBackup.FileSize,
                    IsValid = SelectedBackup.IsValid
                };

                // 편집 다이얼로그 표시
                var dialog = new EditBackupDialog(backupCopy)
                {
                    Owner = Application.Current.MainWindow
                };

                if (dialog.ShowDialog() == true)
                {
                    IsProcessing = true;
                    StatusMessage = Properties.Resources.Save;

                    // 서비스를 통해 메타데이터 업데이트
                    await _backupService.UpdateBackupMetadataAsync(backupCopy);

                    // UI 업데이트
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var index = Backups.IndexOf(SelectedBackup);
                        if (index != -1)
                        {
                            SelectedBackup.Name = backupCopy.Name;
                            SelectedBackup.Description = backupCopy.Description;

                            // ObservableCollection에 변경 알림
                            Backups[index] = SelectedBackup;
                            var temp = SelectedBackup;
                            Backups[index] = null!;
                            Backups[index] = temp;
                        }
                    });

                    StatusMessage = Properties.Resources.Save;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"{Properties.Resources.Error}: {ex.Message}";
                MessageBox.Show($"{Properties.Resources.Error}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async Task DeleteBackupAsync()
        {
            if (SelectedBackup == null) return;

            var result = MessageBox.Show(
                string.Format(Properties.Resources.ConfirmDelete, SelectedBackup.Name),
                Properties.Resources.Delete,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                IsProcessing = true;
                StatusMessage = Properties.Resources.DeletingBackup;

                await Task.Run(() =>
                {
                    if (System.IO.File.Exists(SelectedBackup.BackupPath))
                    {
                        System.IO.File.Delete(SelectedBackup.BackupPath);
                    }
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Backups.Remove(SelectedBackup);
                    SelectedBackup = null;
                });

                StatusMessage = Properties.Resources.BackupDeleted;
            }
            catch (Exception ex)
            {
                StatusMessage = $"{Properties.Resources.Error}: {ex.Message}";
                MessageBox.Show($"{Properties.Resources.Error}\n{ex.Message}", Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private void InitializeLanguage()
        {
            LanguageManager.Instance.InitializeLanguage();
            UpdateLanguageSelection();

            LanguageManager.Instance.LanguageChanged += (sender, culture) =>
            {
                UpdateLanguageSelection();
                RefreshUI();
                
                // Update MainWindow title manually
                if (Application.Current.MainWindow != null)
                {
                    Application.Current.MainWindow.Title = Properties.Resources.AppTitle;
                }
            };
        }

        private void UpdateLanguageSelection()
        {
            var currentCulture = LanguageManager.Instance.CurrentCulture.Name;
            IsKoreanSelected = currentCulture == "ko-KR";
            IsEnglishSelected = currentCulture == "en-US";
            _selectedLanguage = currentCulture;
            OnPropertyChanged(nameof(SelectedLanguageItem));
        }

        private void ChangeLanguage(object? parameter)
        {
            if (parameter is string cultureCode)
            {
                LanguageManager.Instance.SetLanguage(cultureCode);
            }
        }

        private void RefreshUI()
        {
            // UI 텍스트 업데이트
            StatusMessage = Properties.Resources.Ready;
            
            // Notify all UI text properties
            OnPropertyChanged(nameof(AppTitle));
            OnPropertyChanged(nameof(AppDescription));
            OnPropertyChanged(nameof(NewBackup));
            OnPropertyChanged(nameof(BackupName));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(BackupList));
            OnPropertyChanged(nameof(CreateBackupText));
            OnPropertyChanged(nameof(Refresh));
            OnPropertyChanged(nameof(Edit));
            OnPropertyChanged(nameof(Restore));
            OnPropertyChanged(nameof(Delete));
            OnPropertyChanged(nameof(EditBackupInfo));
            OnPropertyChanged(nameof(Cancel));
            OnPropertyChanged(nameof(Save));
            OnPropertyChanged(nameof(Language));
            OnPropertyChanged(nameof(Korean));
            OnPropertyChanged(nameof(English));
            
            // Refresh the backups list to update date formats
            foreach (var backup in Backups)
            {
                backup.RefreshDateDisplay();
            }
            OnPropertyChanged(nameof(Backups));
            
            // Force refresh of all commands to update their CanExecute states
            (CreateBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (RestoreBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (RefreshBackupsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (EditBackupCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
}
