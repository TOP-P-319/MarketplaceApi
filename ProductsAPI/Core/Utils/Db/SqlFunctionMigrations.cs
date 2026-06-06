using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public sealed class SqlFunctionMigrations(MigrationBuilder migrationBuilder)
{
    public ISqlFunctionMigration UpdatedAt => new UpdatedAtSqlFunctionMigration(migrationBuilder);
}