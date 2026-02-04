namespace Application.Features.Orders.Constants;

public static class OrdersBusinessMessages
{
    public const string SectionName = "Orders";

    public const string OrderNotExists = "OrderNotExists";
    public const string CartIsEmpty = "CartIsEmpty";
    public const string InsufficientStock = "InsufficientStock";
    public const string OrderDoesNotBelongToUser = "OrderDoesNotBelongToUser";
    public const string UserCanOnlyCancelOrders = "UserCanOnlyCancelOrders";
    public const string InvalidOrderStatusTransition = "InvalidOrderStatusTransition";
}