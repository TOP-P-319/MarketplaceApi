using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsAPI.Core.Utils.Db;

public sealed class UpdatedAtSqlFunctionMigration(MigrationBuilder migrationBuilder) : ISqlFunctionMigration
{
    [LanguageInjection("SQL")] private const string CreateUpdatedAtFunctionSql =
        """
        CREATE OR REPLACE FUNCTION set_updated_at()
        RETURNS TRIGGER AS $$
        BEGIN 
            NEW.updated_at = NOW();
            RETURN NEW;
        END;
        $$ LANGUAGE plpgsql;
        """;


    [LanguageInjection("SQL")] private const string RemoveUpdatedAtFunctionSql =
        "DROP FUNCTION IF EXISTS set_updated_at();";
            
    public void Add() => migrationBuilder.Sql(CreateUpdatedAtFunctionSql);

    public void Remove() => migrationBuilder.Sql(RemoveUpdatedAtFunctionSql);
}