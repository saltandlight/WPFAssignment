# TIL1 - 20200116🧚‍♀️

## XML parsing
1. foreach를 이용
```C#
        private ObservableCollection<MovieModel> LoadXml(string axmlUrl)
        {
            ObservableCollection<MovieModel> results = new ObservableCollection<MovieModel>();

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(axmlUrl);

                //문서 안에 모든 속성을 가져올 수 있음
                XmlElement KeyList = xml.DocumentElement;
                XmlNodeList xnList = xml.SelectNodes("/Movies/item");


                foreach (XmlNode node1 in xnList)
                {
                    string w_title = node1["title"].InnerText;
                    string w_url = node1["url"].InnerText;

                    w_title = w_title.Replace(Environment.NewLine, string.Empty).Trim();
                    w_url = w_url.Replace(Environment.NewLine, string.Empty).Trim();

                    results.Add(new MovieModel(w_title, w_url));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return results;
        }
```
2. LinQ 이용
```C#
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
```
- LinQ가 시간이 더 걸림...유지 보수에 좋고 효과적이지만 이런 단점이 있다. 
## 2. Replace와 Trim을 사용할 때 권고사항
 1. ""은 String.Empty로 사용함
 2. "\r\n"은 `Environment.NewLine`으로 사용함

## 3. FilePath를 사용할 때 권고사항
```C#
    this.Models = this.LoadXml(Path.Combine(Environment.CurrentDirectory, "MovieModel1.xml"));
```
- 이렇게 사용하면 알아서 결합해줌
