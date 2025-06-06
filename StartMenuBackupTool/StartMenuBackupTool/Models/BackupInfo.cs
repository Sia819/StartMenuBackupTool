namespace StartMenuBackupTool.Models
{
    public class BackupInfo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public DateTime BackupDate { get; set; } = DateTime.Now;
        public string BackupPath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public bool IsValid { get; set; } = true;
    }
}
