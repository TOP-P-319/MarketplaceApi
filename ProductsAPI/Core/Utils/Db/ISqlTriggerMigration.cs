namespace ProductsAPI.Core.Utils.Db;

public interface ISqlTriggerMigration
{
    void AddToTable(string tableName);
    void RemoveFromTable(string tableName);
}