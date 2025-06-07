# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

### Build and Run
```bash
# Build the project
dotnet build StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj

# Run the application (requires admin privileges)
dotnet run --project StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj

# Build release version
dotnet build -c Release StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj

# Publish self-contained executable for Windows
dotnet publish -c Release -r win-x64 --self-contained StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj
```

### Testing
No test projects currently exist. When implementing tests:
```bash
# Run tests (when available)
dotnet test

# Run with coverage (when available)
dotnet test /p:CollectCoverage=true
```

## Architecture Overview

This is a WPF desktop application for backing up and restoring Windows 11 Start Menu layouts, following strict MVVM pattern.

### Key Architectural Decisions

1. **MVVM Pattern (Mandatory)**
   - Views contain only XAML with minimal code-behind
   - ViewModels handle all business logic and state
   - Models are simple data structures
   - Commands use RelayCommand pattern for user interactions

2. **Service Layer**
   - `StartMenuBackupService` encapsulates all backup/restore operations
   - Handles file system operations, compression, and metadata management
   - All long-running operations are async

3. **Data Flow**
   - Backups stored as ZIP files in `%Documents%\StartMenuBackups`
   - Each backup contains Windows Start Menu data plus JSON metadata
   - Two-way data binding between Views and ViewModels via INotifyPropertyChanged

4. **Critical Paths**
   - Start Menu data: `%LocalAppData%\Packages\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\LocalState`
   - Tile database: `%LocalAppData%\Microsoft\Windows\Shell`
   - Application requires Administrator privileges (configured in app.manifest)

### Development Standards

From CONTRIBUTING.md:
- **C# Naming**: PascalCase for classes/methods, camelCase for variables, _camelCase for private fields
- **XAML**: 4-space indentation, alphabetical attributes
- **Git commits**: Use conventional format (feat:, fix:, docs:, etc.)
- **All UI text is in Korean** - maintain consistency

### Important Considerations

1. **Admin Privileges Required**: The app.manifest requests administrator elevation
2. **Explorer Restart**: After restore operations, Explorer.exe is automatically restarted
3. **No External Dependencies**: Uses only .NET built-in compression libraries
4. **Windows 11 Specific**: Targets Windows 11 Start Menu structure which differs from Windows 10