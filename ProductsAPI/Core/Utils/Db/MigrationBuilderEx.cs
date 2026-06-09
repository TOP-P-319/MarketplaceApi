using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public static class MigrationBuilderEx
{
    extension(MigrationBuilder migrationBuilder)
    {
        public SqlFunctionMigrations FunctionMigrations => new(migrationBuilder);

        public SqlTriggerMigrations TriggerMigrations => new(migrationBuilder);
    }
}