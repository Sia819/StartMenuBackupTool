﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StartMenuBackupTool.Properties {
    using System;
    
    
    /// <summary>
    ///   지역화된 문자열 등을 찾기 위한 강력한 형식의 리소스 클래스입니다.
    /// </summary>
    // 이 클래스는 ResGen 또는 Visual Studio와 같은 도구를 통해 StronglyTypedResourceBuilder
    // 클래스에서 자동으로 생성되었습니다.
    // 멤버를 추가하거나 제거하려면 .ResX 파일을 편집한 다음 /str 옵션을 사용하여 ResGen을
    // 다시 실행하거나 VS 프로젝트를 다시 빌드하십시오.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   이 클래스에서 사용하는 캐시된 ResourceManager 인스턴스를 반환합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("StartMenuBackupTool.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   이 강력한 형식의 리소스 클래스를 사용하여 모든 리소스 조회에 대해 현재 스레드의 CurrentUICulture 속성을
        ///   재정의합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   This application requires administrator privileges.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string AdminRequired {
            get {
                return ResourceManager.GetString("AdminRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Administrator privileges are required to backup and restore Start Menu. The application will now close.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string AdminRequiredMessage {
            get {
                return ResourceManager.GetString("AdminRequiredMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup and restore Start Menu layouts과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string AppDescription {
            get {
                return ResourceManager.GetString("AppDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Windows 11 Start Menu Backup Tool과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string AppTitle {
            get {
                return ResourceManager.GetString("AppTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup created successfully.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupCreated {
            get {
                return ResourceManager.GetString("BackupCreated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   An error occurred while creating the backup과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupCreationError {
            get {
                return ResourceManager.GetString("BackupCreationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup deleted successfully.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupDeleted {
            get {
                return ResourceManager.GetString("BackupDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup List과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupList {
            get {
                return ResourceManager.GetString("BackupList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup Name과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupName {
            get {
                return ResourceManager.GetString("BackupName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Please enter a backup name.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupNameRequired {
            get {
                return ResourceManager.GetString("BackupNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup file not found과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupNotFound {
            get {
                return ResourceManager.GetString("BackupNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup restored successfully. Explorer will restart.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupRestored {
            get {
                return ResourceManager.GetString("BackupRestored", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0} backups found.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string BackupsFound {
            get {
                return ResourceManager.GetString("BackupsFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cancel과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Cancel {
            get {
                return ResourceManager.GetString("Cancel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Are you sure you want to delete backup &apos;{0}&apos;?과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string ConfirmDelete {
            get {
                return ResourceManager.GetString("ConfirmDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Are you sure you want to restore this backup? Current Start Menu layout will be replaced and Explorer will restart.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string ConfirmRestore {
            get {
                return ResourceManager.GetString("ConfirmRestore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Create Backup과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CreateBackup {
            get {
                return ResourceManager.GetString("CreateBackup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Creating backup...과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string CreatingBackup {
            get {
                return ResourceManager.GetString("CreatingBackup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   {0:MM/dd/yyyy HH:mm}과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string DateFormat {
            get {
                return ResourceManager.GetString("DateFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Delete과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Delete {
            get {
                return ResourceManager.GetString("Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Deleting backup...과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string DeletingBackup {
            get {
                return ResourceManager.GetString("DeletingBackup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Description (Optional)과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Description {
            get {
                return ResourceManager.GetString("Description", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Edit과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Edit {
            get {
                return ResourceManager.GetString("Edit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Edit Backup Information과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string EditBackupInfo {
            get {
                return ResourceManager.GetString("EditBackupInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   English과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string English {
            get {
                return ResourceManager.GetString("English", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Error과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Invalid backup file format과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string InvalidBackupFile {
            get {
                return ResourceManager.GetString("InvalidBackupFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   한국어과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Korean {
            get {
                return ResourceManager.GetString("Korean", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Language과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Language {
            get {
                return ResourceManager.GetString("Language", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Loading backups...과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string LoadingBackups {
            get {
                return ResourceManager.GetString("LoadingBackups", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Backup metadata not found과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string MetadataNotFound {
            get {
                return ResourceManager.GetString("MetadataNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Create New Backup과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string NewBackup {
            get {
                return ResourceManager.GetString("NewBackup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ready과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Ready {
            get {
                return ResourceManager.GetString("Ready", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Refresh과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Refresh {
            get {
                return ResourceManager.GetString("Refresh", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Restore과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Restore {
            get {
                return ResourceManager.GetString("Restore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   An error occurred while restoring the backup과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string RestoreError {
            get {
                return ResourceManager.GetString("RestoreError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Restoring backup...과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string RestoringBackup {
            get {
                return ResourceManager.GetString("RestoringBackup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Save과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Save {
            get {
                return ResourceManager.GetString("Save", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Please select a backup to delete.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string SelectBackupToDelete {
            get {
                return ResourceManager.GetString("SelectBackupToDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Please select a backup to edit.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string SelectBackupToEdit {
            get {
                return ResourceManager.GetString("SelectBackupToEdit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Please select a backup to restore.과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string SelectBackupToRestore {
            get {
                return ResourceManager.GetString("SelectBackupToRestore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Untitled과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string Untitled {
            get {
                return ResourceManager.GetString("Untitled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   File: 과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        public static string File {
            get {
                return ResourceManager.GetString("File", resourceCulture);
            }
        }
    }
}
