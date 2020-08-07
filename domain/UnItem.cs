using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetUnItems.domain
{
    class UnItem
    {
        public String Name { get; set; }
        public String Description { get; set; }

        public Dictionary<String, String> Props { get; set; }

    }
}
