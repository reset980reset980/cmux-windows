# cmux for Windows

`cmux-windows`는 Windows용 네이티브 터미널 멀티플렉서입니다.  
tmux/cmux 스타일의 작업 흐름을 Windows 환경에 맞게 WPF + ConPTY 기반으로 구현했습니다.

어울리는 사용자는 다음과 같습니다.

- 여러 프로젝트를 동시에 다루는 개발자
- 탭, 분할창, 워크스페이스를 빠르게 오가고 싶은 사용자
- Codex, Claude 같은 에이전트 작업을 여러 pane에서 병행하는 사용자
- 실행 명령 기록, 세션 복구, 알림 추적이 필요한 사용자

---

## 핵심 기능

- 네이티브 **ConPTY 터미널 백엔드**
- **워크스페이스 + 서피스(탭) + 분할 pane** 구조
- **알림 패널 / unread 추적**
- **명령 로그 / 히스토리 / 세션 보관함(Session Vault)**
- **세션 복구**
- **다크 UI / 키보드 중심 조작**
- **CLI (`cmux`)를 통한 자동화**

이번 개선 버전에는 다음도 포함됩니다.

- 한글 출력 겹침 개선
- pane 하단 **IME 입력 바** 추가
- `F2`, `Ctrl+;` 로 IME 입력 바 포커스 이동

---

## 왜 쓰는가

| 문제 | 해결 방식 |
|---|---|
| 프로젝트마다 터미널 문맥이 섞임 | 워크스페이스로 작업 단위 분리 |
| 터미널 하나로는 부족함 | 탭 + 분할 pane 제공 |
| 에이전트 출력이나 긴 작업을 놓치기 쉬움 | 알림 패널, unread 추적 |
| 어떤 명령을 실행했는지 다시 보고 싶음 | 명령 로그 / 히스토리 제공 |
| 재시작 후 작업 상태가 끊김 | 세션 복구 / transcript 저장 |

---

## 스크린샷

<details>
  <summary>스크린샷 보기</summary>

  <p><strong>메인 워크스페이스</strong></p>
  <img src="assets/screenshots/1.jpg" alt="cmux main workspace" width="1000" />

  <p><strong>스니펫 패널</strong></p>
  <img src="assets/screenshots/2.jpg" alt="cmux snippets panel" width="700" />

  <p><strong>명령 로그 창</strong></p>
  <img src="assets/screenshots/3.jpg" alt="cmux command logs" width="1000" />
</details>

---

## 빌드 및 실행

### 요구사항

- Windows 10 / 11
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- 선택: Visual Studio 2022 / Build Tools

### 클론

```powershell
git clone <repo-url> cmux-windows
cd cmux-windows
```

### 개발 실행

```powershell
dotnet build Cmux.sln -c Debug
dotnet run --project src/Cmux/Cmux.csproj -c Debug
```

---

## Windows 실행 파일 만들기

### 1. Framework-dependent 빌드

```powershell
dotnet publish src/Cmux/Cmux.csproj -c Release -r win-x64 --self-contained false -o publish/cmux-win-x64
```

출력:

- `publish/cmux-win-x64/cmuxw.exe`

### 2. Self-contained 빌드

```powershell
dotnet publish src/Cmux/Cmux.csproj -c Release -r win-x64 --self-contained true -o publish/cmux-win-x64-sc
```

출력:

- `publish/cmux-win-x64-sc/cmuxw.exe`

### 3. 단일 파일 빌드

```powershell
dotnet publish src/Cmux/Cmux.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false -o publish/cmux-win-x64-single
```

출력:

- `publish/cmux-win-x64-single/cmuxw.exe`

### CLI 빌드

```powershell
dotnet publish src/Cmux.Cli/Cmux.Cli.csproj -c Release -r win-x64 --self-contained true -o publish/cmux-cli
```

전역 명령으로 쓰려면 `publish/cmux-cli`를 `PATH`에 추가하면 됩니다.

