using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoProject.Data.Common
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext> logger;
        private readonly UserResolverService userResolver;

        public DbSet<AppClaimType> AppClaimTypes { get; set; } = default!;
        public DbSet<AppClaimType_Hist> AppClaimTypes_Hist { get; set; } = default!;
        public DbSet<AppRole> AppRoles { get; set; } = default!;
        public DbSet<AppRole_Hist> AppRoles_Hist { get; set; } = default!;
        public DbSet<AppRoleClaim> AppRoleClaims { get; set; } = default!;
        public DbSet<AppRoleClaim_Hist> AppRoleClaims_Hist { get; set; } = default!;
        public DbSet<AppUser> AppUsers { get; set; } = default!;
        public DbSet<AppUser_Hist> AppUsers_Hist { get; set; } = default!;
        public DbSet<AppUserClaim> AppUserClaims { get; set; } = default!;
        public DbSet<AppUserClaim_Hist> AppUserClaims_Hist { get; set; } = default!;
        public DbSet<ListSettingsForUser> ListSettingsForUsers { get; set; } = default!;
        public DbSet<ListSettingsForUser_Hist> ListSettingsForUsers_Hist { get; set; } = default!;
        public DbSet<AppUserLogin> AppUserLogins { get; set; } = default!;
        public DbSet<AppUserLogin_Hist> AppUserLogins_Hist { get; set; } = default!;
        public DbSet<AppUserRole> AppUserRoles { get; set; } = default!;
        public DbSet<AppUserRole_Hist> AppUserRoles_Hist { get; set; } = default!;
        public DbSet<AppUserToken> AppUserTokens { get; set; } = default!;
        public DbSet<AppUserToken_Hist> AppUserTokens_Hist { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger, UserResolverService userResolver)
            : base(options)
        {
            this.logger = logger;
            this.userResolver = userResolver;
        }

        public void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            var typesToRegister = typeof(AppDbContext).Assembly.GetTypes().Where(
                    t => t.IsSubclassOf(typeof(Record)) && t != typeof(Record) && !t.IsAbstract
            ).ToList();

            foreach (var type in typesToRegister)
            {
                ((Record)Activator.CreateInstance(type)!).Configure(modelBuilder, logger);
            }

            SeedData.PopulateTables(modelBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ApplyAllConfigurations(builder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            List<Record> entitiesGeneratingHist = PrepareAndGetRecordsToSave();

            //only init a transaction if not already started
            using (var trn = (this.Database.CurrentTransaction == null ? this.Database.BeginTransaction() : null))
            {
                logger.LogDebug($@"{nameof(SaveChangesAsync)} - calling {nameof(SaveChangesAsync)} the first time...");
                var retVal = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

                logger.LogDebug($@"{nameof(SaveChangesAsync)} - adding hist records for each change...");
                this.AddRange(entitiesGeneratingHist.Where(e => e.CanGetHistRecord).Select(e => e.GetHistRecord()!));

                await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
                logger.LogDebug($@"{nameof(SaveChangesAsync)} - called hist save, comitting...");
                this.Database.CommitTransaction();

                logger.LogDebug($@"{nameof(SaveChangesAsync)} ended, returning...");
                return retVal;
            }
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            List<Record> entitiesGeneratingHist = PrepareAndGetRecordsToSave();

            //only init a transaction if not already started
            using (var trn = (this.Database.CurrentTransaction == null ? this.Database.BeginTransaction() : null))
            {
                logger.LogDebug($@"{nameof(SaveChanges)} - calling {nameof(SaveChanges)} the first time...");
                var retVal = base.SaveChanges(acceptAllChangesOnSuccess);

                logger.LogDebug($@"{nameof(SaveChanges)} - adding hist records for each change...");
                this.AddRange(entitiesGeneratingHist.Where(e => e.CanGetHistRecord).Select(e => e.GetHistRecord()!));

                base.SaveChanges(acceptAllChangesOnSuccess);
                logger.LogDebug($@"{nameof(SaveChanges)} - called hist save, comitting...");
                this.Database.CommitTransaction();

                logger.LogDebug($@"{nameof(SaveChanges)} ended, returning...");
                return retVal;
            }
        }

        private List<Record> PrepareAndGetRecordsToSave()
        {
            logger.LogDebug($@"{nameof(SaveChangesAsync)} called.");
            DateTime currentDateTime = DateTime.Now;
            var currentUserId = userResolver.GetUserId();
            var currentUserName = userResolver.GetUser() ?? "";
            logger.LogDebug($@"user: {currentUserId}");

            IEnumerable<EntityEntry<Record>> changedRecords = this.ChangeTracker.Entries<Record>();

            var entitiesGeneratingHist = new List<Record>();

            // handle newly added entities
            foreach (var entity in changedRecords.Where(cr => cr.State != EntityState.Unchanged && !cr.Entity.IsHistRecord))
            {
                string what = entity.State == EntityState.Added ? "i"
                            : entity.State == EntityState.Modified ? "u"
                            : entity.State == EntityState.Deleted ? "d"
                            : throw new InvalidOperationException();


                if (what != "i" && (int)entity.OriginalValues[nameof(Record.Ver)]! != (int)entity.CurrentValues[nameof(Record.Ver)]!)
                {
                    throw new DbUpdateConcurrencyException();
                }

                entity.Entity.Ver++;
                entity.Entity.HistActionPerformedById = currentUserId;
                entity.Entity.HistActionPerformedBy = currentUserName;
                entity.Entity.HistActionType = what;
                entity.Entity.HistActionDate = currentDateTime;

                entitiesGeneratingHist.Add(entity.Entity);
            }

            return entitiesGeneratingHist;
        }
    }
}