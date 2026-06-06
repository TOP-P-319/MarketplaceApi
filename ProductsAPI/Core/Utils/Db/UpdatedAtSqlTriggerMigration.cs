using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public sealed class UpdatedAtSqlTriggerMigration(MigrationBuilder migrationBuilder) : ISqlTriggerMigration
{
    [LanguageInjection("SQL")] private const string AddUpdatedAtTriggerSql =
        """
        CREATE TRIGGER {0}_updated
        BEFORE UPDATE ON {0}
        FOR EACH ROW
        EXECUTE FUNCTION set_updated_at();
        """;
    public void AddToTable(string tableName) => migrationBuilder.Sql(string.Format(AddUpdatedAtTriggerSql, tableName));

    [LanguageInjection("SQL")] private const string RemoveUpdatedAtTriggerSql =
        "DROP TRIGGER IF EXISTS {0}_updated ON {0}";
    public void RemoveFromTable(string tableName) => migrationBuilder.Sql(string.Format(RemoveUpdatedAtTriggerSql, tableName));
}