using System;
using System.Reflection;
using System.Linq;

using Food.Core.Model;

namespace Food.Core;

public class AllMenuItemsForRestaurantId : Specification<MenuItem>
{
    public AllMenuItemsForRestaurantId(int id)
    {
        Criteria = mi => mi.RestaurantId == id;
    }
}