---

## 처음 5분 사용법

1. `cmuxw.exe` 실행
2. `Ctrl+N` 으로 워크스페이스 생성
3. `Ctrl+T` 로 새 서피스(탭) 생성
4. `Ctrl+D`, `Ctrl+Shift+D` 로 pane 분할
5. `Ctrl+Shift+P` 로 커맨드 팔레트 열기
6. `Ctrl+Shift+L` 로 로그 열기
7. `Ctrl+Shift+V` 로 Session Vault 열기
8. `Ctrl+,` 로 설정 열기

---

## 한글 입력 안내

영문과 일반 명령 입력은 terminal에 직접 입력하면 됩니다.

한글은 환경에 따라 IME 조합 입력이 terminal 본문에서 불안정할 수 있어서, 이 개선 버전에서는 **pane 하단 IME 입력 바**를 함께 제공합니다.

권장 방식:

- 영어 / 쉘 명령: terminal 직접 입력
- 한글 문장: pane 하단 `IME` 입력 바 사용

IME 입력 바 사용법:

- `F2` 또는 `Ctrl+;` : 현재 pane의 IME 입력 바로 포커스 이동
- 한글 입력 후 `Enter` : terminal로 전송
- `Esc` : terminal로 다시 포커스 복귀
- `Send` 버튼 클릭으로도 전송 가능

---

## 기본 단축키

### 워크스페이스

| 단축키 | 동작 |
|---|---|
| `Ctrl+N` | 새 워크스페이스 |
| `Ctrl+1..8` | 워크스페이스 1..8 이동 |
| `Ctrl+9` | 마지막 워크스페이스 이동 |
| `Ctrl+Shift+W` | 워크스페이스 닫기 |
| `Ctrl+Shift+R` | 워크스페이스 이름 변경 |
| `Ctrl+B` | 사이드바 토글 |

### 서피스(탭)

| 단축키 | 동작 |
|---|---|
| `Ctrl+T` | 새 서피스 |
| `Ctrl+W` | 서피스 닫기 |
| `Ctrl+Shift+]` | 다음 서피스 |
| `Ctrl+Shift+[` | 이전 서피스 |
| `Ctrl+Tab` / `Ctrl+Shift+Tab` | 서피스 순환 |

### Pane

| 단축키 | 동작 |
|---|---|
| `Ctrl+D` | 오른쪽 분할 |
| `Ctrl+Shift+D` | 아래 분할 |
| `Ctrl+Alt+Arrow` | 인접 pane 포커스 이동 |
| `Ctrl+Shift+Z` | pane 확대 / 복귀 |
| `F2` | IME 입력 바 포커스 |
| `Ctrl+;` | IME 입력 바 포커스 |

### 생산성

| 단축키 | 동작 |
|---|---|
| `Ctrl+Shift+P` | 커맨드 팔레트 |
| `Ctrl+Shift+F` | 검색 오버레이 |
| `Ctrl+Shift+L` | 명령 로그 |
| `Ctrl+Shift+V` | Session Vault |
| `Ctrl+Alt+H` | 명령 히스토리 |
| `Ctrl+,` | 설정 |

---

## CLI 사용 예시

```powershell
# 알림 보내기
cmux notify --title "Codex" --body "작업 완료"

# 워크스페이스 관리
cmux workspace list
cmux workspace create --name "My Project"
cmux workspace select --index 0

# 서피스 / pane 동작
cmux surface create
cmux split right
cmux split down

# 상태 확인
cmux status
```

---

## 아키텍처

```text
src/
  Cmux/         WPF 데스크톱 앱 (뷰, 컨트롤, 테마)
  Cmux.Core/    터미널 엔진, 모델, 서비스, 저장, IPC
  Cmux.Cli/     자동화용 CLI
tests/
  Cmux.Tests/   단위 테스트
```

---

## 라이선스

MIT
