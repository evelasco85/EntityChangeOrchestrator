namespace ChangeOrchestrator.Transactions
{
    public interface ITransaction
    {
        
    }

    public class AtomicTransaction : ITransaction
    {
    }

    public class NonAtomicTransaction : ITransaction
    {
    }
}
