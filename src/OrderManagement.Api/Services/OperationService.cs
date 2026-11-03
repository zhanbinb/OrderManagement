namespace OrderManagement.Api.Services;

public class OperationService : IOperationService, ITransientOperationService,IScopedOperationService,ISingletonOperationService
{
    private readonly Guid _id=Guid.NewGuid();
    
    public Guid GetOperationId()
    {
        return _id;
    }
}