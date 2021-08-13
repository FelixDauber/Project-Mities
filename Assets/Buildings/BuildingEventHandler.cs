using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingEventHandler : MonoBehaviour, IMessageHandler
{
    private IMessageHandler messageHandler = new MessageHandler();

    public BuildingEventHandler Current => current;
    private BuildingEventHandler current;

    public void Awake()
    {
        if (current != null) Destroy(this);
        current = this;
        SubscribeTo<EventBuildingConstructed>(delegate { SaySomething(); });
    }

    public void SaySomething()
    {
        Debug.Log("WAH!");
    }

    public void Send<TMessage>(TMessage message)
    {
        messageHandler.Send(message);
    }

    public void SubscribeTo<TMessage>(Action<TMessage> callback)
    {
        messageHandler.SubscribeTo(callback);
    }

    public void UnsubscribeFrom<TMessage>(Action<TMessage> callback)
    {
        messageHandler.UnsubscribeFrom(callback);
    }

    public class EventBuildingConstructed
    {
        public Building building;
        public EventBuildingConstructed(Building building)
        {
            this.building = building;
        }
    }
    public class EventBuildingDestroyed
    {
        public Building building;
        public EventBuildingDestroyed(Building building)
        {
            this.building = building;
        }
    }
}
