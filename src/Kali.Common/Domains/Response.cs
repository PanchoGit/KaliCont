﻿using System.Collections.Generic;
using System.Linq;

namespace Kali.Common.Domains
{
    public class Response<T> : Response where T : class
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    public class Response
    {
        public Response()
        {
            Messages = new List<Message>();
        }

        public IList<Message> Messages { get; set; }

        public bool HasErrors
        {
            get { return Messages.Any(x => x.Type == MessageType.Error); }
        }

        public void AddMessages(IList<Message> list)
        {
            foreach (var message in list)
                Messages.Add(message);
        }

        public void AddError(string message)
        {
            Messages.Add(new Message(message, MessageType.Error));
        }

        public void AddWarning(string message)
        {
            Messages.Add(new Message(message, MessageType.Warning));
        }

        public void AddSuccess(string message)
        {
            Messages.Add(new Message(message, MessageType.Success));
        }
    }
}
