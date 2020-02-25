# TIL
**코딩 시 주의해야 할 점**
- 프로젝트 생성 시 빌드해서 확인할 것
- Garbage Collector는 만능이 아니다!!
- Garbage Collector를 만능으로 생각해서 메모리 생각 없이 코딩하는 경우가 있음
- 그러나 Garbage Collector는 자동으로 해주지 않을 때도 있음
- 놔뒀다가 언제 쓰레기들을 청소할 지 모르는 것임..!
- 어느 순간 갑자기 처리한다.
- Disposable 인터페이스를 상속하는 아이를 사용할 경우, Garbage Collector가 알아서 해줌
- 사용했던 리소스들을 해제시켜주는 역할
- Dispose의 사전적 의미: 대상을 없애기 위한 처리
- **Disposable 인터페이스를 상속받는 클래스만 using을 사용 가능함!!**
## using
- 리소스 정리를 위한 메소드를 만들어두어야 함
- IDisposable interface를 상속받아야 하고 리소스 정리를 위한 Dispose()를 구현해야 함 
- using이 리소스를 정리할 수 있는 Dispose 함수를 부를 때
  - using 구문이 정상적으로 종료
  - using 구문 수행 중 
