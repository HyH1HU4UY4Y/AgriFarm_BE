using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SharedApplication.Authorize;
using SharedDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.MultiTenant.Implement
{
    public class MultiTenantResolver : IMultiTenantResolver
    {
        private HttpContext _httpContext;
        private string _currentTenant = "";
        private ILogger<MultiTenantResolver> _logger;

        public MultiTenantResolver(IHttpContextAccessor contextAccessor, ILogger<MultiTenantResolver> logger)
        {
            _httpContext = contextAccessor.HttpContext!;
            if (_httpContext != null)
            {
                _currentTenant = _httpContext.User?.GetSiteId() ?? "";
            }
            _logger = logger;
        }

        public Guid GetTenantId()
        {
            /*if (string.IsNullOrWhiteSpace(_currentTenant))
            {
                throw new BadRequestException();
            }*/
            var id = Guid.Empty;
            if (!Guid.TryParse(_currentTenant, out id))
            {
                _logger.LogInformation("This is not a tenant id");
            }

            return id;
        }

        public bool IsSuperAdmin()
        {
            return _currentTenant == Guid.Empty.ToString();
        }
    }
}
