using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL
{
    public class Search<T> where T:class
    {
        public List<T> Items { get; set; }
    }
}
