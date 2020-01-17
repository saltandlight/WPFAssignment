using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace Movie
{
    public class MovieModel
    {
        public string Title { get; private set; }
        public string Url { get; private set; }

        public MovieModel(string aTitle, string aURL)
        {
            this.Title = aTitle;
            this.Url = aURL;
        }

        public void setTitle(string aTitle)
        {
            this.Title = aTitle;
        }
        public void setUrl(string aUrl)
        {
            this.Url = aUrl;
        }
    }
}
