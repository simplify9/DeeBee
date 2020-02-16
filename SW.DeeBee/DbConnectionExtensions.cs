using SW.PrimitiveTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SW.DeeBee
{
    public static class DbConnectionExtensions
    {
        async public static Task Add<TEntity>(this DbConnection connection, TEntity entity, DbTransaction transaction = null)
        {
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();
            PropertyInfo idProperty = null;
            var fields = new StringBuilder();
            var parameters = new StringBuilder();
            var command = connection.CreateCommandObject(transaction);
            var tableInfo = GetTableInfo(entityType);

            foreach (PropertyInfo property in properties)

                if (!property.Name.Equals(tableInfo.IdentityColumn, StringComparison.OrdinalIgnoreCase) || !tableInfo.ServerSideIdentity)
                {
                    var column = GetColumnInfo(property).ColumnName;
                    fields.Append(column + ", ");
                    parameters.Append("@" + column + ", ");
                    command.AddCommandParameter(column, property.GetValue(entity));
                }
                else
                    idProperty = property;

            string insertStatement = $"INSERT INTO {tableInfo.TableName} ({fields.ToString().Remove(fields.ToString().Length - 2)}) VALUES ({parameters.ToString().Remove(parameters.ToString().Length - 2)}) {(tableInfo.ServerSideIdentity ? ";" + IdentityCommand : "")}";

            command.CommandText = insertStatement;

            if (tableInfo.ServerSideIdentity)
            {
                var newId = await command.ExecuteScalarAsync();
                idProperty.SetValue(entity, Convert.ChangeType(newId, idProperty.PropertyType));
            }
            else
                await command.ExecuteNonQueryAsync();
        }

        async public static Task Update<TEntity>(this DbConnection connection, TEntity entity, DbTransaction transaction = null)
        {
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();
            PropertyInfo idProperty = null;
            string idColumn = string.Empty;
            var fields = new StringBuilder();
            var command = connection.CreateCommandObject(transaction);
            var tableInfo = GetTableInfo(entityType);

            foreach (PropertyInfo property in properties)
            {
                if (!property.Name.Equals(tableInfo.IdentityColumn, StringComparison.OrdinalIgnoreCase))
                {
                    string column = GetColumnInfo(property).ColumnName;
                    fields.Append(column + "= " + "@" + column + ", ");
                    command.AddCommandParameter(column, property.GetValue(entity));
                }
                else
                {
                    idProperty = property;
                    idColumn = GetColumnInfo(idProperty).ColumnName;
                    command.AddCommandParameter(idColumn, property.GetValue(entity));
                }
            }

            string updateStatement = $"UPDATE {tableInfo.TableName} SET {fields.ToString().Remove(fields.ToString().Length - 2)} WHERE {idColumn}=@{idColumn}";
            command.CommandText = updateStatement;

            await command.ExecuteNonQueryAsync();
        }

        async static public Task<IEnumerable<TEntity>> All<TEntity>(this DbConnection connection, SearchyRequest searchyRequest = null, params SearchySort[] sorts) where TEntity : new()
        {
            var entityType = typeof(TEntity);
            var command = connection.CreateCommandObject();
            var request = searchyRequest.Conditions.FirstOrDefault();
            string where = "";
            string orderBy = "";

            if (request != null && request.Filters.Count > 0)
            {
                where = " WHERE ";

                int index = 0;
                foreach (var filter in request.Filters)
                {
                    index += 1;
                    var filterColName = GetColumnInfo(entityType, filter.Field).ColumnName;
                    var parameter = command.AddCommandParameter(filterColName + index.ToString());

                    switch (filter.Rule)
                    {
                        case SearchyRule.EqualsTo:
                            where = $"{where} ({filterColName}={parameter.ParameterName}) AND ";
                            parameter.Value = filter.Value;
                            break;

                        case SearchyRule.LessThan:
                            where = $"{where} ({filterColName}<{parameter.ParameterName}) AND ";
                            parameter.Value = filter.Value;
                            break;

                        case SearchyRule.LessThanOrEquals:
                            where = $"{where} ({filterColName}<={parameter.ParameterName}) AND ";
                            parameter.Value = filter.Value;
                            break;

                        case SearchyRule.GreaterThan:
                            where = $"{where} ({filterColName}>{parameter.ParameterName}) AND ";
                            parameter.Value = filter.Value;
                            break;

                        case SearchyRule.GreaterThanOrEquals:
                            where = $"{where} ({filterColName}>={parameter.ParameterName}) AND ";
                            parameter.Value = filter.Value;
                            break;

                        case SearchyRule.NotEqualsTo:
                            where = $"{where} ({filterColName}<>{parameter.ParameterName}) AND ";
                            parameter.Value = filter.Value;
                            break;

                        case SearchyRule.StartsWith:
                            where = $"{where} ({filterColName} like {parameter.ParameterName}) AND ";
                            parameter.Value = string.Concat(filter.Value, "%");
                            break;

                        case SearchyRule.Contains:
                            where = $"{where} ({filterColName} like {parameter.ParameterName}) AND ";
                            parameter.Value = string.Concat("%", filter.Value, "%");
                            break;


                            //case SearchyRule.EqualsToList:
                            //    {
                            //        var _ListType = _e.Value.GetType();
                            //        var _ItemType = _ListType.GetGenericArguments();
                            //        var _GenericListType = typeof(List<>);
                            //        var _GenericList = _GenericListType.MakeGenericType(_ItemType);

                            //        if (_GenericList != _ListType)
                            //            throw new Exception(string.Format("The value for the filter {0} is not a generic list", _filtercolname));

                            //        var _Values = new StringBuilder();
                            //        if (_ItemType.Contains(Type.GetType("System.String")) || _ItemType.Contains(Type.GetType("System.Guid")))
                            //        {
                            //            foreach (var _Value in _e.Value)
                            //                _Values.Append(string.Concat("'", _Value.ToString().Replace("'", "''"), "'", ","));
                            //        }
                            //        else
                            //            foreach (var _Value in _e.Value)
                            //                _Values.Append(string.Concat(_Value, ","));

                            //        _whereclause = string.Format(_whereclause + " ({0} IN ({1})) AND ", _filtercolname, _Values.ToString().TrimEnd(new char[] { ',' }));
                            //        break;
                            //    }
                    }
                }

                where = where.Remove(where.Length - 5);
            }

            if (sorts !=null && sorts.Length > 0)
            {
                orderBy = " ORDER BY ";
                foreach (var sort in sorts)
                    orderBy += string.Format(" {0} {1},", sort.Field, sort.Sort.ToString());

                orderBy = orderBy.Remove(orderBy.Length - 1);
            }

            string selectStatement = $"{BuildSelect<TEntity>()} {where} {orderBy} LIMIT {searchyRequest.PageIndex} , {searchyRequest.PageSize}";

            command.CommandText = selectStatement;

            return await (await command.ExecuteReaderAsync()).BindReader<TEntity>();
        }

        async public static Task<IEnumerable<TEntity>> All<TEntity>(this DbConnection connection, string queryText) where TEntity : new()
        {
            var command = connection.CreateCommandObject();

            command.CommandText = queryText;

            return await BindReader<TEntity>(await command.ExecuteReaderAsync());
        }
        public static Task<IEnumerable<TEntity>> All<TEntity>(this DbConnection connection, string field, object value, SearchyRule rule = SearchyRule.EqualsTo) where TEntity : new()
        {
            return connection.All<TEntity>(new SearchyRequest(field, rule, value));
        }
        async public static Task<TEntity> One<TEntity>(this DbConnection connection, object key) where TEntity : new()
        {
            var identityColumn = GetTableInfo(typeof(TEntity)).IdentityColumn;

            string selectStatement = $"{BuildSelect<TEntity>()} WHERE {identityColumn}=@{identityColumn}";
            var command = connection.CreateCommandObject();
            command.CommandText = selectStatement;
            command.AddCommandParameter(identityColumn, key);
            return (await BindReader<TEntity>(await command.ExecuteReaderAsync())).SingleOrDefault();
        }
        private static string BuildSelect<TEntity>()
        {
            var entityType = typeof(TEntity);
            string fields = "";

            foreach (var property in entityType.GetProperties())

                fields = $"{fields}{GetColumnInfo(property).ColumnName},";

            return $"SELECT {fields.Remove(fields.Length - 1)} FROM {GetTableInfo(entityType).TableName} ";
        }
        private static Table GetTableInfo(Type entityType)
        {
            var tableInfo = entityType.GetCustomAttribute<Table>();
            return tableInfo ?? new Table(entityType.Name);
        }

        private static Column GetColumnInfo(PropertyInfo propertyInfo)
        {
            var columnInfo = propertyInfo.GetCustomAttribute<Column>();
            return columnInfo ?? new Column(propertyInfo.Name);
        }

        private static Column GetColumnInfo(Type entityType, string propertyName)
        {
            return GetColumnInfo(entityType.GetProperty(propertyName));
        }

        private static DbCommand CreateCommandObject(this DbConnection connection, DbTransaction transaction = null)
        {
            var command = connection.CreateCommand();

            if (transaction != null)
                command.Transaction = transaction;
            //command.CommandTimeout = 0;

            return command;
        }

        private static IDbDataParameter AddCommandParameter(this IDbCommand command, string parameterName, object parameterValue = null)
        {
            IDbDataParameter parameter = command.CreateParameter();

            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = "@" + parameterName;
            parameter.Value = parameterValue ?? DBNull.Value;

            command.Parameters.Add(parameter);

            return parameter;
        }

        //private static IDataParameter CreateParameterObject(IDbCommand command)
        //{
        //    return command.CreateParameter();
        //}

        async private static Task<IEnumerable<TEntity>> BindReader<TEntity>(this DbDataReader reader) where TEntity : new()
        {
            var properties = typeof(TEntity).GetProperties();
            var list = new List<TEntity>();

            var propertyMapper = new Dictionary<int, int>();

            for (var fieldIndex = 0; fieldIndex < reader.FieldCount; fieldIndex++)

                for (var propertyIndex = 0; propertyIndex < properties.Length; propertyIndex++)
                {
                    string columnName = GetColumnInfo(properties[propertyIndex]).ColumnName;

                    if (columnName.Equals(reader.GetName(fieldIndex), StringComparison.OrdinalIgnoreCase))
                    {
                        propertyMapper.Add(fieldIndex, propertyIndex);
                        break;
                    }
                }


            while (await reader.ReadAsync())
            {
                var entity = new TEntity();

                for (var index = 0; index < reader.FieldCount; index++)
                {
                    if (!reader.IsDBNull(index) && propertyMapper.TryGetValue(index, out int propertyIndex))
                        properties[propertyIndex].SetValue(entity, reader[index], null);
                }

                list.Add(entity);
            }

            reader.Close();
            return list;
        }

        public static string IdentityCommand => "SELECT LAST_INSERT_ID();";
    }
}
