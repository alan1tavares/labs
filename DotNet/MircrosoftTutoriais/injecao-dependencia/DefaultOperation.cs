using static System.Guid;

namespace injecao_dependencia
{
  public class DefaultOperation : ITransientOperation, IScopedOperation, ISingletonOperation
  {
    public string OperationId { get; } = NewGuid().ToString()[^4..];
  }
}
