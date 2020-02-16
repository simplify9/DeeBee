using SW.PrimitiveTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SW.DeeBee
{
    public class ConnectionHost : IDisposable
    {
        private readonly DeeBeeOptions deeBeeOptions;
        private DbTransaction transaction = null;

        public ConnectionHost(DeeBeeOptions deeBeeOptions)
        {
            this.deeBeeOptions = deeBeeOptions;
            Connection = (DbConnection)Activator.CreateInstance(deeBeeOptions.Provider);//     deeBeeOptions.ConnectionFactory.Invoke();
            Connection.ConnectionString = deeBeeOptions.ConntectionString;   
        }

        public DbConnection Connection { get; }

        async public Task Add<TEntity>(TEntity entity)
        {
            await OpenConnection();
            await Connection.Add(entity, transaction);
        }

        async public Task Update<TEntity>(TEntity entity)
        {
            await OpenConnection();
            await Connection.Update(entity, transaction);
        }

        public Task<IEnumerable<TEntity>> All<TEntity>(SearchyCondition condition) where TEntity : new()
        {
            return Connection.All<TEntity>(new SearchyCondition[] { condition });
        }

        async public Task<IEnumerable<TEntity>> All<TEntity>(IEnumerable<SearchyCondition> conditions = null, IEnumerable<SearchySort> sorts = null, int pageSize = 0, int pageIndex = 0) where TEntity : new()
        {
            await OpenConnection();
            return await Connection.All<TEntity>(conditions , sorts, pageSize, pageIndex);
        }

        async public Task<IEnumerable<TEntity>> All<TEntity>(string queryText) where TEntity : new()
        {
            await OpenConnection();
            return await Connection.All<TEntity>(queryText);
        }
        async public Task<IEnumerable<TEntity>> All<TEntity>(string field, object value, SearchyRule rule = SearchyRule.EqualsTo) where TEntity : new()
        {
            await OpenConnection();
            return await Connection.All<TEntity>(field, value, rule);
        }
        async public Task<TEntity> One<TEntity>(object key) where TEntity : new()
        {
            await OpenConnection();
            return await Connection.One<TEntity>(key);
        }

        async private Task OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed) 
            { 
                await Connection.OpenAsync();
                if (deeBeeOptions.Trasnaction)
                    transaction = await Connection.BeginTransactionAsync(deeBeeOptions.IsolationLevel);
            }
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
                Connection.Dispose();  
            }
        }
    }
}
