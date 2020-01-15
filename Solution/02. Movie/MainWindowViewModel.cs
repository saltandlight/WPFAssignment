using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using static Movie.MainWindowViewModel;

namespace Movie
{
    delegate void _SetURL(string aURL);

    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Declare Definition
            private string m_sUrl;
        #endregion

        #region Declare Field
            private ObservableCollection<MovieModel> m_Models = new ObservableCollection<MovieModel>();
        #endregion

        #region Property
            public ObservableCollection<MovieModel> Models
            {
                get
                {
                    return this.m_Models;
                }
            }

            public string sUrl {
                get
                {
                    return m_sUrl;
                }
                set
                {
                    this.m_sUrl = value;

                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("sUrl"));
                }
            }

            public ICommand ChangeUrl { get; private set; }


        #endregion

        #region Constructor && Destructor
            public MainWindowViewModel()
            {
                this.sUrl = "http://www.cgv.co.kr/";

                this.Models.Add(new MovieModel { Title = "닥터 두리틀", Url= "http://www.cgv.co.kr/movies/detail-view/?midx=82531"});
                this.Models.Add(new MovieModel { Title = "백두산", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82747"});
                this.Models.Add(new MovieModel { Title = "스타워즈-라이브 오브 스카이워커", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82537"});
                this.Models.Add(new MovieModel { Title = "시동", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82737"});
                this.Models.Add(new MovieModel { Title = "미드웨이", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82940"});
                this.Models.Add(new MovieModel { Title = "겨울왕국2", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82014"});
                this.Models.Add(new MovieModel { Title = "나이브스 아웃", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82493"});
                this.Models.Add(new MovieModel { Title = "포드 V 페라리", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=81914" });
                this.Models.Add(new MovieModel { Title = "파바로티", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82980"});
                this.Models.Add(new MovieModel { Title = "눈의 여왕4", Url = "http://www.cgv.co.kr/movies/detail-view/?midx=82712"});

                this.ChangeUrl = new ChangeUrl(this.SetURL);
            }
        #endregion

        #region Delegate

            public delegate void TransferUrlDelegate(string str);

        #endregion
        #region Method

            private void SetURL(string aURL)
            {
                this.sUrl = aURL;
            }

        #endregion

        #region Interface : INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

        #endregion
       

    }
    
    internal class ChangeUrl : ICommand
    {
        private _SetURL m_SetURL = null;

        public ChangeUrl(_SetURL aSetURL)
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
            MovieModel mv = (MovieModel)parameter;
            this.m_SetURL(mv.Url);
        }
    }

}