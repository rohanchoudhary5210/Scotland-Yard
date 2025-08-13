using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public enum PlayerType { Detective, MrX }

public class PawnController : MonoBehaviour
{
    public PlayerType playerType;
    public StationController startStation;
    public StationController currentStation;
    public Dictionary<TransportType, int> tickets = new Dictionary<TransportType, int>();
    void Setup(StationController startStation)
    {
        // this.startStation = startStation;
        // Debug.Log($"Setting up pawn at station: {startStation.name}");
        // transform.position = startStation.transform.position;
        // Initialize pawn position, etc.
        tickets.Add(TransportType.Taxi, 0);
        tickets.Add(TransportType.Bus, 0);
        tickets.Add(TransportType.Underground, 0);
        tickets.Add(TransportType.Ferry, 0);
    }

    void AddTickets(int taxi, int bus, int underground, int blackcards)
    {
        tickets[TransportType.Bus] += bus;
        tickets[TransportType.Taxi] += taxi;
        tickets[TransportType.Underground] += underground;
        tickets[TransportType.Ferry] += blackcards;
        Debug.Log($"Added tickets: Taxi={taxi}, Bus={bus}, Underground={underground}, Ferry={blackcards}");
    }
    public void MoveToStation(StationController targetStation)
    {
        this.currentStation = targetStation;
        Debug.Log($"Moved to station: {targetStation.name}");
        transform.position=targetStation.transform.position;
    }
    bool HasTickets(TransportType transport)
    {
        if (tickets.ContainsKey(transport) && tickets[transport] > 0 || playerType == PlayerType.MrX && tickets[TransportType.Ferry] > 0)
        {
            return true;
        }
       else
        return false;
    }
    public void UseTicket(TransportType transport)
    {
        if (HasTickets(transport))
        {
            tickets[transport]--;
        }
        Debug.Log($"Remaining tickets: Taxi={tickets[TransportType.Taxi]}, Bus={tickets[TransportType.Bus]}, Underground={tickets[TransportType.Underground]}, Ferry={tickets[TransportType.Ferry]}");
    }


    void Start()
    {
        Setup(startStation);
        // AddTickets(0, 1, 0, 0);
        // AddTickets(1, 0, 0, 4);
        // Debug.Log($"Initial tickets: Taxi={tickets[TransportType.Taxi]}, Bus={tickets[TransportType.Bus]}, Underground={tickets[TransportType.Underground]}, Ferry={tickets[TransportType.Ferry]}");


    }
    // public void OnMouseDown()
    // {
    //     // Debug.Log($"Pawn clicked: {gameObject.name}");
    //     // UseTicket(TransportType.Bus);


    // }
    public void SetVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
    public void ReceiveTicket(TransportType transport)
    {
        if (tickets.ContainsKey(transport))
        {
            tickets[transport]++;
        }
    }

}
