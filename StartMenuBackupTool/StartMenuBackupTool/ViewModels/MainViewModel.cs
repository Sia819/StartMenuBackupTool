using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StartMenuBackupTool.Commands;
using StartMenuBackupTool.Models;
using StartMenuBackupTool.Services;
using StartMenuBackupTool.Views;

namespace StartMenuBackupTool.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly StartMenuBackupService _backupService;
        
        private ObservableCollection<BackupInfo> _backups;
        private BackupInfo? _selectedBackup;
        private string _newBackupName = string.Empty;
        private string _newBackupDescription = string.Empty;
        private bool _isProcessing;
        private string _statusMessage = "준비됨";

        public MainViewModel()
        {
            _backupService = new StartMenuBackupService();
            _backups = new ObservableCollection<BackupInfo>();

            // 명령 초기화
            CreateBackupCommand = new RelayCommand(async _ => await CreateBackupAsync(), _ => !IsProcessing && !string.IsNullOrWhiteSpace(NewBackupName));
            RestoreBackupCommand = new RelayCommand(async _ => await RestoreBackupAsync(), _ => !IsProcessing && SelectedBackup != null);
            RefreshBackupsCommand = new RelayCommand(async _ => await LoadBackupsAsync(), _ => !IsProcessing);
            DeleteBackupCommand = new RelayCommand(async _ => await DeleteBackupAsync(), _ => !IsProcessing && SelectedBackup != null);
            EditBackupCommand = new RelayCommand(async _ => await EditBackupAsync(), _ => !IsProcessing && SelectedBackup != null);

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

        private async Task LoadBackupsAsync()
        {
            try
            {
                IsProcessing = true;
                StatusMessage = "백업 목록을 불러오는 중...";

                var backups = await _backupService.GetBackupListAsync();
                
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Backups.Clear();
                    foreach (var backup in backups)
                    {
                        Backups.Add(backup);
                    }
                });

                StatusMessage = $"{backups.Count}개의 백업을 찾았습니다.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류: {ex.Message}";
                MessageBox.Show($"백업 목록을 불러오는 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
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
                StatusMessage = "백업을 생성하는 중...";

                var backup = await _backupService.CreateBackupAsync(NewBackupName, NewBackupDescription);
                
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Backups.Insert(0, backup);
                    NewBackupName = string.Empty;
                    NewBackupDescription = string.Empty;
                });

                StatusMessage = "백업이 성공적으로 생성되었습니다.";
                MessageBox.Show("시작 메뉴 백업이 성공적으로 생성되었습니다.", "성공", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류: {ex.Message}";
                MessageBox.Show($"백업 생성 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
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
                $"'{SelectedBackup.Name}' 백업을 복원하시겠습니까?\n\n" +
                "주의사항:\n" +
                "• 현재 시작 메뉴 설정이 덮어써집니다\n" +
                "• Explorer가 자동으로 재시작됩니다\n" +
                "• 잠시 화면이 깜빡일 수 있습니다\n" +
                "• 열려있는 파일 탐색기 창이 닫힙니다\n\n" +
                "계속하시겠습니까?",
                "백업 복원 확인",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                IsProcessing = true;
                StatusMessage = "백업을 복원하는 중...";

                await _backupService.RestoreBackupAsync(SelectedBackup.BackupPath);

                StatusMessage = "백업이 성공적으로 복원되었습니다.";
                MessageBox.Show(
                    "시작 메뉴 백업이 성공적으로 복원되었습니다.\n\nExplorer가 재시작되어 변경사항이 즉시 적용됩니다.",
                    "성공",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류: {ex.Message}";
                MessageBox.Show($"백업 복원 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    StatusMessage = "백업 정보를 업데이트하는 중...";

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

                    StatusMessage = "백업 정보가 업데이트되었습니다.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류: {ex.Message}";
                MessageBox.Show($"백업 정보 수정 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
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
                $"'{SelectedBackup.Name}' 백업을 삭제하시겠습니까?",
                "백업 삭제 확인",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                IsProcessing = true;
                StatusMessage = "백업을 삭제하는 중...";

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

                StatusMessage = "백업이 삭제되었습니다.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"오류: {ex.Message}";
                MessageBox.Show($"백업 삭제 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
} 