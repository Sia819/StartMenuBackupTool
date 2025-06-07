using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StartMenuBackupTool.Models
{
    public class BackupInfo : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _description = string.Empty;
        
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Name 
        { 
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public DateTime BackupDate { get; set; } = DateTime.Now;
        public string BackupPath { get; set; } = string.Empty;
        
        public string Description 
        { 
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public long FileSize { get; set; }
        public bool IsValid { get; set; } = true;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void RefreshDateDisplay()
        {
            OnPropertyChanged(nameof(BackupDate));
        }
    }
}
