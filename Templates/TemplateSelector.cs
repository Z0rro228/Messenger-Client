using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerApp.Models;
using MessengerApp.ViewModels;

namespace MessengerApp.Templates
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate IncomingMessageTemplate { get; set; }
        public DataTemplate OutgoingMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Message chat)
            {
                if (chat.FromUserId != ChatPageViewModel._userId)
                {
                    if (Message.FromUserAvaUri == null)
                    {
                        Message.FromUserAvaUri = "dotnet_bot.svg";
                    }
                    return IncomingMessageTemplate;
                }
                else
                    return OutgoingMessageTemplate;
            }
            throw new NotImplementedException();
        }
    }
}
