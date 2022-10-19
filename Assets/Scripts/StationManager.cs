using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public List<GameObject> BusStops;
    public List<GameObject> BusRoute;
    public DirectionalArrow DAS;

    private int currentIndex;
    private void Awake()
    {
        DAS.ChangeActiveStation = EnableNextStop;
    }
    private void Start()
    {
        BusStops = new List<GameObject>();
        BusRoute = new List<GameObject>();
        GameObject[] Temp = GameObject.FindGameObjectsWithTag("BusStop");
        for (int i = 0; i < Temp.Length;  ++i)
        {
            Temp[i].GetComponent<ParkingScript>().GetNearbyStops();
            BusStops.Add(Temp[i]);
        }
        ArrangeStops();
        currentIndex = -1;
        EnableNextStop();
        
    }

    private void ArrangeStops()
    {
        GameObject Start = BusStops[0];
        GameObject Next;
        BusRoute.Add(Start);
        BusStops.Remove(Start);
        while(BusStops.Count > 0)
        {
            Next = FindNext(Start.GetComponent<ParkingScript>(), ref BusRoute);
            if (Next == null)
            {
                BusRoute.Add(BusStops[0]);
                Start = BusStops[0];
                BusStops.Remove(Start);
            }
            else
            {
                BusRoute.Add(Next);
                BusStops.Remove(Next);
                Start = Next;
            }

        }
    }

    public void EnableNextStop()
    {
        if(currentIndex >= 0 && (currentIndex + 1) < BusRoute.Count)
        {
            BusRoute[currentIndex].GetComponent<ParkingScript>().bIsActive = false;
            BusRoute[currentIndex + 1].GetComponent<ParkingScript>().bIsActive = true;
            currentIndex = currentIndex + 1;
        }
        else
        {
            BusRoute[0].GetComponent<ParkingScript>().bIsActive = true;
            currentIndex = 0;
        }
        DAS.Destination = BusRoute[currentIndex];

    }

    private GameObject FindNext(ParkingScript InStart,ref List<GameObject> OutBusRoute)
    {
        if(InStart.StopsInRadius.Count == 0)
        {
            return null;
        }
        GameObject Temp = InStart.StopsInRadius[Random.Range(0, InStart.StopsInRadius.Count)];
        
        if(OutBusRoute.Contains(Temp))
        {
            InStart.StopsInRadius.Remove(Temp);
            if (InStart.StopsInRadius.Count <= 0)
            {
                return null;
            }
            else
            {
                return FindNext(InStart, ref OutBusRoute);
            }
        }
        else
        {
            return Temp;
        }
    }
}
