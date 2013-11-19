// Type: System.Data.Entity.DbContext
// Assembly: EntityFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\CoreProjects\Bettorknow\PremierStatistics\packages\EntityFramework.5.0.0\lib\net45\EntityFramework.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity
{
  [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Casing is intentional")]
  public class DbContext : IDisposable, IObjectContextAdapter
  {
    protected DbContext();
    protected DbContext(DbCompiledModel model);
    public DbContext(string nameOrConnectionString);
    public DbContext(string nameOrConnectionString, DbCompiledModel model);
    public DbContext(DbConnection existingConnection, bool contextOwnsConnection);
    public DbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection);
    public DbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext);
    protected virtual void OnModelCreating(DbModelBuilder modelBuilder);
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public DbSet Set(Type entityType);
    public virtual int SaveChanges();
    public IEnumerable<DbEntityValidationResult> GetValidationErrors();
    protected virtual bool ShouldValidateEntity(DbEntityEntry entityEntry);
    protected virtual DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items);
    public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    public DbEntityEntry Entry(object entity);
    public void Dispose();
    protected virtual void Dispose(bool disposing);
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string ToString();
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals(object obj);
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode();
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Type GetType();
    public Database Database { get; }
    ObjectContext IObjectContextAdapter.ObjectContext { get; }
    public DbChangeTracker ChangeTracker { get; }
    public DbContextConfiguration Configuration { get; }
  }
}
