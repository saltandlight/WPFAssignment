using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace Movie
{
    internal delegate void _SetURL(string aURL);

    public class ViewModel : INotifyPropertyChanged
    {

    #region Declare Definition

        private string m_sUrl = string.Empty;

    #endregion

    #region Declare Field
        
    #endregion

    #region Property

        public ObservableCollection<MovieModel> Models
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

        public ICommand ChangeUrlCommand { get; private set; }

        public string vTitle
        {
            get;
            private set;
        }

    #endregion

    #region Constructor && Destructor

        public ViewModel()
        {
            this.sUrl = "http://www.cgv.co.kr/";
            this.vTitle = "안녕";
            this.Models = this.LoadXml(Path.Combine(Environment.CurrentDirectory, "MovieModel1.xml"));
            this.ChangeUrlCommand = new ChangeUrlCommand(this.SetURL);
        }

    #endregion

    #region Method

        private void SetURL(string aURL)
        {
            this.sUrl = aURL;
        }

        private ObservableCollection<MovieModel> LoadXml(string axmlUrl)
        {
            ObservableCollection<MovieModel> results = null;

            try
            {
                XElement xml = XElement.Load(axmlUrl);

                results
                    = new ObservableCollection<MovieModel>(
                            xml.DescendantsAndSelf("item")
                               .Select(o => new MovieModel(o.Element("title").Value, o.Element("url").Value)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                results = new ObservableCollection<MovieModel>();
            }

            return results;
        }

        //private ObservableCollection<MovieModel> LoadXml(string axmlUrl)
        //{
        //    ObservableCollection<MovieModel> results = new ObservableCollection<MovieModel>();

        //    try
        //    {
        //        XmlDocument xml = new XmlDocument();
        //        xml.Load(axmlUrl);

        //        //문서 안에 모든 속성을 가져올 수 있음
        //        XmlElement KeyList = xml.DocumentElement;
        //        XmlNodeList xnList = xml.SelectNodes("/Movies/item");


        //        foreach (XmlNode node1 in xnList)
        //        {
        //            string w_title = node1["title"].InnerText;
        //            string w_url = node1["url"].InnerText;

        //            w_title = w_title.Replace(Environment.NewLine, string.Empty).Trim();
        //            w_url = w_url.Replace(Environment.NewLine, string.Empty).Trim();

        //            results.Add(new MovieModel(w_title, w_url));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //    return results;
        //}

    #endregion

    #region Interface : INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    }
    
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

}