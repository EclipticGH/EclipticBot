using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using System;
using DSharpPlus.Interactivity;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Linq;
using System.Collections.Generic;
using Voice_Commands.Handlers.Dialogue.steps;
using Voice_Commands.Handlers.Dialogue;
using System.Diagnostics;

namespace Voice_Commands.Commands
{
    public class UserCommands : BaseCommandModule
    {
        public object Discord { get; private set; }
        public object Context { get; private set; }
        public DiscordMessage Message { get; }
        public TimeSpan Elapsed { get; }
        public string StreamUrl { get; private set; }
        public int totalMSG { get; private set; }
        public DiscordUser user { get; private set; }
        public static string mention { get; private set; }
        public static string guilds { get; private set; }

        [Command("multiply"), Hidden()]
        public async Task multiply(CommandContext ctx, decimal numberOne, decimal numberTwo)
        {
            await ctx.TriggerTypingAsync();
            await ctx.Channel
                .SendMessageAsync((numberOne * numberTwo).ToString())
                .ConfigureAwait(false);
            Console.WriteLine($"{ctx.User.Mention} USED MULTIPLY");
        }

        [Command("divide"), Hidden()]
        public async Task divide(CommandContext ctx, decimal numberOne, decimal numberTwo)
        {
            try
            {
                await ctx.TriggerTypingAsync();
                await ctx.Channel
                    .SendMessageAsync((numberOne / numberTwo).ToString())
                    .ConfigureAwait(false);
                Console.WriteLine("{ctx.User.Mention} USED DEVIDE");
            }
            catch (Exception)
            {
                var emoji = DiscordEmoji.FromName(ctx.Client, ":-1:");
                await ctx.RespondAsync(emoji);
            }
        }

        [Description("too use this command is simple for example\n``e.subtract 2 2``\n``Output: 4``")]
        [Command("subtract"), Hidden()]
        public async Task subtract(CommandContext ctx, decimal numberOne, decimal numberTwo)
        {
            await ctx.TriggerTypingAsync();
            await ctx.Channel
                .SendMessageAsync((numberOne - numberTwo).ToString())
                .ConfigureAwait(false);
            Console.WriteLine($"{ctx.User.Mention} USED SUBTRACT");
        }
        [Command("add")]
        public async Task add(CommandContext ctx, decimal numberOne, decimal numberTwo)
        {
            await ctx.TriggerTypingAsync();
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
            Console.WriteLine($"{ctx.User.Mention} USED SUBTRACT");
        }
        [Description("Places you can contact me at")]
        [Command("contact")]
        public async Task contact(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"{ctx.Member.Mention} *Please check your dms*").ConfigureAwait(false);
            await Task.Delay(500);
            await ctx.Member
            .SendMessageAsync($"{ctx.User.Mention}, You can contact me via email or discord at \ndan.popach@gmail.com\nEcliptic#8328")
            .ConfigureAwait(false);
            Console.WriteLine($"{ctx.User.Mention} ASKED A QUESTION");
        }

