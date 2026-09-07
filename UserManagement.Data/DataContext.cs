using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DataContext()
    {
        
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "System", Surname = "User", Email = "system.user@example.com", IsActive = true, DateOfBirth = new DateTime(1984, 1, 1), Organisation = "Inflo", JobTitle = "System User Account" },
            new User { Id = 2, Forename = "Jonny", Surname = "Wilson", Email = "jonny.wilson@inflo.com", IsActive = true, DateOfBirth = new DateTime(1984, 3, 14), Organisation = "Inflo", JobTitle = "Senior Software Engineer"  },
            new User { Id = 3, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateTime(1968, 11, 22), Organisation = "Inflo", JobTitle = "Database Administrator"  },
            new User { Id = 4, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateTime(1991, 7, 5), Organisation = "Inflo", JobTitle = "Lead Platform Architect"  },
            new User { Id = 5, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateTime(1979, 1, 30), Organisation = "Inflo", JobTitle = "Customer Success Manager"  },
            new User { Id = 6, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateTime(1995, 9, 18), Organisation = "Inflo", JobTitle = "Head of Customer Success"  },
            new User { Id = 7, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateTime(1982, 12, 8), Organisation = "Inflo", JobTitle = "Product Manager"  },
            new User { Id = 8, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateTime(1973, 4, 11), Organisation = "Inflo", JobTitle = "Platform Engineer"  },
            new User { Id = 9, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateTime(1988, 6, 25), Organisation = "Inflo", JobTitle = "Product Manager"  },
            new User { Id = 10, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateTime(1965, 10, 19), Organisation = "Inflo", JobTitle = "Working Papers Product Owner"  },
            new User { Id = 11, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateTime(1992, 2, 14), Organisation = "Inflo", JobTitle = "Senior Account Manager"  },
            new User { Id = 12, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateTime(1959, 8, 3), Organisation = "Inflo", JobTitle = "Talent Aquisition Specialis"  },
        });
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Log>? Logs { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }

    public TEntity? GetById<TEntity>(long id) where TEntity : class
    {
        return base.Find<TEntity>(id);
    }
    
    public TEntity? GetByIdNoTracking<TEntity>(long id) where TEntity : class
    {
        var keyName = Model.FindEntityType(typeof(TEntity))?
            .FindPrimaryKey()?
            .Properties
            .Select(x => x.Name)
            .SingleOrDefault() ?? "Id";

        return base.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefault(e => EF.Property<long>(e, keyName) == id);
    }
}