// using Food.Core.Model;
// using System;
// using System.Linq.Expressions;

// public class OrderFilterSpecification : Specification<Order>
// {
//     public OrderFilterSpecification(int? restaurantId, OrderStatus? status)
//     {
//         // Start with a true predicate if we need to combine filters
//         Expression<Func<Order, bool>>? predicate = null;
        
//         if (restaurantId.HasValue)
//         {
//             predicate = order => order.RestaurantId == restaurantId.Value;
//         }
        
//         if (status.HasValue)
//         {
//             var statusPredicate = order => order.Status == status.Value;
            
//             if (predicate != null)
//             {
//                 // Using PredicateBuilder would be cleaner, but here's a simple approach
//                 var parameter = Expression.Parameter(typeof(Order), "order");
//                 var combined = Expression.AndAlso(
//                     Expression.Invoke(predicate, parameter),
//                     Expression.Invoke(Expression.Lambda<Func<Order, bool>>(statusPredicate.Body, statusPredicate.Parameters), parameter)
//                 );
//                 predicate = Expression.Lambda<Func<Order, bool>>(combined, parameter);
//             }
//             else
//             {
//                 predicate = statusPredicate;
//             }
//         }
        
//         Criteria = predicate ?? (order => true);
        
//         // Add any includes you commonly need with orders
//         // For example:
//         // AddInclude(o => o.OrderItems);
//         // AddInclude(o => o.Customer);
//     }
// }