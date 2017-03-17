using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace Shizuka.Modules
{
	public class ConversationModule : Module
	{
		public ConversationModule() : base("Conversation Module", "Allows Shizuka to coverse with users using Neural Networks to interpret and respond to messages.")
		{
		}

		public override void Init(Server server)
		{
			server.shizukaMentioned += ShizukaConverse;
		}

		private void ShizukaConverse(object sender, MessageEventArgs e)
		{
			
		}

		public override void Respond(Message message)
		{

		}
	}
}
