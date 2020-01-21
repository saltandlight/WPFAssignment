# 오늘 배운 것
## 쓰레드 사용

1. Time을 사용하는 방법
```C#
  //생성자 안에서 도는 코드
  this.m_Timer = new DispatcherTimer();
  this.m_Timer.Interval = TimeSpan.FromSeconds(1);
  this.m_Timer.Tick += this.OnTick;
  this.m_Timer.Start();
  
  this.DBConnect = new DBConnect(this.UpdateResult);
```

2. Execute 함수 안에서 도는 코드
```C#
System.Windows.Application.Current.Dispatcher.Invoke(
                        (Action)(() => { }), DispatcherPriority.Background, null);
```

3. Execute 함수 안에서 도는 코드
```C#
System.Threading.Tasks.Task.Factory.StartNew(
() =>
{
   while (true)
     this.updateResult();
});
```