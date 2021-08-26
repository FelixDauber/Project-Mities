using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

interface IMessageHandler
{
    void SubscribeTo<TMessage>(Action<TMessage> callback);
    void UnsubscribeFrom<TMessage>(Action<TMessage> callback);
    void Send<TMessage>(TMessage message);
}

// Here is an implementation of a generic messageHandler using delegates:
class MessageHandler : IMessageHandler
{

    readonly Dictionary<Type, object> subscribers = new Dictionary<Type, object>();

    public void SubscribeTo<TMessage>(Action<TMessage> callback)
    {
        if (this.subscribers.TryGetValue(typeof(TMessage), out var subscribers))
        {
            callback = (subscribers as Action<TMessage>) + callback;
        }

        this.subscribers[typeof(TMessage)] = callback;
    }

    public void UnsubscribeFrom<TMessage>(Action<TMessage> callback)
    {
        if (this.subscribers.TryGetValue(typeof(TMessage), out var subscribers))
        {
            callback = (subscribers as Action<TMessage>) - callback;
            if (callback != null)
                this.subscribers[typeof(TMessage)] = callback;
            else
                this.subscribers.Remove(typeof(TMessage));
        }
    }

    public void Send<TMessage>(TMessage message)
    {
        if (this.subscribers.TryGetValue(typeof(TMessage), out var subscribers))
        {
            (subscribers as Action<TMessage>).Invoke(message);
        }
    }
}