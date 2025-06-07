# CLAUDE.md

이 파일은 이 저장소의 코드 작업 시 Claude Code (claude.ai/code)를 위한 가이드를 제공합니다.

## 명령어

### 빌드 및 실행
```bash
# 프로젝트 빌드
dotnet build StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj

# 애플리케이션 실행 (관리자 권한 필요)
dotnet run --project StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj

# 릴리스 버전 빌드
dotnet build -c Release StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj

# Windows용 독립 실행형 파일 게시
dotnet publish -c Release -r win-x64 --self-contained StartMenuBackupTool/StartMenuBackupTool/StartMenuBackupTool.csproj
```

### 테스트
현재 테스트 프로젝트가 없습니다. 테스트 구현 시:
```bash
# 테스트 실행 (사용 가능한 경우)
dotnet test

# 커버리지와 함께 실행 (사용 가능한 경우)
dotnet test /p:CollectCoverage=true
```

## 아키텍처 개요

이것은 엄격한 MVVM 패턴을 따르는 Windows 11 시작 메뉴 레이아웃 백업 및 복원을 위한 WPF 데스크톱 애플리케이션입니다.

### 주요 아키텍처 결정사항

1. **MVVM 패턴 (필수)**
   - View는 최소한의 코드 비하인드와 함께 XAML만 포함
   - ViewModel이 모든 비즈니스 로직과 상태 처리
   - Model은 단순한 데이터 구조
   - 사용자 상호작용을 위해 RelayCommand 패턴 사용

2. **서비스 레이어**
   - `StartMenuBackupService`가 모든 백업/복원 작업을 캡슐화
   - 파일 시스템 작업, 압축, 메타데이터 관리 처리
   - 모든 장시간 실행 작업은 비동기

3. **데이터 흐름**
   - 백업은 `%Documents%\StartMenuBackups`에 ZIP 파일로 저장
   - 각 백업에는 Windows 시작 메뉴 데이터와 JSON 메타데이터 포함
   - INotifyPropertyChanged를 통한 View와 ViewModel 간 양방향 데이터 바인딩

4. **중요 경로**
   - 시작 메뉴 데이터: `%LocalAppData%\Packages\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\LocalState`
   - 타일 데이터베이스: `%LocalAppData%\Microsoft\Windows\Shell`
   - 애플리케이션은 관리자 권한 필요 (app.manifest에 구성됨)

### 개발 표준

CONTRIBUTING.md로부터:
- **C# 명명**: 클래스/메서드는 PascalCase, 변수는 camelCase, private 필드는 _camelCase
- **XAML**: 4칸 들여쓰기, 속성은 알파벳순
- **Git 커밋**: 규약 형식 사용 (feat:, fix:, docs: 등)
- **모든 UI 텍스트는 한국어로** - 일관성 유지

### 중요 고려사항

1. **관리자 권한 필요**: app.manifest가 관리자 권한 상승 요청
2. **탐색기 재시작**: 복원 작업 후 Explorer.exe가 자동으로 재시작됨
3. **외부 종속성 없음**: .NET 내장 압축 라이브러리만 사용
4. **Windows 11 전용**: Windows 10과 다른 Windows 11 시작 메뉴 구조를 대상으로 함