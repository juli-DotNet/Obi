using System;
using System.Collections.Generic;

namespace ObiMenagement.Core.Models;

public class RoadData:IdLongBaseModel
{

    public virtual Trip Trip { get; set; }
    public virtual TruckBase TruckBase { get; set; }
    public virtual TruckContainer TruckContainer { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public virtual Location StartingLocation { get; set; }
    public virtual Location DestinationLocation { get; set; }
    public long TotalKM { get; set; }
    public decimal Price { get; set; } 
    public virtual List<RoadClient> Clients { get; set; }
    public virtual List<RoadExpense> RoadFuels { get; set; }
    public bool IsExport { get; set; }
}