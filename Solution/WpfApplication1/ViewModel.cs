using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WpfApplication1
{
    public class ViewModel : INotifyPropertyChanged
    {


    #region Declare Definition

        private string m_sUrl = string.Empty;
        private string m_sTitle = string.Empty;

    #endregion

    #region Declare Field
        ObservableCollection<UrlModel> m_Models = new ObservableCollection<UrlModel>();
    #endregion

    #region Property

        public ObservableCollection<UrlModel> Models
        {
            get;
            private set;
        }

        public string sUrl
        {
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

        public string sTitle
        {
            get
            {
                return m_sTitle;
            }
            set
            {
                this.m_sTitle = value;

                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("sTitle"));
            }
        }
        public ICommand ChangeUrlCommand { get; private set; }
        
    #endregion

    #region Constructor && Destructor

        public ViewModel(int num)
        {
            this.sUrl = "http://www.cgv.co.kr/";

            

            this.m_Models.Add(new UrlModel("네이버", "https://www.naver.com"));
            this.m_Models.Add(new UrlModel("다음", "https://www.daum.net"));
            this.m_Models.Add(new UrlModel("구글", "https://www.google.com"));
            this.m_Models.Add(new UrlModel("야후", "https://www.yahoo.com/"));

            if (num == 1)
            {
                sUrl = m_Models[0].Url;
                sTitle = m_Models[0].Title;
            }
            else if (num == 2)
            {
                sUrl = m_Models[1].Url;
                sTitle = m_Models[1].Title;
            }
            else if (num == 3)
            {
                sUrl = m_Models[2].Url;
                sTitle = m_Models[2].Title;
            }
            else
            {
                sUrl = m_Models[3].Url;
                sTitle = m_Models[3].Title;
            }

        }

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
}
