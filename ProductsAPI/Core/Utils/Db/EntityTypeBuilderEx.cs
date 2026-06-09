using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsAPI.Core.Infrastructure.Db.Entities;

namespace ProductsAPI.Core.Utils.Db;

public static class EntityTypeBuilderEx
{
    public static void ToTable<TEntity>(this EntityTypeBuilder<TEntity> entity, string tableName)
        where TEntity : EntityBase
    {
        entity.ToTable(
            tableName,
            table => table.HasTrigger($"{tableName}_updated")
        );

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired()
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAddOrUpdate();
    }
}