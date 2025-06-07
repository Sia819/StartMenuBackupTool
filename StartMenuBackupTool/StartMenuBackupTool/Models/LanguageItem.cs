namespace StartMenuBackupTool.Models
{
    public class LanguageItem
    {
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;

        public LanguageItem(string code, string displayName, string flag)
        {
            Code = code;
            DisplayName = displayName;
            Flag = flag;
        }
    }
}