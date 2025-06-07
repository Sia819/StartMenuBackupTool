# Windows 11 시작 메뉴 백업 도구 (StartMenuBackupTool)

<div align="center">
  <img src="Images/main-window.png" alt="메인 화면" width="800"/>
  
  [![GitHub release](https://img.shields.io/github/v/release/yourusername/StartMenuBackupTool?include_prereleases)](https://github.com/yourusername/StartMenuBackupTool/releases)
  [![License: AGPL v3](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](https://www.gnu.org/licenses/agpl-3.0)
  [![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=.net)](https://dotnet.microsoft.com/)
  [![WPF](https://img.shields.io/badge/WPF-MVVM-0078D4?style=flat-square)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
  [![Windows](https://img.shields.io/badge/Windows-11-0078D6?style=flat-square&logo=windows)](https://www.microsoft.com/windows)
</div>

## 📥 다운로드

### 최신 버전 (Beta)
[![Download](https://img.shields.io/badge/Download-v0.1.0%20Beta-blue.svg)](https://github.com/yourusername/StartMenuBackupTool/releases/latest)

- **파일**: StartMenuBackupTool.exe (162MB)
- **버전**: v0.1.0 (Beta)
- **SHA256**: `9c28e07bed923e9f910a15bf8bec4c301308e4532f869a4336d910868f0ad012`

> ⚠️ **주의**: 베타 버전이므로 예상치 못한 문제가 발생할 수 있습니다. 중요한 시스템에서는 테스트 후 사용을 권장합니다.

## 📋 개요

Windows 11 시작 메뉴 백업 도구는 Windows 11의 시작 메뉴 레이아웃과 설정을 백업하고 복원할 수 있는 WPF 애플리케이션입니다. 시스템 재설치나 설정 이전 시 유용하게 사용할 수 있습니다.

### 주요 기능

- ✅ **시작 메뉴 백업**: Windows 11 시작 메뉴 레이아웃을 ZIP 파일로 백업
- ♻️ **백업 복원**: 저장된 백업에서 시작 메뉴 설정 복원
- 📝 **백업 관리**: 백업 이름 및 설명 편집
- 🌐 **다국어 지원**: 한국어/영어 언어 전환 (재시작 없이 즉시 적용)
- 🔄 **즉시 적용**: Explorer 자동 재시작으로 변경사항 즉시 반영
- 🎨 **모던 UI**: Windows 11 스타일의 깔끔한 인터페이스

## 🖼️ 스크린샷

### 메인 화면
<img src="Images/main-window.png" alt="메인 화면" width="700"/>

*백업 생성 및 관리가 가능한 메인 인터페이스*

### 백업 편집
<img src="Images/edit-dialog.png" alt="백업 편집" width="400"/>

*백업 이름과 설명을 수정할 수 있는 편집 다이얼로그*

### 백업 복원
<img src="Images/restore-confirm.png" alt="복원 확인" width="500"/>

*백업 복원 전 확인 다이얼로그*

## 🚀 시작하기

### 시스템 요구사항

- Windows 11
- .NET 8.0 Runtime (실행 파일에 포함됨)
- 관리자 권한

### 설치 방법

1. [최신 릴리즈](https://github.com/yourusername/StartMenuBackupTool/releases/latest)에서 `StartMenuBackupTool.exe` 다운로드
2. 다운로드한 파일을 원하는 위치로 이동
3. 마우스 오른쪽 클릭 → "관리자 권한으로 실행"
4. Windows Defender SmartScreen 경고가 나타나면 "추가 정보" → "실행"

### 빌드 방법

```bash
# 저장소 클론
git clone https://github.com/yourusername/StartMenuBackupTool.git

# 프로젝트 디렉토리로 이동
cd StartMenuBackupTool/StartMenuBackupTool

# 빌드
dotnet build

# 실행
dotnet run

# 릴리스 빌드 (단일 실행 파일)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## 💡 사용 방법

### 백업 생성
1. 백업 이름 입력
2. 설명 추가 (선택사항)
3. "백업 생성" 버튼 클릭

### 백업 복원
1. 백업 목록에서 복원할 백업 선택
2. "복원" 버튼 클릭
3. 확인 다이얼로그에서 "예" 선택
4. Explorer가 자동으로 재시작되며 변경사항 적용

### 백업 편집
1. 백업 목록에서 편집할 백업 선택
2. "편집" 버튼 클릭 또는 더블클릭
3. 이름과 설명 수정
4. "저장" 버튼 클릭

### 언어 변경
1. 헤더 우측 상단의 언어 드롭다운 클릭
2. 한국어 또는 English 선택
3. UI가 즉시 선택한 언어로 변경됨

## 📁 프로젝트 구조

```
StartMenuBackupTool/
├── StartMenuBackupTool/
│   ├── Commands/
│   │   └── RelayCommand.cs              # ICommand 구현
│   ├── Helpers/
│   │   ├── Converters.cs                # XAML 값 변환기
│   │   └── PathHelper.cs                # 경로 관리 헬퍼
│   ├── Models/
│   │   ├── BackupInfo.cs                # 백업 정보 모델
│   │   └── LanguageItem.cs              # 언어 선택 모델
│   ├── Properties/
│   │   ├── Resources.resx               # 영어 리소스
│   │   ├── Resources.ko-KR.resx         # 한국어 리소스
│   │   └── Resources.Designer.cs        # 리소스 접근자
│   ├── Services/
│   │   ├── StartMenuBackupService.cs    # 백업/복원 비즈니스 로직
│   │   └── LanguageManager.cs           # 언어 관리 서비스
│   ├── ViewModels/
│   │   ├── ViewModelBase.cs             # ViewModel 기본 클래스
│   │   └── MainViewModel.cs             # 메인 화면 ViewModel
│   ├── Views/
│   │   ├── EditBackupDialog.xaml        # 백업 편집 다이얼로그
│   │   ├── EditBackupDialog.xaml.cs
│   │   ├── MainWindow.xaml              # 메인 윈도우 UI
│   │   └── MainWindow.xaml.cs
│   ├── App.xaml                         # 애플리케이션 진입점
│   ├── App.xaml.cs
│   ├── App.config                       # 앱 설정 파일
│   ├── app.manifest                     # 관리자 권한 매니페스트
│   └── StartMenuBackupTool.csproj       # 프로젝트 파일
├── Images/                              # 스크린샷
├── .gitignore
├── CONTRIBUTING.md                      # 기여 가이드
├── LICENSE                              # GNU AGPL v3 라이센스
├── README.md
└── StartMenuBackupTool.sln              # 솔루션 파일
```

## 🏗️ 아키텍처

### MVVM 패턴
- **Model**: 백업 데이터 및 비즈니스 로직
- **View**: XAML 기반 UI
- **ViewModel**: View와 Model 간의 데이터 바인딩 및 명령 처리

### 주요 기술 스택
- **Framework**: .NET 8.0, WPF
- **패턴**: MVVM (Model-View-ViewModel)
- **언어**: C# 12.0
- **UI**: XAML, Modern Windows 11 Design
- **다국어**: .NET 리소스 파일 (resx)

## 🔒 보안 및 권한

- **관리자 권한 필요**: 시스템 파일 접근을 위해 관리자 권한으로 실행
- **백업 위치**: `문서\StartMenuBackups` 폴더에 안전하게 저장
- **데이터 무결성**: ZIP 압축 및 메타데이터로 백업 관리

## 📝 개발 노트

### 백업 대상 경로
```csharp
// Windows 11 시작 메뉴 레이아웃 데이터
%LocalAppData%\Packages\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\LocalState

// 시작 메뉴 타일 데이터베이스
%LocalAppData%\Microsoft\Windows\Shell
```

### Explorer 재시작 로직
백업 복원 후 자동으로 Explorer.exe를 재시작하여 변경사항을 즉시 적용합니다.

## 🚀 릴리스 노트

### v0.1.0 (Beta) - 2024-06-07
- 🎉 첫 베타 릴리스
- ✨ 기본 백업/복원 기능 구현
- 🌐 한국어/영어 다국어 지원
- 📝 백업 편집 기능
- 🎨 Windows 11 스타일 UI

### 향후 계획
- **v0.2.0**: 버그 수정 및 안정성 개선
- **v0.3.0**: 자동 백업 기능
- **v0.4.0**: 백업 스케줄링
- **v1.0.0**: 정식 릴리스

## 🤝 기여하기

기여를 환영합니다! [CONTRIBUTING.md](CONTRIBUTING.md)를 참고해 주세요.

### 기여 방법
1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 라이센스

이 프로젝트는 GNU Affero General Public License v3.0 (AGPL-3.0) 라이센스 하에 배포됩니다.

### 주요 내용
- ✅ 상업적 사용 가능
- ✅ 수정 가능
- ✅ 배포 가능
- ✅ 특허 사용 가능
- ⚠️ 소스 코드 공개 의무
- ⚠️ 동일 라이센스 유지
- ⚠️ 네트워크 사용 시에도 소스 공개

자세한 내용은 [LICENSE](LICENSE) 파일을 참조하세요.

## 🙏 감사의 말

- Windows 11 디자인 가이드라인
- .NET 커뮤니티
- WPF MVVM 패턴 가이드
- 오픈소스 커뮤니티

## 📞 문의

프로젝트 관련 문의사항이 있으시면 [Issues](https://github.com/yourusername/StartMenuBackupTool/issues) 탭을 이용해 주세요.

## ⚠️ 주의사항

- 이 도구는 Windows 11 전용입니다
- 백업 복원 시 현재 시작 메뉴 설정이 덮어씌워집니다
- 중요한 시스템에서는 사용 전 테스트를 권장합니다
- 백업 파일은 같은 사용자 계정에서만 복원 가능합니다

---

<div align="center">
  Made with ❤️ for Windows 11 users
  
  <br/>
  
  [![GitHub stars](https://img.shields.io/github/stars/yourusername/StartMenuBackupTool?style=social)](https://github.com/yourusername/StartMenuBackupTool)
</div>