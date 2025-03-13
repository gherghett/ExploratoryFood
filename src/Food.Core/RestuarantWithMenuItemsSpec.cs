public class RestaurantWithMenuItemsSpec : Specification<Restaurant>
{
    public RestaurantWithMenuItemsSpec()
    {
        AddInclude(r => r.MenuItems);
    }
}
