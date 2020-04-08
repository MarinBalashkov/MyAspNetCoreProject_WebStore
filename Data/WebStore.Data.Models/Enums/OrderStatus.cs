namespace WebStore.Data.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public enum OrderStatus
    {
        NotVisited = 0,
        Visited = 1,
        Completed = 2,
        Shipped = 3,
        Delivered = 4,
        Canceled = 5,
    }
}
