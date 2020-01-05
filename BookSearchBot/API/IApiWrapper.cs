using BookSearchBot.Models;
using System.Threading.Tasks;

namespace BookSearchBot.API
{
    public interface IApiWrapper
    {
        Task<GoodReadsResponse> GetSearchResponse(string input);

        string SearchType { get; set; }
    }
}