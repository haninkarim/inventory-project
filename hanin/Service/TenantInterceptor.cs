using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using hanin.Entities;
using hanin.ServiceIntefrace;

namespace hanin.Interceptors
{
    public class TenantInterceptor : SaveChangesInterceptor
    {
        private readonly ITenantService _tenantService;

        public TenantInterceptor(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SetTenantId(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SetTenantId(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void SetTenantId(DbContext? context)
        {
            if (context == null) return;

            var tenantId = _tenantService.GetTenantId();

            var entries = context.ChangeTracker.Entries<EntityBase>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                entry.Entity.TenantId = tenantId ?? "default-tenant";
            }
        }
    }
}