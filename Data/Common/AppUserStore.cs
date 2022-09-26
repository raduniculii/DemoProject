using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Data.Common;

public class AppUserStore : IUserStore<AppUser?>
                            , IUserRoleStore<AppUser?>
                            , IUserClaimStore<AppUser?>
                            , IUserLoginStore<AppUser?>
                            , IUserPasswordStore<AppUser?>
                            , IUserSecurityStampStore<AppUser?>
                            , IUserTwoFactorStore<AppUser?>
                            , IUserPhoneNumberStore<AppUser?>
                            , IUserEmailStore<AppUser?>
                            , IQueryableUserStore<AppUser?>
                            , IUserLockoutStore<AppUser?>
                            , IUserAuthenticatorKeyStore<AppUser?>
                            , IUserTwoFactorRecoveryCodeStore<AppUser?>
{
    private bool _disposed;

    private readonly AppDbContext Context;

    public AppUserStore(AppDbContext dbContext)
    {
        Context = dbContext;
    }

    protected DbSet<AppUserClaim> UserClaims { get { return Context.Set<AppUserClaim>(); } }
    protected DbSet<AppUserLogin> UserLogins { get { return Context.Set<AppUserLogin>(); } }
    protected DbSet<AppRole> Roles { get { return Context.Set<AppRole>(); } }
    protected DbSet<AppUserRole> UserRoles { get { return Context.Set<AppUserRole>(); } }
    protected DbSet<AppUser> Users { get { return Context.Set<AppUser>(); } }
    protected DbSet<AppUserToken> UserTokens { get { return Context.Set<AppUserToken>(); } }

    IQueryable<AppUser?> IQueryableUserStore<AppUser?>.Users => Context.Set<AppUser>();

    protected AppUserClaim CreateUserClaim(AppUser user, Claim claim)
    {
        var userClaim = new AppUserClaim
        {
            AppUserId = user.Id
            , ClaimType = claim.Type
            , ClaimValue = claim.Value
            , Issuer = claim.Issuer
        };

        return userClaim;
    }

    protected AppUserLogin CreateUserLogin(AppUser user, UserLoginInfo login)
    {
        return new AppUserLogin
        {
            AppUserId = user.Id
            , ProviderKey = login.ProviderKey
            , LoginProvider = login.LoginProvider
            , ProviderDisplayName = login.ProviderDisplayName
        };
    }

    protected AppUserRole CreateUserRole(AppUser user, AppRole role)
    {
        return new AppUserRole
        {
            AppUserId = user.Id
            , AppRoleId = role.Id
        };
    }

    public bool AutoSaveChanges { get; set; } = true;

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
    protected Task<AppRole?> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return Roles.SingleOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
    }
    protected Task<AppUserLogin?> FindUserLoginAsync(int appUserId, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        return UserLogins.SingleOrDefaultAsync(userLogin => userLogin.AppUserId.Equals(appUserId) && userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey, cancellationToken);
    }
    protected Task<AppUserLogin?> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        return UserLogins.SingleOrDefaultAsync(userLogin => userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey, cancellationToken);
    }
    protected Task<AppUser?> FindUserAsync(int appUserId, CancellationToken cancellationToken)
    {
        return Users.SingleOrDefaultAsync(u => u.Id.Equals(appUserId), cancellationToken);
    }
    protected Task<AppUserRole?> FindUserRoleAsync(int appUserId, int appRoleId, CancellationToken cancellationToken)
    {
        return UserRoles.FindAsync(new object[] { appUserId, appRoleId }, cancellationToken).AsTask();
    }

    public void Dispose()
    {
        _disposed = true;
    }

    Task IUserClaimStore<AppUser?>.AddClaimsAsync(AppUser? user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            (nameof(user), user)
            , (nameof(claims), claims)
        );

        foreach (var claim in claims)
        {
            UserClaims.Add(CreateUserClaim(user!, claim));
        }

        return Task.FromResult(false);
    }

    Task IUserLoginStore<AppUser?>.AddLoginAsync(AppUser? user, UserLoginInfo login, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(login), login)
        );

        UserLogins.Add(CreateUserLogin(user!, login));
        return Task.FromResult(false);
    }

    async Task IUserRoleStore<AppUser?>.AddToRoleAsync(AppUser? user, string roleName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(roleName), roleName)
        );

        var roleEntity = await FindRoleAsync(roleName, cancellationToken);
        if (roleEntity == null)
        {
            throw new InvalidOperationException(string.Format("Role '{0}' not found.", roleName));
        }
        UserRoles.Add(CreateUserRole(user!, roleEntity));
    }

    async Task<IdentityResult> IUserStore<AppUser?>.CreateAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        Context.Add(user!);
        await SaveChanges(cancellationToken);
        return IdentityResult.Success;
    }

    async Task<IdentityResult> IUserStore<AppUser?>.DeleteAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        Context.Remove(user!);
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

    void IDisposable.Dispose()
    {
        Dispose();
    }

    Task<AppUser?> IUserEmailStore<AppUser?>.FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(normalizedEmail), normalizedEmail)
        );

        return Users.SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
    }

    Task<AppUser?> IUserStore<AppUser?>.FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(userId), userId)
        );

        var id = Convert.ToInt32(userId, 10);

        return Users.FindAsync(new object?[] { id }, cancellationToken).AsTask();
    }

    async Task<AppUser?> IUserLoginStore<AppUser?>.FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
        );

        var userLogin = await FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
        if (userLogin != null)
        {
            return await FindUserAsync(userLogin.AppUserId, cancellationToken);
        }

        return null;
    }

    Task<AppUser?> IUserStore<AppUser?>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
        );

        return Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
    }

    Task<int> IUserLockoutStore<AppUser?>.GetAccessFailedCountAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.AccessFailedCount);
    }

    async Task<IList<Claim>> IUserClaimStore<AppUser?>.GetClaimsAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return await UserClaims.Where(uc => uc.AppUserId.Equals(user!.Id)).Select(c => new Claim(c.ClaimType!, c.ClaimValue!, null, c.Issuer)).ToListAsync(cancellationToken);
    }

    Task<string?> IUserEmailStore<AppUser?>.GetEmailAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.Email);
    }

    Task<bool> IUserEmailStore<AppUser?>.GetEmailConfirmedAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.EmailConfirmed);
    }

    Task<bool> IUserLockoutStore<AppUser?>.GetLockoutEnabledAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.LockoutEnabled);
    }

    Task<DateTimeOffset?> IUserLockoutStore<AppUser?>.GetLockoutEndDateAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.LockoutEnd == null ? null : (DateTimeOffset?)new DateTimeOffset(user!.LockoutEnd!.Value));
    }

    async Task<IList<UserLoginInfo>> IUserLoginStore<AppUser?>.GetLoginsAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return await UserLogins.Where(l => l.AppUserId.Equals(user!.Id))
            .Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToListAsync(cancellationToken);
    }

    Task<string?> IUserEmailStore<AppUser?>.GetNormalizedEmailAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.NormalizedEmail);
    }

    Task<string?> IUserStore<AppUser?>.GetNormalizedUserNameAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.NormalizedUserName);
    }

    Task<string?> IUserPasswordStore<AppUser?>.GetPasswordHashAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.PasswordHash);
    }

    Task<string?> IUserPhoneNumberStore<AppUser?>.GetPhoneNumberAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.PhoneNumber);
    }

    Task<bool> IUserPhoneNumberStore<AppUser?>.GetPhoneNumberConfirmedAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.PhoneNumberConfirmed);
    }

    async Task<IList<string>> IUserRoleStore<AppUser?>.GetRolesAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        var userId = user!.Id;
        var query = from userRole in UserRoles
                    join role in Roles on userRole.AppRoleId equals role.Id
                    where userRole.AppUserId.Equals(userId)
                    select role.Name;

        return await query.ToListAsync(cancellationToken);
    }

    Task<string?> IUserSecurityStampStore<AppUser?>.GetSecurityStampAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.SecurityStamp);
    }

    Task<bool> IUserTwoFactorStore<AppUser?>.GetTwoFactorEnabledAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.TwoFactorEnabled);
    }

    Task<string> IUserStore<AppUser?>.GetUserIdAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.Id.ToString());
    }

    Task<string?> IUserStore<AppUser?>.GetUserNameAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.UserName);
    }

    async Task<IList<AppUser?>> IUserClaimStore<AppUser?>.GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(claim), claim)
        );

        var query = from userclaims in UserClaims
                    join user in Users on userclaims.AppUserId equals user.Id
                    where userclaims.ClaimValue == claim.Value
                    && userclaims.ClaimType == claim.Type
                    select user;

        return await query.ToListAsync(cancellationToken);
    }

    async Task<IList<AppUser?>> IUserRoleStore<AppUser?>.GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(roleName), roleName)
        );

        var role = await FindRoleAsync(roleName, cancellationToken);

        if (role != null)
        {
            var query = from userrole in UserRoles
                        join user in Users on userrole.AppUserId equals user.Id
                        where userrole.AppRoleId.Equals(role.Id)
                        select user;

            return await query.ToListAsync(cancellationToken);
        }
        return new List<AppUser?>();
    }

    Task<bool> IUserPasswordStore<AppUser?>.HasPasswordAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return Task.FromResult(user!.PasswordHash != null);
    }

    Task<int> IUserLockoutStore<AppUser?>.IncrementAccessFailedCountAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.AccessFailedCount++;
        return Task.FromResult(user!.AccessFailedCount);
    }

    async Task<bool> IUserRoleStore<AppUser?>.IsInRoleAsync(AppUser? user, string roleName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(roleName), roleName)
        );

        var role = await FindRoleAsync(roleName, cancellationToken);
        if (role != null)
        {
            var userRole = await FindUserRoleAsync(user!.Id, role.Id, cancellationToken);
            return userRole != null;
        }
        return false;
    }

    async Task IUserClaimStore<AppUser?>.RemoveClaimsAsync(AppUser? user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(claims), claims)
        );

        foreach (var claim in claims)
        {
            var matchedClaims = await UserClaims.Where(uc => uc.AppUserId.Equals(user!.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToListAsync(cancellationToken);
            foreach (var c in matchedClaims)
            {
                UserClaims.Remove(c);
            }
        }
    }

    async Task IUserRoleStore<AppUser?>.RemoveFromRoleAsync(AppUser? user, string roleName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(roleName), roleName)
        );

        var roleEntity = await FindRoleAsync(roleName, cancellationToken);
        if (roleEntity != null)
        {
            var userRole = await FindUserRoleAsync(user!.Id, roleEntity.Id, cancellationToken);
            if (userRole != null)
            {
                UserRoles.Remove(userRole);
            }
        }
    }

    async Task IUserLoginStore<AppUser?>.RemoveLoginAsync(AppUser? user, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        var entry = await FindUserLoginAsync(user!.Id, loginProvider, providerKey, cancellationToken);
        if (entry != null)
        {
            UserLogins.Remove(entry);
        }
    }

    async Task IUserClaimStore<AppUser?>.ReplaceClaimAsync(AppUser? user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(claim), claim)
            , (nameof(newClaim), newClaim)
        );

        var matchedClaims = await UserClaims.Where(uc => uc.AppUserId.Equals(user!.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToListAsync(cancellationToken);
        foreach (var matchedClaim in matchedClaims)
        {
            matchedClaim.ClaimValue = newClaim.Value;
            matchedClaim.ClaimType = newClaim.Type;
        }
    }

    Task IUserLockoutStore<AppUser?>.ResetAccessFailedCountAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.AccessFailedCount = 0;
        return Task.CompletedTask;
    }

    Task IUserEmailStore<AppUser?>.SetEmailAsync(AppUser? user, string email, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(email), email)
        );

        user!.Email = email;
        return Task.CompletedTask;
    }

    Task IUserEmailStore<AppUser?>.SetEmailConfirmedAsync(AppUser? user, bool confirmed, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.EmailConfirmed = confirmed;
        return Task.CompletedTask;
    }

    Task IUserLockoutStore<AppUser?>.SetLockoutEnabledAsync(AppUser? user, bool enabled, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.LockoutEnabled = enabled;
        return Task.CompletedTask;
    }

    Task IUserLockoutStore<AppUser?>.SetLockoutEndDateAsync(AppUser? user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.LockoutEnd = lockoutEnd?.DateTime;
        return Task.CompletedTask;
    }

    Task IUserEmailStore<AppUser?>.SetNormalizedEmailAsync(AppUser? user, string normalizedEmail, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(normalizedEmail), normalizedEmail)
        );

        user!.NormalizedEmail = normalizedEmail;
        return Task.CompletedTask;
    }

    Task IUserStore<AppUser?>.SetNormalizedUserNameAsync(AppUser? user, string normalizedName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(normalizedName), normalizedName)
        );

        user!.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    Task IUserPasswordStore<AppUser?>.SetPasswordHashAsync(AppUser? user, string passwordHash, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(passwordHash), passwordHash)
        );

        user!.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    Task IUserPhoneNumberStore<AppUser?>.SetPhoneNumberAsync(AppUser? user, string phoneNumber, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(phoneNumber), phoneNumber)
        );

        user!.PhoneNumber = phoneNumber;
        return Task.CompletedTask;
    }

    Task IUserPhoneNumberStore<AppUser?>.SetPhoneNumberConfirmedAsync(AppUser? user, bool confirmed, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.PhoneNumberConfirmed = confirmed;
        return Task.CompletedTask;
    }

    Task IUserSecurityStampStore<AppUser?>.SetSecurityStampAsync(AppUser? user, string stamp, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(stamp), stamp)
        );

        user!.SecurityStamp = stamp;
        return Task.CompletedTask;
    }

    Task IUserTwoFactorStore<AppUser?>.SetTwoFactorEnabledAsync(AppUser? user, bool enabled, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        user!.TwoFactorEnabled = enabled;
        return Task.CompletedTask;
    }

    Task IUserStore<AppUser?>.SetUserNameAsync(AppUser? user, string userName, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(userName), userName)
        );

        user!.UserName = userName;
        return Task.CompletedTask;
    }

    async Task<IdentityResult> IUserStore<AppUser?>.UpdateAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        Context.Attach(user!);
        //user.ConcurrencyStamp = Guid.NewGuid().ToString();
        //removed this as I have the Ver thing going on automatically, for all derived entities from Record.
        Context.Update(user!);
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

    private const string InternalLoginProvider = "[AppUserStore]";
    private const string AuthenticatorKeyTokenName = "AuthenticatorKey";
    private const string RecoveryCodeTokenName = "RecoveryCodes";

    protected virtual AppUserToken CreateUserToken(AppUser user, string loginProvider, string name, string value)
    {
        return new AppUserToken
        {
            AppUserId = user.Id,
            LoginProvider = loginProvider,
            Name = name,
            Value = value
        };
    }

    public virtual async Task SetTokenAsync(AppUser? user, string loginProvider, string name, string value, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        var token = await FindTokenAsync(user!, loginProvider, name, cancellationToken).ConfigureAwait(false);
        if (token == null)
        {
            await AddUserTokenAsync(CreateUserToken(user!, loginProvider, name, value)).ConfigureAwait(false);
        }
        else
        {
            token.Value = value;
        }
    }

    public virtual async Task RemoveTokenAsync(AppUser? user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        var entry = await FindTokenAsync(user!, loginProvider, name, cancellationToken).ConfigureAwait(false);
        if (entry != null)
        {
            await RemoveUserTokenAsync(entry).ConfigureAwait(false);
        }
    }

    public virtual async Task<string?> GetTokenAsync(AppUser? user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        var entry = await FindTokenAsync(user!, loginProvider, name, cancellationToken).ConfigureAwait(false);
        return entry?.Value;
    }
    
    protected async Task<AppUserToken?> FindTokenAsync(AppUser user, string loginProvider, string name, CancellationToken cancellationToken){
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        return await UserTokens.Where(ut => ut.AppUserId == user!.Id && ut.LoginProvider == loginProvider && ut.Name == name).FirstOrDefaultAsync(cancellationToken);
    }

    protected Task AddUserTokenAsync(AppUserToken token)
    {
        UserTokens.Add(token);
        return Task.CompletedTask;
    }

    protected Task RemoveUserTokenAsync(AppUserToken token)
    {
        UserTokens.Remove(token);
        return Task.CompletedTask;
    }

    Task IUserAuthenticatorKeyStore<AppUser?>.SetAuthenticatorKeyAsync(AppUser? user, string key, CancellationToken cancellationToken)
        => SetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, key, cancellationToken);

    Task<string?> IUserAuthenticatorKeyStore<AppUser?>.GetAuthenticatorKeyAsync(AppUser? user, CancellationToken cancellationToken)
        => GetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, cancellationToken);

    Task IUserTwoFactorRecoveryCodeStore<AppUser?>.ReplaceCodesAsync(AppUser? user, IEnumerable<string> recoveryCodes, CancellationToken cancellationToken)
    {
        var mergedCodes = string.Join(";", recoveryCodes);
        return SetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, mergedCodes, cancellationToken);
    }

    async Task<bool> IUserTwoFactorRecoveryCodeStore<AppUser?>.RedeemCodeAsync(AppUser? user, string code, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
            , (nameof(code), code)
        );

        var mergedCodes = await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken).ConfigureAwait(false) ?? "";
        var splitCodes = mergedCodes.Split(';');
        if (splitCodes.Contains(code))
        {
            var updatedCodes = new List<string>(splitCodes.Where(s => s != code));
            await ((IUserTwoFactorRecoveryCodeStore<AppUser>)this).ReplaceCodesAsync(user!, updatedCodes, cancellationToken).ConfigureAwait(false);
            return true;
        }
        return false;
    }

    async Task<int> IUserTwoFactorRecoveryCodeStore<AppUser?>.CountCodesAsync(AppUser? user, CancellationToken cancellationToken)
    {
        CheckRepeatingStuff(
            cancellationToken
            , (nameof(user), user)
        );

        var mergedCodes = await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken).ConfigureAwait(false) ?? "";
        if (mergedCodes.Length > 0)
        {
            return mergedCodes.Split(';').Length;
        }
        return 0;
    }
}