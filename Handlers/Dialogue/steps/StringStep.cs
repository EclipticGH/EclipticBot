using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Voice_Commands.Handlers.Dialogue.steps
{
    public class StringStep : DialogueBaseStep
    {
        private readonly IDialogueStep _nextStep;
        private readonly int? _minLength;
        private readonly int? _maxLength;

        public StringStep(
            string content,
            IDialogueStep nextStep,
            int? minLength = null,
            int? maxLength = null) : base(content)
        {
            _nextStep = nextStep;
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public Action<string> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Menu",
                Description = $"{user.Mention}, {_content}"
            };
            embedBuilder.AddField("Too stop the menu", "Do ?cancel");

            if (_minLength.HasValue)
            {
                embedBuilder.AddField("Min Length:", $"{_minLength.Value} characters");
            }
            if (_maxLength.HasValue)
            {
                embedBuilder.AddField("Min Length:", $"{_maxLength.Value} characters");
            }

            var interactivity = client.GetInteractivity();

            while (true)
            {
                var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

                OnMessageAdded(embed);
                var messsageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id & x.Author.Id == user.Id).ConfigureAwait(false);

                OnMessageAdded(messsageResult.Result);

                if(messsageResult.Result.Content.Equals("?cancel", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (_minLength.HasValue)
                {
                    if(messsageResult.Result.Content.Length < _minLength)
                    {
                        await TryAgain(channel, $"Your input is {_minLength - messsageResult.Result.Content.Length} characters too short").ConfigureAwait(false);
                        continue;
                    }
                }
                if (_maxLength.HasValue)
                {
                    if(messsageResult.Result.Content.Length > _maxLength.Value)
                    {
                        await TryAgain(channel, $"Your input is {messsageResult.Result.Content.Length - _maxLength.Value} characters too long").ConfigureAwait(false);
                        continue;
                    }
                }

                OnValidResult(messsageResult.Result.Content);

                return false;
            }
        }
    }
}
