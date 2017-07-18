using System;

namespace ChangeOrchestrator.Transactions
{
    public interface ITransactionDispatcher
    {
        void Dispatch();
    }

    public interface ITransaction : ITransactionDispatcher
    {
        void Process();
        void Commit();
    }

    public interface IAtomicTransaction : ITransaction
    {
        void Rollback(Exception ex);
    }

    public abstract class BaseTransaction : ITransaction
    {
        public abstract void Dispatch();
        public abstract void Process();
        public abstract void Commit();
    }

    public abstract class AtomicTransaction : BaseTransaction, IAtomicTransaction
    {
        public override void Dispatch()
        {
            try
            {
                Process();
            }
            catch (Exception e)
            {
                Rollback(e);
            }

            Commit();
        }

        public abstract void Rollback(Exception ex);
    }

    public abstract class NonAtomicTransaction : BaseTransaction
    {
        public override void Dispatch()
        {
            Process();
            Commit();
        }
    }
}
