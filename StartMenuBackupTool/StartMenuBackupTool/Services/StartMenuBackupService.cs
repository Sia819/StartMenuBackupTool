using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json;
using StartMenuBackupTool.Helpers;
using StartMenuBackupTool.Models;

namespace StartMenuBackupTool.Services
{
    public class StartMenuBackupService
    {
        private readonly List<string> _foldersToBackup = new()
        {
            PathHelper.StartMenuLayoutPath,
            PathHelper.TileDatabasePath
        };

        // 백업 생성
        public async Task<BackupInfo> CreateBackupAsync(string backupName, string description = "")
        {
            return await Task.Run(() =>
            {
                try
                {
                    // 백업 폴더 생성
                    if (!Directory.Exists(PathHelper.DefaultBackupPath))
                    {
                        Directory.CreateDirectory(PathHelper.DefaultBackupPath);
                    }

                    var backupFileName = PathHelper.GenerateBackupFileName();
                    var backupFilePath = Path.Combine(PathHelper.DefaultBackupPath, backupFileName);

                    // 백업 정보 생성
                    var backupInfo = new BackupInfo
                    {
                        Name = !string.IsNullOrWhiteSpace(backupName) ? backupName : GenerateUntitledName(),
                        Description = description,
                        BackupPath = backupFilePath,
                        BackupDate = DateTime.Now,
                        IsValid = true
                    };

                    // ZIP 파일 생성
                    using (var archive = ZipFile.Open(backupFilePath, ZipArchiveMode.Create))
                    {
                        // 백업 메타데이터 추가
                        var metadataEntry = archive.CreateEntry("backup_metadata.json");
                        using (var stream = metadataEntry.Open())
                        using (var writer = new StreamWriter(stream))
                        {
                            var json = JsonSerializer.Serialize(backupInfo, new JsonSerializerOptions { WriteIndented = true });
                            writer.Write(json);
                        }

                        // 시작 메뉴 데이터 추가
                        foreach (var folderPath in _foldersToBackup)
                        {
                            if (Directory.Exists(folderPath))
                            {
                                AddDirectoryToZip(archive, folderPath, Path.GetFileName(folderPath));
                            }
                        }
                    }

                    // 파일 크기 업데이트
                    var fileInfo = new FileInfo(backupFilePath);
                    backupInfo.FileSize = fileInfo.Length;

                    return backupInfo;
                }
                catch (Exception ex)
                {
                    throw new Exception($"백업 생성 중 오류가 발생했습니다: {ex.Message}", ex);
                }
            });
        }

        // 백업 복원
        public async Task RestoreBackupAsync(string backupFilePath)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!File.Exists(backupFilePath))
                    {
                        throw new FileNotFoundException("백업 파일을 찾을 수 없습니다.", backupFilePath);
                    }

                    // 임시 폴더에 압축 해제
                    var tempPath = Path.Combine(Path.GetTempPath(), $"StartMenuRestore_{Guid.NewGuid()}");
                    Directory.CreateDirectory(tempPath);

