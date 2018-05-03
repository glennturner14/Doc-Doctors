using System;
using System.Collections.Generic;
using System.Text;

namespace DocDoctors.Models
{
    public class Send
    {
        public string Message { get; set; }
        public string CommMethod { get; set; }
        public string DefaultFolder { get; set; }
        public FileRepo FileRepo { get; set; }

    }
}
