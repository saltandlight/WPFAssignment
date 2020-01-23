# 오늘 배운 것

## 쿼리 사용 시 권고사항
- 쿼리를 이러한 방식으로 사용해왔음
`string sql = "INSERT INTO hblee_Test (T_NUM, TITLE, URL) VALUES(EX_SEQ.NEXTVAL,'"+this.Title+"', '"+ this.URL;`
- 그러나 아래와 같은 방식이 사용하기에 더 좋다...!
`string sql = string.Format("INSERT INTO hblee_Test (T_NUM, TITLE, URL) VALUES(EX_SEQ.NEXTVAL,'{0}','{1}')", this.Title, this.URL);`

## 네이밍
- #region Declare Definition 영역에는 static 변수들을 넣기(대문자로 사용할 것)
- #region Declare Field 영역에서 변수 선언을 할 때, string은 `string.Empty`로 할당
    - 변수가 쓸 데 없이 많다면 리팩토링이 필요한 순간일 수 있다.
- #region Property에서 프로퍼티 선언은 앞 글자는 무조건 대문자
- 네이밍을 할 때, 이름이 길어져도 상관 없으니 어떤 성격의 아이인지 명확하게 한 눈에 볼 수 있게 해야 함
- **어떠한 역할을 하는 변수인가? 어떠한 역할을 하는 함수인가? 어떠한 역할을 하는 커맨드인가? 를 유의하여 네이밍을 할 것**

## 리팩토링
- 수정하기 쉽도록 코드를 바꾸는 작업
- 비슷한 색을 가진 메소드가 남발되고 있다면 --> 공통적인 작업을 담당하는 코드들을 하나의 메소드로 뺼 것
- Command가 계속 남발되고 있다면 공통적인 부분을 가진 코드를 빼서 상속 관계로 만들어주기
- `using`을 사용할 수 있다면 사용할 것(Garbage Collector의 일을 줄여줌) 

## RelayCommand
