using System;

namespace DocDoctors.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string  FileRepo { get; set; }
        public string DefaultFolder { get; set; }
        public string Secret1 { get; set; }
        public string Secret2 { get; set; }


    }
}
