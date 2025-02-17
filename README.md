# 교수님이 사라졌다(Saving The Earth)
2023년 서울여자대학교 게임 개발 소학회 Dission 프로젝트<br/>

2D 픽셀 스토리 PC 게임<br/>
아포칼립스 서바이벌 RPG 과 힐링 RPG를 동시에 즐길 수 있는 어드벤처 게임<br/>
* 본 프로젝트는 서울여자대학교의 게임 개발 소학회의 프로젝트 결과물입니다.
* Unity를 처음 접하거나 사용한지 얼마 되지 않은 팀원들과, 그렇지 않은 팀원들이 모여 진행한 프로젝트로 Unity 프로젝트의 팀장이자 멘토로 참여하였습니다.

## 게임 소개
'교수님이 사라졌다(Saving The Earth)'는 기후변화로 인해 변한 세상에서 기후변화, 환경오염에 대한 경각심 함양하는 스토리 게임입니다.<br>
기후변화, 환경오염의 경각심 함양, 모험과 전투뿐만 아니라 농사의 일상적인 재미를 제공하는 것을 목적으로 하고 있습니다. <br/>

> 기후 변화로 지구의 절반은 바닷물에 잠기고, 절반은 메마른 사막이 되었다. OO대학교 환경대학원 소속 주인공은 해저 잠수함에서 지구를 되돌리기 위한 연구를 진행중이다. <br/>
> 그러던 어느 날 졸업을 앞둔 주인공에게 탐사를 떠난 지도교수님이 실종되는 사건이 발생하고, 주인공은 사라진 교수님을 찾기 위해 모험을 떠난다. 아포칼립스 세상에서 모험을 성공적으로 마무리해 무사히 졸업을 할 수 있을까?

### 게임 플레이
* 스토리 진행
  * 오브젝트와의 상호작용과 퀘스트로 스토리를 진행합니다.
  <br/><img width="50%" src="https://github.com/user-attachments/assets/61bd27a9-524b-43a2-8417-1e22b81a26c9"/>
* 몬스터와의 전투 시스템
  * 몬스터와의 전투를 통해 모험을 즐깁니다.
   <br/><img width="50%" src="https://github.com/user-attachments/assets/f3d0c62e-ce0f-476d-ad9b-37110e9d45a6"/><img width="50%" src="https://github.com/user-attachments/assets/f0d2c5ea-0157-433b-8578-89e9e9914811"/>
* 농사 시스템
  * 농사를 통해 힐링을 즐깁니다.
  <br/><img width="50%" src="https://github.com/user-attachments/assets/e59fdddb-dca4-43e2-ae49-a1c02b05a3b4"/>
## 프로젝트 개요
🔗자세한 내용은 Notion에서 확인하실 수 있으십니다.    [<img src="https://img.shields.io/badge/Notion-000000?style=flat-round&logo=Notion&logoColor=white"/>](https://www.notion.so/SavingTheEarth-178b66b96b778005b8cbe9d33e903e73?pvs=4)
### 개발 기간
* 2023.05 - 2023.11 (약 7개월)
### 개발 환경
* Unity 2021.3.5f1
### 수행업무
프로젝트 팀원은 5명으로 그 중 개발과 개발 멘토에 참여하여 다음과 같은 부분을 담당하였습니다.<br/>

타이틀 및 로딩, Player 씬, 상점 UI 제작 및 연결

세이브 시스템 제작
* Json을 활용한 세이브 파일 관리
* 직렬화가 불가능한 Dictionary를 List로 변환하여 Json 파일에 저장하도록 제작

씬 이동 및 로딩 시스템 제작
* 비동기 씬 전환 사용
* AsyncOperation를 사용한 로딩 진행 상황 표현

인벤토리 시스템 제작
* ScriptableObject를 아이템 데이터 관리
* 화면에 보여지는 퀵 슬롯, 인벤토리 내의 아이템 및 중요물품, 농사 시스템과 연결된 상자 총 4가지 슬롯으로 이루어진 인벤토리 시스템 제작
* Dictionary를 활용하여 Key 값을 아이템 ID, Value 값을 아이템 소지 클래스로 관리

아이템 및 슬롯 드래그 / 드롭 제작
* Handler 인터페이스를 사용하여 인벤토리 슬롯 및 아이템 드래그 / 드롭 제작
* Canvas의 RenderMode가 Screen Space - Camera 이므로 PointerEventData를 활용하여 아이템 위치를 월드 좌표를 고려해 마우스를 따라다니도록 제작

플레이어 카메라 및 맵 제작
* RawImage와 RenderTexture를 사용한 미니맵 및 전체 맵 제작

상점 시스템 제작
* 상점 구매하기 제작

게임 내 시간 시스템 제작
* Coroutine을 활용한 게임 내 시간 설정
* 현실 5분을 게임 내 시간 30분으로 설정 

농사 시스템 제작
* 농사 아이템에 따른 밭과 플레이어의 상호작용(밭 갈기, 물 주기, 씨 뿌리기) 제작

중간 보스 몬스터 공격 제작
* Coroutine을 활용한 중간 문어 보스 몬스터 공격 2종 제작
## 프로젝트 성과
* 2023년 서울여자대학교 디지털미디어학과 소학회 전시회 'Dimiverse' 참여
