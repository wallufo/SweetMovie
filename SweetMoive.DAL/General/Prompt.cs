using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetMoive.DAL.General
{
    public class Prompt
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public List<string> Buttons { get; set; }
    }
}