                    try
                    {
                        ZipFile.ExtractToDirectory(backupFilePath, tempPath);

                        // 각 폴더 복원
                        foreach (var directory in Directory.GetDirectories(tempPath))
                        {
                            var dirName = Path.GetFileName(directory);
                            
                            // 메타데이터 파일은 건너뛰기
                            if (dirName == "backup_metadata.json") continue;
                            
                            var targetPath = _foldersToBackup.FirstOrDefault(f => Path.GetFileName(f) == dirName);

                            if (targetPath != null && Directory.Exists(targetPath))
                            {
                                // 기존 파일 백업 (선택적)
                                var backupSuffix = $"_backup_{DateTime.Now:yyyyMMddHHmmss}";
                                
                                // 파일 복원
                                CopyDirectory(directory, targetPath, true);
                            }
                        }

                        // Explorer 재시작하여 변경사항 즉시 적용
                        RestartExplorer();
                    }
                    finally
                    {
                        // 임시 폴더 정리
                        if (Directory.Exists(tempPath))
                        {
                            Directory.Delete(tempPath, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"백업 복원 중 오류가 발생했습니다: {ex.Message}", ex);
                }
            });
        }

        // Explorer.exe 재시작
        public void RestartExplorer()
        {
            try
            {
                // Explorer 프로세스 찾기
                var explorerProcesses = Process.GetProcessesByName("explorer");
                
                // 실행 중인 Explorer가 없는 경우 바로 시작
                if (explorerProcesses.Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        UseShellExecute = true
                    });
                    return;
                }
                
                // 모든 Explorer 프로세스 종료
                foreach (var process in explorerProcesses)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit(5000); // 최대 5초 대기
                    }
                    catch
                    {
                        // 프로세스 종료 실패 시 계속 진행
                    }
                }

                // 잠시 대기 (프로세스가 완전히 종료될 때까지)
                System.Threading.Thread.Sleep(2000);

                // Explorer 재시작
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    UseShellExecute = true
                });

                // Explorer가 완전히 시작될 때까지 잠시 대기
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                // 실패 시에도 Explorer 시작 시도
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        UseShellExecute = true
                    });
                }
                catch
                {
                    // 무시
                }
                
                throw new Exception($"Explorer 재시작 중 오류가 발생했습니다: {ex.Message}", ex);
            }
        }

        // 백업 목록 가져오기
        public async Task<List<BackupInfo>> GetBackupListAsync()
        {
            return await Task.Run(() =>
            {
                var backups = new List<BackupInfo>();
                var untitledNames = new Dictionary<string, int>();

                if (!Directory.Exists(PathHelper.DefaultBackupPath))
                {
                    return backups;
                }

                var files = Directory.GetFiles(PathHelper.DefaultBackupPath, "*.zip");
                foreach (var file in files)
                {
                    try
                    {
                        var fileInfo = new FileInfo(file);
                        BackupInfo? backupInfo = null;

                        // ZIP 파일에서 메타데이터 읽기 시도
                        try
                        {
                            using (var archive = ZipFile.OpenRead(file))
                            {
                                var metadataEntry = archive.GetEntry("backup_metadata.json");
                                if (metadataEntry != null)
                                {
                                    using (var stream = metadataEntry.Open())
                                    using (var reader = new StreamReader(stream))
                                    {
                                        var json = reader.ReadToEnd();
                                        backupInfo = JsonSerializer.Deserialize<BackupInfo>(json);
                                    }
                                }
                            }
                        }
                        catch
                        {
                            // 메타데이터 읽기 실패 시 기본값 사용
                        }

                        // 메타데이터가 없거나 읽기 실패한 경우
                        if (backupInfo == null)
                        {
                            backupInfo = new BackupInfo
                            {
                                BackupDate = fileInfo.CreationTime
                            };
                        }

                        // 이름이 없거나 비어있는 경우 Untitled로 설정
                        if (string.IsNullOrWhiteSpace(backupInfo.Name))
                        {
                            var baseName = "Untitled";
                            if (!untitledNames.ContainsKey(baseName))
                            {
                                backupInfo.Name = baseName;
                                untitledNames[baseName] = 1;
                            }
                            else
                            {
                                untitledNames[baseName]++;
                                backupInfo.Name = $"{baseName} ({untitledNames[baseName]})";
                            }
                        }

                        // 공통 속성 설정
                        backupInfo.BackupPath = file;
                        backupInfo.FileSize = fileInfo.Length;
                        backupInfo.IsValid = ValidateBackupFile(file);

                        backups.Add(backupInfo);
                    }
                    catch
                    {
                        // 잘못된 파일은 무시
                    }
                }

                return backups.OrderByDescending(b => b.BackupDate).ToList();
            });
        }

        // Untitled 이름 생성
        private string GenerateUntitledName()
        {
            var existingBackups = GetBackupListAsync().Result;
            var untitledCount = existingBackups.Count(b => b.Name.StartsWith("Untitled"));
            
            if (untitledCount == 0)
                return "Untitled";
            
            return $"Untitled ({untitledCount + 1})";
        }

        // 백업 파일 유효성 검사
        private bool ValidateBackupFile(string backupFilePath)
        {
            try
            {
                using (var archive = ZipFile.OpenRead(backupFilePath))
                {
                    return archive.Entries.Any();
                }
            }
            catch
            {
                return false;
            }
        }

        // 디렉토리를 ZIP에 추가
        private void AddDirectoryToZip(ZipArchive archive, string sourceDir, string entryName)
        {
            var files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            
            foreach (var file in files)
            {
                var relativePath = Path.GetRelativePath(sourceDir, file);
                var entry = Path.Combine(entryName, relativePath).Replace('\\', '/');
                archive.CreateEntryFromFile(file, entry);
            }
        }

        // 디렉토리 복사
        private void CopyDirectory(string sourceDir, string destDir, bool overwrite)
        {
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            // 파일 복사
            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, overwrite);
            }

            // 하위 디렉토리 복사
            foreach (var subDir in Directory.GetDirectories(sourceDir))
            {
                var destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir, overwrite);
            }
        }

        // 백업 메타데이터 업데이트
        public async Task UpdateBackupMetadataAsync(BackupInfo backupInfo)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (!File.Exists(backupInfo.BackupPath))
                    {
                        throw new FileNotFoundException("백업 파일을 찾을 수 없습니다.", backupInfo.BackupPath);
                    }

                    // 임시 파일 경로
                    var tempFile = Path.GetTempFileName();
                    
                    try
                    {
                        // 기존 ZIP 파일 복사 (메타데이터 제외)
                        using (var sourceArchive = ZipFile.OpenRead(backupInfo.BackupPath))
                        using (var destArchive = ZipFile.Open(tempFile, ZipArchiveMode.Create))
                        {
                            // 메타데이터 파일 제외하고 모든 항목 복사
                            foreach (var entry in sourceArchive.Entries)
                            {
                                if (entry.Name != "backup_metadata.json")
                                {
                                    var destEntry = destArchive.CreateEntry(entry.FullName);
                                    using (var sourceStream = entry.Open())
                                    using (var destStream = destEntry.Open())
                                    {
                                        sourceStream.CopyTo(destStream);
                                    }
                                }
                            }

                            // 업데이트된 메타데이터 추가
                            var metadataEntry = destArchive.CreateEntry("backup_metadata.json");
                            using (var stream = metadataEntry.Open())
                            using (var writer = new StreamWriter(stream))
                            {
                                var json = JsonSerializer.Serialize(backupInfo, new JsonSerializerOptions { WriteIndented = true });
                                writer.Write(json);
                            }
                        }

                        // 원본 파일을 임시 파일로 교체
                        File.Delete(backupInfo.BackupPath);
                        File.Move(tempFile, backupInfo.BackupPath);
                    }
                    finally
                    {
                        // 임시 파일 정리
                        if (File.Exists(tempFile))
                        {
                            File.Delete(tempFile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"백업 메타데이터 업데이트 중 오류가 발생했습니다: {ex.Message}", ex);
                }
            });
        }
    }
} 