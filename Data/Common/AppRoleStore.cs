using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Data.Common;

public class AppRoleStore:  IRoleStore<AppRole?>
                            , IQueryableRoleStore<AppRole?>
{
    private bool _disposed;

    private readonly AppDbContext Context;

    public AppRoleStore(AppDbContext dbContext)
    {
        Context = dbContext;
    }

    public bool AutoSaveChanges { get; set; } = true;

    public IQueryable<AppRole> Roles => Context.Set<AppRole>();

    protected Task SaveChanges(CancellationToken cancellationToken)
    {
        return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
    }

    protected void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }

    protected void CheckRepeatingStuff(CancellationToken cancellationToken, params (string name, object? value)[] argNameValuePairs)
    {
        cancellationToken.ThrowIfCancellationRequested();
        CheckRepeatingStuff(argNameValuePairs);
    }

    protected void CheckRepeatingStuff(params (string name, object? value)[] argNameValuePairs)
    {
        ThrowIfDisposed();
        foreach (var argNameValuePair in argNameValuePairs)
        {
            if (argNameValuePair.value == null)
            {
                throw new ArgumentNullException(argNameValuePair.name);
            }
        }
    }

    async Task<IdentityResult> IRoleStore<AppRole?>.CreateAsync(AppRole? role, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        Context.Add(role!);
        await SaveChanges(cancellationToken);
        return IdentityResult.Success;
    }

    async Task<IdentityResult> IRoleStore<AppRole?>.UpdateAsync(AppRole? role, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        Context.Attach(role!);
        //role.ConcurrencyStamp = Guid.NewGuid().ToString();
        //automatically using my Ver field
        Context.Update(role!);
        try
        {
            await SaveChanges(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "ConcurrencyFailure", Description = "ConcurrencyFailure" });
        }
        return IdentityResult.Success;
    }

    async Task<IdentityResult> IRoleStore<AppRole?>.DeleteAsync(AppRole? role, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        Context.Remove(role!);
        try
        {
            await SaveChanges(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(new IdentityError() { Code = "ConcurrencyFailure", Description = "ConcurrencyFailure" });
        }
        return IdentityResult.Success;
    }

    Task<string> IRoleStore<AppRole?>.GetRoleIdAsync(AppRole? role, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        return Task.FromResult(role!.Id.ToString());
    }

    Task<string?> IRoleStore<AppRole?>.GetRoleNameAsync(AppRole? role, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        return Task.FromResult(role!.Name);
    }

    Task IRoleStore<AppRole?>.SetRoleNameAsync(AppRole? role, string roleName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        role!.Name = roleName;
        return Task.CompletedTask;
    }

    Task<string?> IRoleStore<AppRole?>.GetNormalizedRoleNameAsync(AppRole? role, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        return Task.FromResult(role!.NormalizedName);
    }

    Task IRoleStore<AppRole?>.SetNormalizedRoleNameAsync(AppRole? role, string normalizedName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(role), role)
        );

        role!.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    Task<AppRole?> IRoleStore<AppRole?>.FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
        );

        var roleId = Convert.ToInt32(id, 10);
        return Roles.FirstOrDefaultAsync(u => u.Id.Equals(roleId), cancellationToken);
    }

    Task<AppRole?> IRoleStore<AppRole?>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(normalizedRoleName), normalizedRoleName)
        );

        return Roles.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
    }

    void IDisposable.Dispose()
    {
        _disposed = true;
    }
}
