using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public static class MigrationBuilderEx
{
    private const string SetUpdatedAtFuncName = "set_updated_at";

    extension(MigrationBuilder migrationBuilder)
    {
        public void CreateUpdatedAtFunction()
        {
            migrationBuilder.Sql(
                $"""
                 CREATE OR REPLACE FUNCTION {SetUpdatedAtFuncName}()
                 RETURNS TRIGGER AS $$
                 BEGIN 
                     NEW.updated_at = NOW();
                     RETURN NEW;
                 END;
                 $$ LANGUAGE plpgsql;
                 """
            );
        }

        public void RemoveUpdatedAtFunction()
        {
            migrationBuilder.Sql(
                $"DROP FUNCTION IF EXISTS {SetUpdatedAtFuncName}();"
            );
        }

        public void AddUpdatedAtTriggerToTable(string tableName)
        {
            migrationBuilder.Sql(
                $"""
                 CREATE TRIGGER {tableName}_updated
                 BEFORE UPDATE ON {tableName}
                 FOR EACH ROW
                 EXECUTE FUNCTION {SetUpdatedAtFuncName}();
                 """
            );
        }

        public void RemoveUpdatedAtTriggerFromTable(string tableName)
        {
            migrationBuilder.Sql(
                $"DROP TRIGGER IF EXISTS {tableName}_updated ON {tableName};"
            );
        }
    }
}