
![image](https://github.com/user-attachments/assets/15b26952-3f21-4ffc-a353-537c68cfd9a1)
![image](https://github.com/user-attachments/assets/05c5fb09-994a-4f63-a1b7-589e9b6574b0)



# 🍪 TeamProject_Team19: 쿠키런 스타일 2D 횡스크롤 러너 게임

> Unity를 활용한 2D 러너 게임 제작 프로젝트

---

## 🎮 프로젝트 소개

**“끝없이 달려 살아남자!”**  
쿠키런을 모티브로 한 **2D 횡스크롤 러너 게임**입니다.  
플레이어는 자동으로 앞으로 달리며, 장애물을 점프와 슬라이드로 피하고, 다양한 아이템을 수집해 점수를 올립니다.
과로사를 컨셉으로한 19조만의 저승런을 즐겨보세요!

- **장애물 피하기, 점수 수집, 체력 관리 등 핵심 메커니즘 구현**
- 게임 오버 시 점수 저장 및 UI 출력
- 버튼 클릭으로 **다시 시작 / 메인 화면으로 이동 가능**

---

## ✨ 주요 기능

| 기능            | 설명 |
|----------------|------|
| 자동 이동      | 플레이어가 일정 속도로 자동 전진 |
| 점프/슬라이드   | 장애물을 회피할 수 있는 입력 방식 구현 |
| 점수 시스템    | 시간 경과 및 아이템 획득에 따라 점수 증가 |
| 체력 시스템    | 시간, 충돌 등으로 체력 감소 → 0이 되면 게임 종료 |
| 아이템         | 회복, 점수 추가, 속도 변화 등 다양한 효과 |
| UI 시스템      | 점수 표시, 게임 오버 화면, 다시 시작 버튼 등 |
| BGM 및 효과음  | 배경 음악 자동 재생 및 종료 시 정지 |

---

## 🛠 개발 환경

- **Engine**: Unity 2022.3.17f1
- **IDE**: Visual Studio / JetBrains Rider
- **버전관리**: Git + GitHub Desktop
- **플랫폼**: Windows / macOS
- **언어**: C#

---

## 📁 폴더 구조
// Assets/<br/>
// ├── Scripts/<br/>
// │ ├── Player/<br/>
// │ ├── Item/<br/>
// │ ├── System/ ← GameManager, ScoreManager, UIManager<br/>
// │ └── Obstacle/<br/>
// ├── Scenes/<br/>
// │ ├── MainScene.unity<br/>
// │ └── InGameScene.unity<br/>
// ├── Prefabs/<br/>
// └── UI/<br/>


---

## 🧩 팀 구성

| 이름 | 역할 | 주요 담당 |
|------|------|------------|

| 조은서 | 플레이어 | Player 모션 기능(이동 및 점프) 구현, 체력 감소 기능 구현, 아트 담당, System 코드 리팩토링 |<br/>
| 김주원 | 장애물 및 아이템 생성 기능 구현 자석, 장애물 충돌 감지 기능 구현 |<br/>
| 박재윤 | 배경bgm |<br/>
| 최종현 | 아이템 오브젝트 제작, 씬 내 프리팹 배치 및 관리, System 코드 리팩토링 |<br/>
| 황연주 | UI 담당, 와이어프레임, System 코드 리팩토링, 발표 자료 제작 및 발표, README 작성 |<br/>

---

## 🧪 핵심 코드 트러블슈팅 예시

| 문제 | 해결 |
|------|------|
| 장애물 위/아래 위치가 동일해 ‘구멍 없음’ | `bottomObject.localPosition = new Vector3(0, -halfHoleSize)` 로 수정 |
| GameManager에서 Game Over 후 UI 미출력 | `UIManager.Instance?.ShowGameOver("Game Over")` 코드 추가로 해결 |
| 점수 UI 중복 관리 | `UIManager.cs`에 점수 업데이트 코드 통합 (SRP 원칙 반영) |

---

## 📽 시연 영상

👉 [시연 영상 보기 (YouTube 링크 등)](https://youtu.be/5oCQjq6K-Pk)

---

## 📌 기대 효과

- **Unity 2D 실무 기술 습득** (씬 전환, 이벤트, UI 연결, 싱글톤 구조 등)
- 기초 체력 향상: 팀 단위 협업, Git 활용, 이슈 관리 경험
- 간단한 2D 게임을 직접 기획~개발까지 경험

---

## 📝 License

본 프로젝트는 교육 목적으로 제작되었습니다.
