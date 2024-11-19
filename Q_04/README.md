# 4번 문제

주어진 프로젝트는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### 1. Player
- 상태 패턴을 사용해 Idle 상태와 Attack 상태를 관리한다.
- Idle상태에서 Q를 누르면 Attack 상태로 진입한다
  - 진입 시 2초 이후 지정된 구형 범위 내에 있는 데미지를 입을 수 있는 적을 탐색해 데미지를 부여하고 Idle상태로 돌아온다
- 상태 머신 : 각 상태들을 관리하는 객체이며, 가장 첫번째로 입력받은 상태를 기본 상태로 설정한다.

### 2. NormalMonster
- 데미지를 입을 수 있는 몬스터

### 3. ShieldeMonster
- 데미지를 입지 않는 몬스터

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안

### 확인된 문제들

#### 1. StateAttack 에서 공격을 진행할 때 NullReferenceException 발생

- 원인
  - `Attack()` 함수에서 foreach 문으로 공격할 대상을 찾으려고 스크립트를 가져올 때 발생
- 해결
  - 모든 몬스터가 데미지를 입을 수 있으면 모르지만 데미지를 입지 않는 몬스터가 있어서 `IDamagable`가 없을 수도 있음
  - 그래서 `IDamagable`가 있을 때만 공격하게 예외처리 추가

#### 2. StateAttack에서 공격을 진행 후 Idle 상태 변환 중 StackOverflorException 발생

- 원인
  - 로그에서 Exit와 ChangeState가 계속 호출되다가 CurrentState에서 발생한다
- 해결
  - Exit 함수안에 `Machine.ChangeState(StateType.Idle);` 해당 부분이 있어서 
  - `StateAttack.Exit() <-> CurrentState.Exit()` 서로 호출해서 반복 호출하기 때문에
  - StateAttack 에서 DelayRoutine에서의 Exit 부분에 상태 변경 요청을 놓고 Exit 내용은 삭제로 해결