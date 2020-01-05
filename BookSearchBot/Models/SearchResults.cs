using System.Collections.Generic;
using System.Xml.Serialization;

namespace BookSearchBot.Models
{
    public class SearchResults
    {
        [XmlElement("work")]
        public List<WorkItem> WorkItem { get; set; }
    }
}
