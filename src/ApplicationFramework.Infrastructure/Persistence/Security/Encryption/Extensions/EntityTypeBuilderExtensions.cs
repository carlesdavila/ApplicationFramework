using System.Linq.Expressions;
using ApplicationFramework.Infrastructure.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationFramework.Infrastructure.Persistence.Security.Encryption.Extensions;

public static class EntityTypeBuilderExtensions
{
    /// <summary>
    /// Sets the encryption.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    /// <param name="propertyExpression">The property expression to encrypt.</param>
    /// <param name="encryptorProvider">The encryptor provider to use for encryption.</param>
    public static void SetEncryption<TEntity,TProperty>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity,TProperty>> propertyExpression, IEncryptionProvider encryptorProvider) where TEntity : class
    {
        var encryptionConverter = new EncryptionConverter(encryptorProvider);
        entityTypeBuilder.Property(propertyExpression).HasConversion(encryptionConverter);
    }

    /// <summary>
    /// Sets the encryption.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    /// <param name="propertyName">The property name to encrypt.</param>
    /// <param name="encryptorProvider">The encryptor provider to use for encryption.</param>
    public static void SetEncryption<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string propertyName, IEncryptionProvider encryptorProvider) where TEntity : class
    {
        var encryptionConverter = new EncryptionConverter(encryptorProvider);
        entityTypeBuilder.Property(propertyName).HasConversion(encryptionConverter);
    }

    /// <summary>
    /// Sets the encryption.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRelatedEntity">The type of the related entity.</typeparam>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    /// <param name="navigationExpression">The navigation expression for the owned type.</param>
    /// <param name="propertyExpression">The property expression to encrypt.</param>
    /// <param name="encryptorProvider">The encryptor provider to use for encryption.</param>
    public static void SetEncryption<TEntity,TRelatedEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder,Expression<Func<TEntity, TRelatedEntity>> navigationExpression, Expression<Func<TRelatedEntity,string>> propertyExpression, IEncryptionProvider encryptorProvider) where TEntity : class where TRelatedEntity : class
    {
        var encryptionConverter = new EncryptionConverter(encryptorProvider);
        entityTypeBuilder.OwnsOne(navigationExpression, y =>
        {
            y.Property(propertyExpression).HasConversion(encryptionConverter);
        });
    }

    /// <summary>
    /// Sets the encryption.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRelatedEntity">The type of the related entity.</typeparam>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    /// <param name="navigationExpression">The navigation expression for the owned type.</param>
    /// <param name="propertyExpression">The property expression to encrypt.</param>
    /// <param name="encryptorProvider">The encryptor provider to use for encryption.</param>
    public static void SetEncryption<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> navigationExpression, Expression<Func<TRelatedEntity, string>> propertyExpression, IEncryptionProvider encryptorProvider) where TEntity : class where TRelatedEntity : class
    {
        var encryptionConverter = new EncryptionConverter(encryptorProvider);
        entityTypeBuilder.OwnsMany(navigationExpression, y =>
        {
            y.Property(propertyExpression).HasConversion(encryptionConverter);
        });
    }


    /// <summary>
    /// Sets the encryption.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRelatedEntity">The type of the related entity.</typeparam>
    /// <param name="entityTypeBuilder">The entity type builder.</param>
    /// <param name="navigation"></param>
    /// <param name="propertyExpression">The property expression to encrypt.</param>
    /// <param name="encryptorProvider">The encryptor provider to use for encryption.</param>
    public static void SetEncryption<TEntity, TRelatedEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string navigation, Expression<Func<TRelatedEntity, string>> propertyExpression, IEncryptionProvider encryptorProvider) where TEntity : class where TRelatedEntity : class
    {
        var encryptionConverter = new EncryptionConverter(encryptorProvider);
        entityTypeBuilder.OwnsOne<TRelatedEntity>(navigation, y =>
        {
            y.Property(propertyExpression).HasConversion(encryptionConverter);
        });
    }
}