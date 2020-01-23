# 오늘 배운 것

## 쿼리 사용 시 권고사항
- 쿼리를 이러한 방식으로 사용해왔음<br>
```C#
string sql = "INSERT INTO hblee_Test (T_NUM, TITLE, URL) VALUES(EX_SEQ.NEXTVAL,'"+this.Title+"', '"+ this.URL;
```
- 그러나 아래와 같은 방식이 사용하기에 더 좋다...!<br>
```C#
string sql = string.Format("INSERT INTO hblee_Test (T_NUM, TITLE, URL) VALUES(EX_SEQ.NEXTVAL,'{0}','{1}')", this.Title, this.URL);
```

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

## ICommand
- ICommand 사용 시 다음과 같은 Command를 만들 수 있음
```C#
 internal class ChangeUrlCommand : ICommand
 {
        private _SetURL m_SetURL = null;

        public ChangeUrlCommand(_SetURL aSetURL)
        {
            this.m_SetURL = aSetURL;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MovieModel mv = parameter as MovieModel;
            if (mv != null)
                this.m_SetURL(mv.Url);
        }
 }
```
- 버뜨... 각 명령마다 이런 식으로 새로 구현해야 한다는 단점이 있다...
- 이런 문제를 해결하고 싶어서 실행될 Action들을 인스턴스화할 수 있는 RelayCommand가 등장함..!

## RelayCommand
```C#
 internal class RelayCommad : ICommand
    {
        private readonly Action m_Execute = null;
        private readonly Func<bool> m_CanExecute = null;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.m_CanExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (this.m_CanExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommad(Action aExecute, Func<bool> aCanExecute = null)
        {
            if (aExecute == null)
                throw new ArgumentNullException("Execute");

            this.m_Execute = aExecute;
            this.m_CanExecute = aCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.m_CanExecute == null)
                return true;
            else
                return this.m_CanExecute();
        }

        public void Execute(object parameter)
        {
            this.m_Execute();
        }
    }
```
- 솔직히 뭔 소리인지 모르겠다.
- 이걸 사용하면 실행할 항목을 정할 수 있어서 각 작업마다 클래스를 만들 필요가 없음..!
- 자바와 다르게 Action 과 Function을 변수로 사용한다!
```C#
    this.Models = new ObservableCollection<MovieModel>(this.SelectResult());
    this.SelectCommand = new RelayCommad<MovieModel>(this.Clear);
    //InsertResult는 Title과 URL이 둘 다 비어있지 않을 때 명령이 먹힐 수 있는 것!
    this.InsertCommand = new RelayCommad(this.InsertResult, () => !string.IsNullOrEmpty(this.Title) || !string.IsNullOrEmpty(this.URL));
    //UpdateResult는 aModel이 변수로 사용되는데 aModel의 Title과 Url이 비어있지 않을 때 명령이 먹힐 수 있는 것!
    this.UpdateCommand = new RelayCommad<MovieModel>(this.UpdateResult, aModel => !string.IsNullOrEmpty(this.Title) || !string.IsNullOrEmpty(this.URL));
    this.DeleteCommand = new RelayCommad<MovieModel>(this.DeleteResult);
```

- 참고한 사이트:https://www.c-sharpcorner.com/UploadFile/20c06b/icommand-and-relaycommand-in-wpf/