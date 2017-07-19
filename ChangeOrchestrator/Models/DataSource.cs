using System;
using System.Collections.Generic;
using Framework.Data_Manipulation;

namespace ChangeOrchestrator.Models
{
    public interface IDataSource<TSearchKey, TOutput>
    {
        IList<TOutput> GetData(TSearchKey searchKey);
    }

    public interface IDataSource<TSearchKey, TQueryObject, TQueryObjectSearchInput, TOutput> : IDataSource<TSearchKey, TOutput>
        where TQueryObject : IBaseQueryObject<TOutput, TQueryObjectSearchInput>
    {
        DataSource<TSearchKey, TQueryObject, TQueryObjectSearchInput, TOutput>.SearchKeyToSearchInputTranslation SearchKeyToSearchInputTranslator { set; }
    }

    public class DataSource<TSearchKey, TQueryObject, TQueryObjectSearchInput, TOutput> : IDataSource<TSearchKey, TQueryObject, TQueryObjectSearchInput, TOutput>
        where TQueryObject : IBaseQueryObject<TOutput, TQueryObjectSearchInput>
    {
        public delegate TQueryObjectSearchInput SearchKeyToSearchInputTranslation(TSearchKey key);

        private IBaseQueryObject<TOutput> _queryObject;
        private SearchKeyToSearchInputTranslation _searchKeyToSearchInputTranslator;

        public SearchKeyToSearchInputTranslation SearchKeyToSearchInputTranslator
        {
            set { _searchKeyToSearchInputTranslator = value; }
        }

        public DataSource(TQueryObject queryObject)
        {
            _queryObject = queryObject;

            //Default translator
            _searchKeyToSearchInputTranslator = new SearchKeyToSearchInputTranslation((key) =>
            {
                return (TQueryObjectSearchInput)((object)key);
            });
        }

        public IList<TOutput> GetData(TSearchKey searchKey)
        {
            List<TOutput> searchResults = new List<TOutput>();

            if (_queryObject == null) throw new NullReferenceException("'Query Object' is not defined");

            if (_searchKeyToSearchInputTranslator == null) throw new NullReferenceException("'Search key to search input' translator not defined");

            _queryObject.SearchInputObject = _searchKeyToSearchInputTranslator(searchKey);

            searchResults.AddRange(_queryObject.Execute());

            return searchResults;
        }
    }
}
