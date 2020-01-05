using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookSearchBot.Models
{
    [XmlRoot("GoodreadsResponse")]
    public class GoodReadsResponse
    {
        [XmlElement("search")]
        public SearchResponse Search { get; set; }
    }
}
