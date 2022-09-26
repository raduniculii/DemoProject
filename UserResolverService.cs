using System.Security.Claims;

namespace DemoProject
{
    public class UserResolverService  
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string? GetUser()
        {
            return _context.HttpContext?.User?.Identity?.Name;
        }

        public int? GetUserId()
        {
            var strId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (strId == null) return null;
            
            return Convert.ToInt32(strId, 10);
        }

        public bool HasClaim(string claimType){
            return _context.HttpContext?.User?.HasClaim(c => c.Type == claimType) ?? false;
        }
    }
}