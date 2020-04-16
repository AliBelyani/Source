using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Others
{
    /// <summary>
    /// مدل جیسون تنظیمات آیدنتیتی سرور
    /// </summary>
    public class IdentityServerSettings
    {
        public string IdentityServerAuthority { get; set; }
        public IEnumerable<string> IdentityResources { get; set; }
        public IEnumerable<IdentityServerAPI> Apis { get; set; }
        public IEnumerable<IdentityServerClient> Clients { get; set; }

    }
    public class IdentityServerAPI
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<string> UserClaims { get; set; }
    }

    public class IdentityServerClient
    {
        public int AccessTokenLifeTime { get; set; }
        public string AllowedGrantTypes { get; set; }
        public string ClientID { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public IEnumerable<string> AllowCorsOrigins { get; set; }
        public bool RequireClientSecret { get; set; }
        public IEnumerable<string> AllowedScopes { get; set; }
        public bool AllowOfflineAccess { get; set; }

    }

}
