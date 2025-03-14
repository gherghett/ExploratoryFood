using System;
using System.Reflection;
using System.Linq;

namespace Food.Core;

public class AllMenuItemsForRestaurantId : Specification<MenuItem>
{
    public AllMenuItemsForRestaurantId(int id)
    {
        Criteria = mi => mi.RestaurantId == id;
    }
}
