using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookSearchBot.Models
{
    public class SearchResponse
    {
        [XmlElement("total-results")]
        public int TotalResults { get; set; }

        [XmlElement("results")]
        public SearchResults Results { get; set; }
    }
}
