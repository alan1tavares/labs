namespace injecao_dependencia
{
  public interface IOperation
  {
    string OperationId { get; }
  }

  public interface ITransientOperation : IOperation { }

  public interface IScopedOperation : IOperation { }

  public interface ISingletonOperation : IOperation { }
}
