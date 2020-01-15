using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {

    #region Declare Definition

    #endregion

    #region Declare Field

        private string m_Title = string.Empty;
        private string m_InputText = string.Empty;
        private string m_ResultText = string.Empty;

    #endregion

    #region Property

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            private set
            {
                this.m_Title = value;

                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }
        
        public string InputText
        {
            get
            {
                return this.m_InputText;
            }
            set
            {
                if (this.m_InputText != value)
                {
                    this.m_InputText = value;
                    this.m_ResultText = this.m_InputText;

                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("InputText"));
                }
            }
        }

        public string Opert { get; set; }
        public double Oper1 { get; set; }
        public double Oper2 { get; set; }
        public double Result { get; set; }
        public int Sign { get; set; }
        
        public ICommand OperandInput { get; private set; }
        public ICommand OperatorInput { get; private set; }
        public ICommand CInput { get; private set; }
        public ICommand CEInput { get; private set; }
        public ICommand SignInput { get; private set; }
        public ICommand BackInput { get; private set; }
        public ICommand GetResult { get; private set; }

    #endregion

    #region Constructor && Destructor

        public MainWindowViewModel()
        {
            this.Title = "Calculator";

            this.OperandInput = new OperandInput(this);
            this.OperatorInput = new OperatorInput(this);
            this.SignInput = new SignInput(this);
            this.CInput = new CInput(this);
            this.CEInput = new CEInput(this);
            this.BackInput = new BackInput(this);
            this.GetResult = new GetResult(this);
        }

    #endregion

    #region Method

    #endregion

    #region Interface : INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
     
    #endregion

    }

    internal abstract class CommonCommand : ICommand
    {
        protected MainWindowViewModel ViewModel { get; private set; }

        public CommonCommand(MainWindowViewModel aViewModel)
        {
            if (aViewModel == null)
                throw new ArgumentNullException("ViewModel");

            this.ViewModel = aViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
    }


    internal class BackInput : CommonCommand
    {
        public BackInput(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {

        }

        public override void Execute(object parameter)
        {
            string text = base.ViewModel.InputText;

            if (text.Length == 1)
            { 
                base.ViewModel.InputText = "0";
            }
            else
            {
                text = text.Substring(0, text.Length - 1);
                base.ViewModel.InputText = text;
            }
        }
    }

    internal class SignInput : CommonCommand
    {
        public SignInput(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {

        }

        public override void Execute(object parameter)
        {
            string text2 = string.Empty;

            this.ViewModel.Sign *= -1;

            if (this.ViewModel.Sign == -1)
            { 
                this.ViewModel.InputText = "-" + this.ViewModel.InputText;
            }
            else
            {
                text2 = this.ViewModel.InputText.Replace('-', ' ');
                this.ViewModel.InputText = text2.Trim();
            }
        }
    }

    internal class GetResult : CommonCommand
    {
        public GetResult(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {
            
        }

        public override void Execute(object parameter)
        {
            //여기서는 연산자들을 switch로 분류해서 각각을 처리한 결과를 show하자!
            this.ViewModel.InputText += "=";

            string str_startIndex = this.ViewModel.Opert;
            string str_endIndex = "=";
            int startIndex = this.ViewModel.InputText.IndexOf(str_startIndex);
            int endIndex = this.ViewModel.InputText.IndexOf(str_endIndex);
            string test = this.ViewModel.InputText.Substring(startIndex + 1, endIndex-startIndex-1);
            
            //Oper2가 할당됨
            this.ViewModel.Oper2 = Convert.ToDouble(test);

            bool flag = false;

            switch (this.ViewModel.Opert)
            {
                case "+":
                    this.ViewModel.Result = this.ViewModel.Oper1 + this.ViewModel.Oper2;
                    break;

                case "-":
                    this.ViewModel.Result = this.ViewModel.Oper1 - this.ViewModel.Oper2;
                    break;

                case "x":
                    this.ViewModel.Result = this.ViewModel.Oper1 * this.ViewModel.Oper2;
                    break;

                case "÷":
                    if (this.ViewModel.Oper2 != 0)
                        this.ViewModel.Result = this.ViewModel.Oper1 / this.ViewModel.Oper2;
                    else
                        flag = true;
                    break;
            }

            if (flag)
            {
                this.ViewModel.InputText = "0으로 나눌 수 없습니다.";
                this.ViewModel.Oper1 = 0.0f;
                this.ViewModel.Oper2 = 0.0f;
                this.ViewModel.Opert = string.Empty;
            }
            else
            {
                if (this.ViewModel.Result < 0)
                    this.ViewModel.Sign = -1;

                this.ViewModel.InputText = this.ViewModel.Result + "";

                double val;
                if (double.TryParse(this.ViewModel.InputText, out val))
                    this.ViewModel.Oper1 = val;
                else
                    this.ViewModel.Oper1 = 0;

                this.ViewModel.Oper2 = 0.0f;
                this.ViewModel.Opert = string.Empty;
            }
        }
    }


    internal class CEInput : CommonCommand
    {
        public CEInput(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {
            
        }

        public override void Execute(object parameter)
        {
            base.ViewModel.InputText = string.Empty;
        }
    }

    internal class OperandInput : CommonCommand
    {
        public OperandInput(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {
            
        }

        public override void Execute(object parameter)
        {
            if (base.ViewModel.InputText == "0")
                base.ViewModel.InputText = string.Empty;

            base.ViewModel.InputText += parameter;
        }
    }

    internal class OperatorInput : CommonCommand
    {
        public OperatorInput(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {
            
        }

        public override void Execute(object parameter)
        {
            this.ViewModel.Opert = parameter != null ? parameter.ToString() : string.Empty;
            this.ViewModel.Oper1 = Convert.ToDouble(this.ViewModel.InputText);
            this.ViewModel.InputText +=parameter;
        }
    }

    internal class CInput : CommonCommand
    {
        public CInput(MainWindowViewModel mainWindowViewModel)
            : base(mainWindowViewModel)
        {

        }

        public override void Execute(object parameter)
        {
            this.ViewModel.Opert = string.Empty;
            this.ViewModel.Oper1 = 0.0f;
            this.ViewModel.Oper2 = 0.0f;
            this.ViewModel.InputText = string.Empty;
        }
    }
}
