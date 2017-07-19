namespace ChangeOrchestrator.Models
{
    public interface IDataSourceAggregator
    {
        
    }

    //Observer pattern
    //Use transaction(atomic or non-atomic?)
    //1 Entity = Multi-data sources
    //1 Entity = Multi-data sources identifier?
    //1 Entity = Multi-data sources search keys(logs?)
    public class DataSourceAggregator : IDataSourceAggregator   
    {
        void AggregateResult()
        {
            
        }
    }
}
