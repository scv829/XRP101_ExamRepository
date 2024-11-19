# 5번 문제

주어진 프로젝트는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### 01. Main Scene
- 실행 시, Start 버튼을 누르면 게임매니저를 통해 게임 씬이 로드된다.

### 02. Game Scene
- Go to Main을 누르면 메인 씬으로 이동한다
- `+`버튼을 누르면 큐브 오브젝트의 회전 속도가 증가한다
- `-`버튼을 누르면 큐브 오브젝트의 회전 속도가 감소한다 (-가 될 경우 역방향으로 회전한다)
- Popup 버튼을 누르면 게임 오브젝트가 정지(큐브의 회전이 정지)하며, Popup창을 출력한다. 이 때 출력된 팝업창은 2초 후 자동으로 닫힌다.

### 공통 사항
- 게임 실행 중 씬 전환 시에도 큐브 오브젝트의 회전 속도는 저장되어 있어야 한다.

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안

### 확인된 문제들

#### 1. Main 씬에서 Start 버튼 눌렀을 때 반응이 없음

- 원인
  - Main씬에서 게임을 시작했을 때 Start 버튼과 상호작용이 안됨
- 해결
  - UI와 상호작용을 담당해주는 `EventSystem`이 없어서 추가로 해결

#### 2. Game 씬에서 버튼 눌렀을 때 반응이 없음

- 원인
  - Game 씬에서 버튼과 상호작용이 안됨
- 해결
  - UI와 상호작용을 담당해주는 `EventSystem`이 없어서 추가로 해결

#### 3. Popup 창이 일정 시간뒤에 안닫히는 현상

- 원인
  - Popup창이 생기고 게임 오브젝트가 저징하고 팝업창은 2초뒤에 자동으로 닫혀야 하는데 안 닫힘.
  - 팝업창 보이는 로직에서 제대로 수행이 안되는 걸로 확인
- 시행 착오
  - Popup 창이 생기고 2초뒤에 게임이 다시 진행해야하는데 Time.timeScale을 원복하는 부분이 없다.
  - 그래서 GameManager에 다시 게임을 재개하는 함수를 추가
     ```C#
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    ```
    - 이렇게 했는데 2초의 대기 시간이 지나고도 재개를 안한다.
  - 현재 대기 시간은 `Coroutine`으로 구현했다. 여기서 yield return WaitForSeconds로 되어 있는데 `WaitForSeconds은 Time.timeScale` 을 사용한다고 한다
  - 그래서 Time.timeScale이 0이여서 해당 루틴이 작동을 안했다.
- 해결
  - GameManager에 게임을 재개하는 함수를 추가하고
  - `DeactivateRoutine()`부분에서 사용하는 _wait를 `WaitForSeconds` 에서 `WaitForSecondsRealtime`로 변경해서 일정 대기 시간 뒤에 닫히게 해결했다.
  
#### 4. Popup 창이 열려도 큐브가 계속 돌아가는 현상

- 원인
  - Popup창이 생기고 큐브도 멈춰야 하는데 계속 움직임
- 해결
  - [유니티 메뉴얼](https://docs.unity3d.com/kr/2021.3/Manual/TimeFrameManagement.html)에서 찾아보니 다음과 같은 내용을 확인했다.
    - `. 이런 경우 **Update 메서드는 여전히 호출되어 있지만** Time.time은 전혀 증가하지 않으며 Time.deltaTime은 0입니다.`
  - 그래서 큐브의 회전하는 로직이 `Update`에서 구현되어 있어 게임을 일시정지(Time.timeScale = 0)해도 계속 작동을 했다.
  - 큐브의 회전하는 로직에 다음과 같이 수정을 해서 해결했다
    ```C#
    private void Update()
    {
        transform.Rotate(Vector3.up * GameManager.Intance.Score * Time.timeScale);
    }
    ```

#### 5. 큐브의 회전 속도가 저장이 안된다.

- 원인
  - 큐브의 회전 속도가 담겨진 GameManager에서 속도의 값이 저장이 안된다.
- 해결
  - GameManager 있을때는 생기지 말아야 하는데 계속 생겨서 없을 때에만 생기게 예외 처리로 해결