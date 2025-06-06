# 기여 가이드라인

Windows 11 시작 메뉴 백업 도구 프로젝트에 기여해 주셔서 감사합니다!

## 🚀 시작하기

### 개발 환경 설정

1. **필수 도구 설치**
   - Visual Studio 2022 (WPF 워크로드 포함)
   - .NET 8.0 SDK
   - Git

2. **프로젝트 클론**
   ```bash
   git clone https://github.com/yourusername/StartMenuBackupTool.git
   cd StartMenuBackupTool
   ```

3. **솔루션 열기**
   - Visual Studio에서 `StartMenuBackupTool.sln` 열기
   - NuGet 패키지 복원

## 📝 코드 스타일 가이드

### C# 코딩 규칙

- **명명 규칙**
  - 클래스, 메서드: PascalCase
  - 변수, 매개변수: camelCase
  - 상수: UPPER_CASE
  - Private 필드: _camelCase

- **MVVM 패턴 준수**
  - View: XAML 파일 (UI만 포함)
  - ViewModel: 비즈니스 로직과 데이터 바인딩
  - Model: 데이터 구조
  - 코드 비하인드 최소화

### XAML 스타일

- 들여쓰기: 4 스페이스
- 속성 정렬: 알파벳 순서
- 리소스 사용: 반복되는 스타일은 리소스로 정의

## 🔧 기여 방법

### 이슈 보고

1. 기존 이슈 확인
2. 새 이슈 생성 시 템플릿 사용
3. 재현 가능한 단계 포함
4. 스크린샷 첨부 (가능한 경우)

### Pull Request

1. **브랜치 생성**
   ```bash
   git checkout -b feature/기능명
   git checkout -b fix/버그명
   ```

2. **커밋 메시지 규칙**
   ```
   feat: 새로운 기능 추가
   fix: 버그 수정
   docs: 문서 수정
   style: 코드 포맷팅
   refactor: 코드 리팩토링
   test: 테스트 추가
   chore: 빌드 업무 수정
   ```

3. **코드 변경**
   - 단위 테스트 추가/수정
   - 기존 테스트 통과 확인
   - 코드 리뷰 가능한 크기로 유지

4. **PR 제출**
   - PR 템플릿 작성
   - 관련 이슈 연결
   - 변경사항 스크린샷 첨부

## 🧪 테스트

### 테스트 실행
```bash
dotnet test
```

### 테스트 작성 가이드
- 새로운 기능에는 반드시 테스트 추가
- 버그 수정 시 재발 방지 테스트 추가
- Given-When-Then 패턴 사용

## 📋 체크리스트

PR 제출 전 확인사항:

- [ ] 코드가 컴파일되고 실행됨
- [ ] 모든 테스트 통과
- [ ] 코드 스타일 가이드 준수
- [ ] 새로운 종속성 최소화
- [ ] 문서 업데이트 (필요시)
- [ ] 변경사항이 다른 기능에 영향 없음

## 🤝 행동 강령

- 존중과 배려의 커뮤니케이션
- 건설적인 피드백
- 다양성 존중
- 협력적인 문제 해결

## 📞 문의

- GitHub Issues 사용
- 긴급한 보안 문제는 이메일로 연락

감사합니다! 🎉 