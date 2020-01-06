// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookSearchBot.API;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace BookSearchBot.Bots
{
    public class SearchBot : ActivityHandler
    {
        private readonly IApiWrapper _apiWrapper;

        public SearchBot(IApiWrapper apiWrapper) => _apiWrapper = apiWrapper;

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var input = turnContext.Activity.Text;
            var attachment = MessageFactory.Attachment(new List<Attachment>());
            var inputResponse = await _apiWrapper.GetSearchResponse(input);

            if (inputResponse != null)
            {
                var resp = $"'{input}' found {inputResponse.Search.TotalResults}. Top 3 results are:";

                var heroItems = inputResponse.Search.Results.WorkItem.Take(3);

                var heroCards = heroItems.Select(i => new HeroCard
                {
                    Images = new[] { new CardImage { Url = i.BookItem.ImageUrl } },
                    Title = i.BookItem.Title,
                    Subtitle = i.BookItem.Author.Name
                });

                attachment.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                foreach (var item in heroCards)
                {
                    attachment.Attachments.Add(item.ToAttachment());
                }

                await turnContext.SendActivityAsync(resp, cancellationToken: cancellationToken);
                await turnContext.SendActivityAsync(attachment, cancellationToken);
            }

            await WelcomeSuggestedActions(turnContext, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(CreateActivityWithTextAndSpeak($"Hello and welcome to the Book Search Bot!"), cancellationToken);
                    await WelcomeSuggestedActions(turnContext, cancellationToken); 
                }
            }
        }

        private IActivity CreateActivityWithTextAndSpeak(string message)
        {
            var activity = MessageFactory.Text(message);
            string speak = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'>" +
              $"{message}" + "</voice></speak>";
            activity.Speak = speak;
            return activity;
        }

        private static async Task WelcomeSuggestedActions(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var message = MessageFactory.Text("What book title would you like to search?");

            await turnContext.SendActivityAsync(message, cancellationToken);
        }
    }
}