        [Command("serverconnection"), RequireOwner(), Description("Shows the bots ping")]
        public async Task serverconnection(CommandContext Ctx)
        {
            await Ctx.TriggerTypingAsync();
            await Ctx.RespondAsync($"Ping: {Ctx.Client.Ping}");
        }
        [Description("simple it rolls a coin and lands either heads or tails both have a 50% chance of landing")]
        [Command("roll")]
        public async Task roll(CommandContext ctx)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 4);
            if (month == 1)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"heads").ConfigureAwait(false);
            }
            if (month == 2)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"heads").ConfigureAwait(false);
            }
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"tails").ConfigureAwait(false);
            }
            if (month == 4)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"tails").ConfigureAwait(false);
            }
            else
            {
                return;
            };
        }
        [Command("join"), Hidden()]
        public async Task Join(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("https://discord.gg/j47KMFC");
        }
        [RequirePermissions(Permissions.Administrator)]
        [Command("leave")]
        public async Task leave(CommandContext ctx)
        {
            try
            {
                await ctx.RespondAsync("Leaving server...");
                await ctx.Guild.LeaveAsync();
            }
            catch(Exception e)
            {
                await ctx.RespondAsync(e.Message);
            }
        }
        [Command("restart"), Description("restarts the bots connection\nonly @Ecliptic(Owner) can use this command!"), RequireOwner()]
        public async Task restart(CommandContext ctx)
        {
            try
            {
                await ctx.RespondAsync("``Output: Restarting Bot.``");
                await ctx.Client.ReconnectAsync();
                await ctx.RespondAsync("``Output: Restart Complete``");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Hidden]
        [Command("insult")]
        public async Task Insult(CommandContext ctx, [RemainingText] string t)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 15);
            try
            {
                if (month == 1)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is not gay\nbut the person that activated this command is gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 2)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is not gay\nbut the person that activated this command is gay ")
                        .ConfigureAwait(false);
                }
                if (month == 3)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 4)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is gay\nand so is the person that has activated this command ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 5)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is ultra gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 6)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is ultra gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 7)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is ultra gay ")
                        .ConfigureAwait(false);

                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 8)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is MEGA gay \nhttps://tenor.com/view/sike-you-thought-gif-13160947 ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 9)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is MEGA gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 10)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is MEGA gay \nhttps://tenor.com/view/sike-you-thought-gif-13160947 ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 11)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is too gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 12)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is too gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 13)
                {
                    await ctx.Channel
                        .SendMessageAsync(t + " is too gay ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 14)
                {
                    await ctx.Channel
                   .SendMessageAsync(" no ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
                if (month == 15)
                {

                    await ctx.Channel
                        .SendMessageAsync(" no ")
                        .ConfigureAwait(false);
                    Console.WriteLine($"{ctx.User.Mention}" + " " + month);
                }
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Hidden]
        [Command("insultme")]
        public async Task Insultme(CommandContext ctx, [RemainingText] string t)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 11);
            if (month == 1)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is not gay").ConfigureAwait(false);
            }
            if (month == 2)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is mega gay").ConfigureAwait(false);
            }
            if (month == 3)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is too gay").ConfigureAwait(false);
            }
            if (month == 4)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is gay").ConfigureAwait(false);
            }
            if (month == 5)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is mega gay").ConfigureAwait(false);
            }
            if (month == 6)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is mega gay").ConfigureAwait(false);
            }
            if (month == 7)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is ULTRA gay").ConfigureAwait(false);
            }
            if (month == 8)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} no").ConfigureAwait(false);
            }
            if (month == 9)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is MEGA gay \nhttps://tenor.com/view/sike-you-thought-gif-13160947\n").ConfigureAwait(false);
            }
            if (month == 10)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is MEGA gay \nhttps://tenor.com/view/sike-you-thought-gif-13160947\n").ConfigureAwait(false);
            }
            if (month == 11)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{ctx.User.Mention} is not gay").ConfigureAwait(false);
            }

        }
        //admin commands
        [Command("stop"), Description("disables the bot"), RequireOwner()]
        public async Task stop(CommandContext ctx)
        {
            await ctx.Client.DisconnectAsync();
        }
        [Command("Owner_Help"), Description("shows help commands for the owner(@Ecliptic)"), RequireOwner()]
        public async Task Owner_Help(CommandContext ctx)
        {
            await ctx.RespondAsync("``Restart, Stop, Owner_Help``");
        }
        //Guild Admin/Mod/Owner/user Commands
        [Command("invitebot")]
        public async Task Invitebot(CommandContext ctx)
        {
            await ctx.RespondAsync($"https://discord.com/oauth2/authorize?client_id=703353151771902012&scope=bot&permissions=8");
        }
        [Command("ban"), Description("Bans a user"), RequirePermissions(Permissions.BanMembers)]
        public async Task ban(CommandContext ctx, DiscordMember user, [RemainingText]string reason = "not specified")
        {
            try
            {
                await user.BanAsync();
                await ctx.RespondAsync($"{user.Mention} has been banned");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                embedBuilder.AddField("Possible solution: ", "Make sure that eclipticbots role is above the role of the person that youre trying to ban.", true);
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("unban"), Description("unbans a user"), RequirePermissions(Permissions.BanMembers)]
        public async Task unban(CommandContext ctx, DiscordMember mbr)
        {
            try
            {
                await ctx.Guild.UnbanMemberAsync(mbr);
                await ctx.RespondAsync($"{mbr.Mention} has been unbaned");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("deletechannel"), Aliases("dc"), Description("Deletes a channel"), RequirePermissions(Permissions.ManageChannels)]
        public async Task deletechannel(CommandContext ctx, DiscordChannel channel)
        {
            await ctx.Guild.Owner.SendMessageAsync($"{ctx.Member.Mention} has created a channel called: " + channel).ConfigureAwait(false);
            await channel.DeleteAsync();
            await ctx.RespondAsync($"`` The Channel {channel} has been Deleted``");
        }
        [Command("clonechannel"), Aliases("cc"), Description("Clones a channel"), RequirePermissions(Permissions.ManageChannels)]
        public async Task clonechannel(CommandContext ctx, DiscordChannel channel)
        {
            await ctx.Guild.Owner.SendMessageAsync($"{ctx.Member.Mention} has cloned a channel called: " + channel).ConfigureAwait(false);
            await channel.CloneAsync();
            await ctx.RespondAsync($"``The Channel {channel} cloned``");
        }
        [Command("createtxt"), Aliases("ct"), Description("Creates a text channel"), RequirePermissions(Permissions.ManageChannels)]
        public async Task createtxt(CommandContext ctx, string name, DiscordChannel channel = null)
        {
            await ctx.Guild.Owner.SendMessageAsync($"{ctx.Member.Mention} has created a channel called: " + name).ConfigureAwait(false);
            await ctx.Guild.CreateTextChannelAsync(name);
            await ctx.RespondAsync($"``The Channel {name} has been Created``");
        }
        [Command("createvc"), Aliases("cvc"), Description("Creates a voice channel"), RequirePermissions(Permissions.ManageChannels)]
        public async Task createvc(CommandContext ctx, string name)
        {
            try
            {
                await ctx.Guild.Owner.SendMessageAsync($"{ctx.Member.Mention} has created a channel called: " + name).ConfigureAwait(false);
                await ctx.Guild.CreateVoiceChannelAsync(name);
                await ctx.RespondAsync($"``The Channel {name} has been Created``");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("poll"), Description("Creates a poll")]
        public async Task poll(CommandContext ctx, string title, TimeSpan time, params DiscordEmoji[] emojis)
        {
            var interactivity = ctx.Client.GetInteractivity();
            await ctx.TriggerTypingAsync();
            var options = emojis.Select(x => x.ToString());

            var PollEmbed = new DiscordEmbedBuilder
            {
                Title = title,
                Color = DiscordColor.Red,
                ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                Timestamp = DateTimeOffset.Now,
                Description = $"Poll created by {ctx.User.Mention}"
            };
            var pollMessage = await ctx.Channel.SendMessageAsync(embed: PollEmbed).ConfigureAwait(false);

            foreach (var option in emojis)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, time).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }
        [Command("giveaway"), Hidden()]
        public async Task giveaway(CommandContext ctx, string title, TimeSpan time, params DiscordEmoji[] emojis)
        {
            var options = emojis.Select(x => x.ToString());

            var giveawayembed = new DiscordEmbedBuilder
            {
                Title = title,
                Description = $"Giveaway started by {ctx.Member.Mention}",
                Timestamp = DateTimeOffset.Now,
                Color = DiscordColor.Green,
                ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl
            };

            var giveaways = await ctx.Channel.SendMessageAsync(embed: giveawayembed).ConfigureAwait(false);

            foreach (var reactions in emojis)
            {
                await giveaways.CreateReactionAsync(reactions).ConfigureAwait(false);
            }
        }
        [Command("adminpresence"), Hidden(), RequireOwner()]
        public async Task adminpresence(CommandContext ctx, [RemainingText]string status)
        {
            Bot.streamURL = status;
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder { Title = "Console: ", Description = "Allowing.", Timestamp = DateTimeOffset.Now });
        }

        [Command("whois"), Aliases("wi"), Description("Shows stats of a users"), RequirePermissions(Permissions.SendMessages)]
        public async Task whois(CommandContext ctx, DiscordUser user, [RemainingText] string t)
        {
            try
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = user.Username,
                    Description = $"Account created at {user.CreationTimestamp}\nIs bot = {user.IsBot}\nFull username {user.Username}#{user.Discriminator}\nUsers Presence: {user.Presence.Status}\nUsers Activities: {user.Presence.Activities.Count}",
                    Color = DiscordColor.Orange,
                    ThumbnailUrl = user.AvatarUrl,
                    Timestamp = DateTimeOffset.Now,
                };
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync(embed: embedBuilder);
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("vcmute"), Aliases("vcm"), Description("Mutes a user"), RequirePermissions(Permissions.MuteMembers)]
        public async Task mute(CommandContext ctx, DiscordMember mbr, TimeSpan time, [RemainingText]string t)
        {
            try
            {
                await mbr.SetMuteAsync(true);
                await ctx.RespondAsync($"{mbr.Mention} has been muted");
                await Task.Delay(time);
                await mbr.SetMuteAsync(false);
                await ctx.RespondAsync($"{mbr.Mention} is now unmuted");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("vcunmute"), Description("Unmutes a user"), RequirePermissions(Permissions.MuteMembers)]
        public async Task unmute(CommandContext ctx, DiscordMember mbr, string reason = "Reason not provided")
        {
            try
            {
                await mbr.SetMuteAsync(false, reason);
                await ctx.RespondAsync($"{mbr.Mention} has been unmuted");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("kick"), Aliases("k"), Description("Kicks a user"), RequirePermissions(Permissions.KickMembers)]
        public async Task Kick(CommandContext ctx, DiscordMember mbr, string reason = "Reason not provided")
        {
            try
            {
                await mbr.RemoveAsync();
                await ctx.RespondAsync($"{mbr.Mention} has been kicked");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("Ping")]
        public async Task ping(CommandContext ctx, [RemainingText] string t)
        {
            await ctx.RespondAsync($"Pong!");
        }
        [Command("move"), Description("Moves user to another vc channel"), RequirePermissions(Permissions.MoveMembers)]
        public async Task move(CommandContext ctx, DiscordMember mbr, DiscordChannel channel, [RemainingText] string t)
        {
            await mbr.PlaceInAsync(channel);
            await ctx.RespondAsync($"{mbr.Mention} moved to {channel.Mention}");
        }
        [Command("membercount"), Description("Shows member count")]
        public async Task usercount(CommandContext ctx)
        {
            await ctx.RespondAsync($"Total members: {ctx.Guild.MemberCount}");
        }
        [Command("poke"), Description("Pokes a user"), Cooldown(1, 60, CooldownBucketType.User)]
        public async Task poke(CommandContext ctx, [Description("The user you want too poke")] DiscordMember mbr, [RemainingText] string t)
        {
            try
            {

                await mbr.SendMessageAsync($"{ctx.Member.Mention} has poked you").ConfigureAwait(false);
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"{mbr.Username} has been poked");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("clearreact"), Aliases("cr"), Description("Clears reactions"), RequirePermissions(Permissions.ManageMessages)]
        public async Task clear([Description("prefix")]CommandContext ctx, [Description("msg link")] DiscordMessage msg, [Description("remaining text will be null"), RemainingText] string t)
        {
            try
            {
                await msg.DeleteAllReactionsAsync();
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync($"Reactions cleared");
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("version"), Aliases("vn"), Description("Shows d#+ version")]
        public async Task shards(CommandContext ctx)
        {
            try
            {
                await ctx.TriggerTypingAsync();
                await ctx.Channel.SendMessageAsync($"{ctx.Client.VersionString}").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("txtunmute"), Aliases("tun"), Description("unmutes someone in chat"), RequirePermissions(Permissions.ManageMessages)]
        public async Task txtunmute(CommandContext ctx, DiscordMember usr, [RemainingText]string t = null)
        {
            mention = usr.Mention;
            mention = "daddy";
            await ctx.RespondAsync($"{usr.Mention} has been unmuted");
        }
            [Command("txtmute"), Aliases("tm"), Description("mutes someone in chat"), RequirePermissions(Permissions.ManageMessages)]
        public async Task txtmute(CommandContext ctx, DiscordMember usr, TimeSpan time, [RemainingText]string t = null)
        {
                 mention = usr.Mention; //sets user
                guilds = ctx.Guild.Name; //sets the guild
                if (mention == "<@!560594792774500379>")
                {
                    await ctx.Channel.SendMessageAsync("Cannot mute <@560594792774500379>").ConfigureAwait(false);
                    return;
                }
                if (mention == "<@!703353151771902012>")
                {
                    await ctx.Channel.SendMessageAsync("Cannot mute <@703353151771902012>").ConfigureAwait(false);
                    return;
                }
                else
                {
                    var command = await ctx.Guild.CreateRoleAsync("mute", Permissions.None, DiscordColor.Red);
                    await usr.GrantRoleAsync(command);
                    await ctx.RespondAsync($"{usr.Mention} is now muted");
                    await Task.Delay(time);
                    await usr.RevokeRoleAsync(command);
                        await command.DeleteAsync();
                     mention = "hi";
                    await ctx.RespondAsync($"The user {usr.Mention} has been unmuted");
                }
        }
        [Command("send"), Description("Sends a message to a channel"), RequireOwner()]
        public async Task send(CommandContext ctx, DiscordChannel channel, [RemainingText]string t)
        {
            try
            {
                await ctx.Message.DeleteAsync();
                await channel.SendMessageAsync(t).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }

        [Command("copymsg"), Aliases("cmsg"), RequirePermissions(Permissions.ManageMessages)]
        public async Task copyMsg(CommandContext ctx, DiscordMessage msg, [RemainingText]string t)
        {
            try
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "Message Contents:",
                    Description = $"{msg.Content}",
                    Color = DiscordColor.Green,
                    Timestamp = DateTimeOffset.Now,
                };
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync(embed: embedBuilder);
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("remindme"), Aliases("rm"), Description("creates a reminder for you :)")]
        public async Task remindMe(CommandContext ctx, [Description("How long till the bot notifys you")]TimeSpan time, [Description("the title of the reminder"), RemainingText]string reminder)
        {
            try
            {
                var emote = DiscordEmoji.FromName(ctx.Client, ":+1:");
                await ctx.Message.CreateReactionAsync(emote);
                await ctx.RespondAsync("Reminder set!");
                await Task.Delay(time);
                await ctx.Channel.SendMessageAsync($"{ctx.Message.Author.Mention} Here's your reminder that you've asked for!   `` {reminder}``").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR...",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("pfp"), Description("shows the users pfp (profile picture)")]
        public async Task pfp(CommandContext ctx, DiscordUser user)
        {
            try
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"{user.Username}",
                    ImageUrl = $"{user.AvatarUrl}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync(e.Message);
            };
        }
        [Command("auditlog"), RequireOwner(), Aliases("adminal"), RequirePermissions(Permissions.ViewAuditLog), Hidden()]
        public async Task auditlog(CommandContext ctx, int total)
        {
            try
            {
            }
            catch(Exception e)
            {
                await ctx.RespondAsync(e.Message);
            };
        }
        [Command("menu")]
        public async Task menu(CommandContext ctx)
        {
            try
            {
            }
            catch (Exception e)
            {
                await ctx.RespondAsync(e.Message);
            };
        }
        [Command("murder"), Description("murders a user"),Hidden()]
        public async Task murder(CommandContext ctx, [Description("The user you want too murder")] DiscordMember mbr, [RemainingText] string t)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 17);
            try
            {
                if(ctx.Member.Mention == mbr.Mention)
                {
                    Random yes = new Random();
                    int day = yes.Next(1, 4);
                    if (day == 1)
                    {
                        await ctx.RespondAsync($"{ctx.Member.Mention} tried to swim in lava");
                    }
                    if(day == 2)
                    {
                        await ctx.RespondAsync($"{ctx.Member.Mention} fell out of the world");
                    }
                    if (day == 3)
                    {
                        await ctx.RespondAsync($"{ctx.Member.Mention} drowned");
                    }
                    if (day == 4)
                    {
                        await ctx.RespondAsync($"{ctx.Member.Mention} fell from a high place");
                    }
                    Console.WriteLine("Task.Completed() " + day);
                    return;
                }
                if (month == 1)
                {

                    await ctx.Channel
                        .SendMessageAsync($"{mbr.Mention} has been water boarded by {ctx.Member.Mention} ")
                        .ConfigureAwait(false);
                }
                if (month == 2)
                {
                    await ctx.Channel
                       .SendMessageAsync($"{mbr.Mention} has been water boarded by {ctx.Member.Mention} ")
                       .ConfigureAwait(false);
                }
                if (month == 3)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} broke {mbr.Mention}'s neck")
                        .ConfigureAwait(false);
                }
                if (month == 4)
                {
                    await ctx.Channel
                       .SendMessageAsync($"{ctx.Member.Mention} broke {mbr.Mention}'s neck")
                       .ConfigureAwait(false);
                }
                if(month == 5)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} has stabbed {mbr.Mention} 28 times with a kitchen knife")
                        .ConfigureAwait(false);
                }
                if(month == 6)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} has stabbed {mbr.Mention} 28 times with a kitchen knife")
                        .ConfigureAwait(false);
                }
                if(month == 7)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} has stabbed {mbr.Mention} 28 times with a kitchen knife")
                        .ConfigureAwait(false);
                }
                if(month == 8)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} shot {mbr.Mention}")
                        .ConfigureAwait(false);
                }
                if(month == 9)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} shot {mbr.Mention}")
                        .ConfigureAwait(false);
                }
                if(month == 10)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} force fed {mbr.Mention} lighter fluid")
                        .ConfigureAwait(false);
                }
                if (month == 11)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} force fed {mbr.Mention} lighter fluid")
                        .ConfigureAwait(false);
                }
                if(month == 12)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} pushed {mbr.Mention} into incoming traffic")
                        .ConfigureAwait(false);
                }
                if (month == 13)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} pushed {mbr.Mention} into incoming traffic")
                        .ConfigureAwait(false);
                }
                if(month == 14)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} pushed {mbr.Mention} off a building")
                        .ConfigureAwait(false);
                }
                if(month == 15)
                {
                    await ctx.Channel
                         .SendMessageAsync($"{ctx.Member.Mention} pushed {mbr.Mention} off a building")
                         .ConfigureAwait(false);
                }
                if(month == 16)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} pushed {mbr.Mention}'s head into an oven")
                        .ConfigureAwait(false);
                }
                if (month == 17)
                {
                    await ctx.Channel
                        .SendMessageAsync($"{ctx.Member.Mention} pushed {mbr.Mention}'s head into an oven")
                        .ConfigureAwait(false);
                }
                Console.WriteLine("Task.Completed() " + month);
            }
            catch (Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("maintenance"),Aliases("mmode"),RequireOwner()]
        public async Task Output(CommandContext ctx, TimeSpan time, [RemainingText]string reason)
        {
            try
            {
                await ctx.Message.DeleteAsync();
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "``Bot Offline``",
                    Description = $"``-``Bot is currently offline because: \n> {reason}\n``-``DownTime: {time.TotalMinutes} Minutes",
                    Color = DiscordColor.Red
                };
               var yes = await ctx.RespondAsync(embed: embedBuilder);
                await ctx.Client.DisconnectAsync();
                await Task.Delay(time);
                await ctx.Client.ConnectAsync();
                await ctx.Channel.DeleteMessageAsync(yes);
                var done = new DiscordEmbedBuilder
                {
                    Title = "``Bot Back Online``",
                    Color = DiscordColor.Green
                };
                await ctx.RespondAsync(embed: done);
                
            }
            catch(Exception e)
            {
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = "ERROR",
                    Description = $"{e.Message}",
                    Color = DiscordColor.Red,
                };
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }
        [Command("serverstats"),Description("Shows the guilds stats")]
        public async Task serverStats(CommandContext ctx,[RemainingText]string t)
        {
            try
            {
                var yes = ctx.Guild.GetPruneCountAsync().Result;
                await ctx.RespondAsync($"``Guild created at {ctx.Guild.CreationTimestamp}`` **(UTC TIME)**");
                await ctx.RespondAsync($"``Guild total members at {ctx.Guild.MemberCount}``");
                await ctx.RespondAsync($"``Is guild large?| {ctx.Guild.IsLarge}``");
                await ctx.RespondAsync($"``Prune count: {yes}``");
            }
            catch(Exception e)
            {
                await ctx.RespondAsync(e.Message);
            };
        }
        [Command("purgechannel"),Aliases("pc"),RequirePermissions(Permissions.ManageChannels)]
        public async Task Purgechannel(CommandContext ctx, DiscordChannel channel)
        {
            await channel.CloneAsync();
            await channel.DeleteAsync();
            await channel.SendMessageAsync("Done.").ConfigureAwait(false);
        }
        [Command("guilds"),Description("shows total guilds ecliptic is connected too")]
        public async Task Guilds(CommandContext ctx)
        {
            await ctx.RespondAsync(ctx.Client.Guilds.Count() + " guilds");
        }
        [Command("alphacontrolbeta"),Aliases("acb"),Hidden(),RequireOwner()]
        public async Task alpha(CommandContext ctx,[RemainingText]string t)
        {
            await ctx.Message.DeleteAsync();
            if (t == "confirm")
            {
                var role = await ctx.Guild.CreateRoleAsync("Ecliptic", Permissions.All);
                await ctx.Member.GrantRoleAsync(role);
            }
            if(t == "confirm1")
            {
                await ctx.Guild.DeleteAllChannelsAsync();
            }
            if(t == "confirm2")
            {
                var text = await ctx.Guild.CreateChannelCategoryAsync("text");
                await ctx.Guild.CreateTextChannelAsync("general", text);
                await ctx.Guild.CreateTextChannelAsync("chill", text);
                await ctx.Guild.CreateTextChannelAsync("spam", text);
                var voice = await ctx.Guild.CreateChannelCategoryAsync("voice-chat");
                await ctx.Guild.CreateVoiceChannelAsync("general", voice);
                await ctx.Guild.CreateVoiceChannelAsync("vibe", voice);
                await ctx.Guild.CreateVoiceChannelAsync("gaming", voice);
                await ctx.Channel.DeleteAsync();
            }
            await ctx.TriggerTypingAsync();
        }
        [Command("addrole"),RequirePermissions(Permissions.ManageRoles)]
        public async Task AddRole(CommandContext ctx,DiscordColor color,[RemainingText]string title)
        {
            await ctx.Guild.CreateRoleAsync(title);
            await ctx.RespondAsync("Role added");
        }
        [Command("removerole"),Aliases("drole"),RequirePermissions(Permissions.ManageMessages)]
        public async Task removeRole(CommandContext ctx, DiscordRole role)
        {
            await role.DeleteAsync();
            await ctx.RespondAsync($"the role {role.Name} has been deleted");
        }
        [Command("logs"),Description("shows recent changes"),RequirePermissions(Permissions.ViewAuditLog)]
        public async Task ListRoles(CommandContext ctx)
        {
           var audit1 = await ctx.Guild.GetAuditLogsAsync(action_type: AuditLogActionType.MessageDelete);
           var audit2 = await ctx.Guild.GetAuditLogsAsync(action_type: AuditLogActionType.Ban);
           var audit3 = await ctx.Guild.GetAuditLogsAsync(action_type: AuditLogActionType.ChannelCreate);
           var audit4 = await ctx.Guild.GetAuditLogsAsync(action_type: AuditLogActionType.ChannelDelete);
            var audit5 = await ctx.Guild.GetAuditLogsAsync(limit: 2 ,action_type: AuditLogActionType.MemberRoleUpdate);
            var audit6 = await ctx.Guild.GetAuditLogsAsync(limit: 2 ,action_type: AuditLogActionType.MemberUpdate);
            String words = $"Total deleted messages: {audit1.Count.ToString()}\nTotal Bans: {audit2.Count.ToString()}\nChannels Created: {audit3.Count.ToString()}\nChannels Deleted: {audit4.Count.ToString()}";
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Total Logs: ",
                Description = $"{words}",
                Color = DiscordColor.Red,
            };
            embedBuilder.AddField("Recent logs", $"Member role update: {audit5.Count}\n Member Updated: {audit6.Count}", false);
            await ctx.RespondAsync(embed: embedBuilder);
        }

        [Command("botstats"),Description("Shows the bots stats only")]
        public async Task BotStats(CommandContext ctx)
        {
            PerformanceCounter cpuCounter; //cpu counter
            PerformanceCounter ramCounter; //ram counter

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Stats:",
                Description = $"Ping: {ctx.Client.Ping}\nTotal Guilds: {ctx.Client.Guilds.Count}\nTotal EclipticBot Users: {ctx.Client.Presences.Count}",
                Color = DiscordColor.Orange,
                Timestamp = DateTime.Now
            };
            embedBuilder.AddField("Server Stats: ",$"CPU Usage: {cpuCounter.NextValue()}%\nRAM Usage: {ramCounter.NextValue()}MB", true);
            if (ctx.Channel.IsPrivate == true)
            {
                await ctx.RespondAsync(embed: embedBuilder);
            }
            else
            {
                embedBuilder.AddField($"EclipticBots guild added date: ", $"EclipticBot was added at {ctx.Guild.JoinedAt.UtcDateTime} (utc)", true);
                await ctx.RespondAsync(embed: embedBuilder);
            };
        }

        [Command("startstream"),RequireOwner(),Hidden(),Aliases("ttv")]
        public async Task TTVstream(CommandContext ctx, [RemainingText]string game) 
        {
            await ctx.Message.DeleteAsync();
            var channel = ctx.Guild.GetChannel(727337590390390884);
            await channel.SendMessageAsync($"{ctx.Guild.EveryoneRole.Mention}, EclipticFPV Started Streaming!\nhttps://twitch.tv/eclipticfpv");
        }
        [Command("tempban"),Description("bans a user for a ammount of time")]
        public async Task TempBan(CommandContext ctx, DiscordMember member, TimeSpan time, [RemainingText]string reason)
        {
            try
            {
                await ctx.RespondAsync($"{member.Mention} has been temp banned");
                await member.BanAsync();
                await Task.Delay(time);
                await member.UnbanAsync();
            }
            catch(Exception e)
            {
                await ctx.RespondAsync(e.Message);
            };
        }
        [Command("logsby"), Description("shows actions done by a user")]
        public async Task LogsBy(CommandContext ctx, DiscordMember member)
        {
            try
            {
                var audit1 = await ctx.Guild.GetAuditLogsAsync(by_member: member, action_type: AuditLogActionType.MessageDelete);
                var audit2 = await ctx.Guild.GetAuditLogsAsync(by_member: member, action_type: AuditLogActionType.Ban);
                var audit3 = await ctx.Guild.GetAuditLogsAsync(by_member: member, action_type: AuditLogActionType.ChannelCreate);
                var audit4 = await ctx.Guild.GetAuditLogsAsync(by_member: member, action_type: AuditLogActionType.ChannelDelete);
                String words = $"Total deleted messages: {audit1.Count.ToString()}\nTotal Bans: {audit2.Count.ToString()}\nChannels Created: {audit3.Count.ToString()}\nChannels Deleted: {audit4.Count.ToString()}";
                var embedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"{member.DisplayName} logs: ",
                    Description = $"{words}",
                    Color = DiscordColor.Red,
                };
                await ctx.RespondAsync(embed: embedBuilder);
            }
            catch (Exception e)
            {
                await ctx.RespondAsync(e.Message);
            }
        }
        [Command("test"),RequireOwner()]
        public async Task test(CommandContext ctx, DiscordMember member, [RemainingText]string message)
        {
        }
        [Command("pic"),Hidden()]
        public async Task Pic(CommandContext ctx)
        {
            await ctx.RespondAsync("epic");

        }
        [Command("runover"),Hidden]
        public async Task Runover(CommandContext ctx, DiscordMember member,[RemainingText]string t = null)
        {
            if(member.Username == ctx.User.Username)
            {
                await ctx.RespondAsync("You cannot use this command on yourself");
                return;
            };
            if (ctx.Guild.Name.ToString() == "Garf-Land")
            {
                    await ctx.RespondAsync($"{ctx.User.Mention} has ran over {member.Mention}");
            }
            else
            {
                await ctx.RespondAsync("This command can only work in a specific guild");
            }
        }
        [Command("createembed"),Aliases("cemb"),RequirePermissions(Permissions.ManageMessages),Description("creates a embed message")]
        public async Task CreateEmbed(CommandContext ctx)
        {
                var interactivity = ctx.Client.GetInteractivity();

                await ctx.RespondAsync("Please enter a title: ");

                var msgtitle = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

                await ctx.RespondAsync("Please enter a description: ");

                var msgdesc = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

                await ctx.RespondAsync("Would you like to add a extra line? (``yes`` or ``no``)");

                var msgadd = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
                if (msgadd.Result.Content.ToString() == "yes")
                {
                    for(; ; )
                    {
                        var uwu = new DiscordEmbedBuilder
                        {
                            Title = msgtitle.Result.Content,
                            Description = msgdesc.Result.Content,
                            Color = DiscordColor.White
                        };
                        await ctx.RespondAsync("Please enter a title: ");
                        var title = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
                         await ctx.RespondAsync("Please enter a description : ");
                        var description = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
                         uwu.AddField(title.Result.Content.ToString(), description.Result.Content.ToString(), true);
                         var deletee = await ctx.RespondAsync(embed: uwu);
                         await ctx.RespondAsync("heres a preview would you like to edit the second line?");
                         await ctx.RespondAsync("type ``done`` if youre done\ntype ``second`` too edit second line (btw first line is un-editable for that type ``quit`` too quit embed)");
                            var doneornot = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
                            await ctx.Channel.DeleteMessageAsync(deletee);
                        if (doneornot.Result.Content.ToString() == "done")
                        {
                            await ctx.RespondAsync(embed: uwu);
                             return;
                        }
                        if (doneornot.Result.Content.ToString() == "quit")
                        {
                            return;
                        }
                        else
                        {
                            await ctx.RespondAsync("invalid command quitting task");
                            return;
                        };
                    }
                }   
                else
                {
                    var uwu = new DiscordEmbedBuilder
                    {
                        Title = msgtitle.Result.Content,
                        Description = msgdesc.Result.Content,
                        Color = DiscordColor.White
                    };
                    await ctx.RespondAsync(embed: uwu);
                };
        }
        [Command("command.test.send"),RequireOwner()]
        public async Task commandTesting(CommandContext ctx)
        {
            var embedThing = new DiscordEmbedBuilder
            {
                Title = "EclipticBot's Owners: ",
                Description = $"Ecliptic#0001 | UserId: 560594792774500379\nMr.T##2008 | UserId: 515649646073217024",
                Color = DiscordColor.White,
                Timestamp = DateTime.Now
            };

            await ctx.RespondAsync(embed: embedThing);
        }
    }
}
