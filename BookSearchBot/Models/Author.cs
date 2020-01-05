using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookSearchBot.Models
{
    public class Author
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
