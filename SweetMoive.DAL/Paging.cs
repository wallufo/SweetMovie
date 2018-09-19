using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL
{
    public class Paging<T> where T:class 
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public List<T> Items { get; set; }
        public int TotalNumber;
        public Paging()
        {
            PageIndex = 1;
            PageSize = 5;
        }
    }
}
