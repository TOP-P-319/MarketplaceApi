using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public sealed class SqlTriggerMigrations(MigrationBuilder migrationBuilder)
{
    public ISqlTriggerMigration UpdatedAt => new UpdatedAtSqlTriggerMigration(migrationBuilder);
}