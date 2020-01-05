using System.Xml.Serialization;

namespace BookSearchBot.Models
{
    public class WorkItem
    {
        [XmlElement("best_book")]
        public BookItem BookItem { get; set; }
    }
}
