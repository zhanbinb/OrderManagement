namespace  OrderManagement.Api.Settings
{
    
}
public class OrderSettings
{
    public string SystemName { get; set; } = string.Empty;
    public int MaxItemsPerOrder { get; set; }
    public bool EnableAutoCancel { get; set; }
    public int AutoCancelMinutes { get; set; }
}