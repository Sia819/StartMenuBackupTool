using System;
using System.IO;

namespace StartMenuBackupTool.Helpers
{
    public static class PathHelper
    {
        // Windows 11 시작 메뉴 레이아웃 데이터 경로
        public static string StartMenuLayoutPath
        {
            get
            {
                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(localAppData, @"Packages\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\LocalState");
            }
        }

        // 백업 폴더 기본 경로
        public static string DefaultBackupPath
        {
            get
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return Path.Combine(documentsPath, "StartMenuBackups");
            }
        }

        // 시작 메뉴 타일 데이터베이스 경로
        public static string TileDatabasePath
        {
            get
            {
                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(localAppData, @"Microsoft\Windows\Shell");
            }
        }

        // 백업 파일명 생성
        public static string GenerateBackupFileName()
        {
            return $"StartMenuBackup_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
        }

        // 경로 유효성 검사
        public static bool IsValidPath(string path)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(path) && Directory.Exists(Path.GetDirectoryName(path));
            }
            catch
            {
                return false;
            }
        }
    }
} 