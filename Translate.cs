using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsherProject
{
    class Translate
    {
        private List<string> english, hebrow, column, column2, column3, hebrow2;
        public Translate()
        {
            english = new List<string>();
            hebrow = new List<string>();
            column = new List<string>();
            column2 = new List<string>();
            column3 = new List<string>();
            hebrow2 = new List<string>();
        }
        public void Add(string eng, string heb)
        {
            english.Add(eng);
            hebrow.Add(heb);
        }
        public void Add(string eng, string heb, string col)
        {
            english.Add(eng);
            hebrow.Add(heb);
            column.Add(col);
        }
        public void Add(string eng, string heb, string col, string col2, string col3, string heb2)
        {
            english.Add(eng);
            hebrow.Add(heb);
            column.Add(col);
            column2.Add(col2);
            column3.Add(col3);
            hebrow2.Add(heb2);
        }
        public string Find(string a)
        {
            return english[hebrow.IndexOf(a)];
        }
        public string FindCol(string a)
        {
            return column[hebrow.IndexOf(a)];
        }
        public string FindCol2(string a)
        {
            return column2[hebrow.IndexOf(a)];
        }
        public string FindCol3(string a)
        {
            return column3[hebrow.IndexOf(a)];
        }
        public string Findheb(string a)
        {
            return hebrow2[hebrow.IndexOf(a)];
        }
        public List<string> Returneng()
        {
            return english;
        }
        public List<string> Returnheb()
        {
            return hebrow;
        }
    }
}
