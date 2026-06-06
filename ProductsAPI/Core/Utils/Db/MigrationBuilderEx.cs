using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public static class MigrationBuilderEx
{
    private const string SetUpdatedAtFuncName = "set_updated_at";
    
    public static void CreateUpdatedAtFunction(
        this MigrationBuilder migrationBuilder
        )
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

    public static void AddUpdatedTrigger(
        this MigrationBuilder migrationBuilder,
        string tableName)
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
}