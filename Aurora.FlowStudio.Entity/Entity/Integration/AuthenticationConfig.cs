using Aurora.FlowStudio.Entity.Entity.Base;
using Aurora.FlowStudio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aurora.FlowStudio.Entity.Entity.Integration
{
    [Table("AuthenticationConfigs", Schema = "integration")]

        public class AuthenticationConfig
    {
        public AuthenticationType Type { get; set; } = AuthenticationType.None;

        // Basic Auth
        [MaxLength(200)]
        public string? Username { get; set; }
        [MaxLength(200)]
        public string? Password { get; set; }

        // API Key
        [MaxLength(100)]
        public string? ApiKey { get; set; }
        [MaxLength(100)]
        public string? ApiKeyHeader { get; set; } = "X-API-Key";
        public ApiKeyLocation ApiKeyLocation { get; set; } = ApiKeyLocation.Header;

        // OAuth 2.0
        [MaxLength(100)]
        public string? ClientId { get; set; }
        [MaxLength(200)]
        public string? ClientSecret { get; set; }
        [MaxLength(2000)]
        public string? TokenUrl { get; set; }
        [MaxLength(2000)]
        public string? AuthorizationUrl { get; set; }
        [MaxLength(200)]
        public string? RedirectUri { get; set; }
        public List<string> Scopes { get; set; } = new();
        [MaxLength(100)]
        public string? AccessToken { get; set; }
        [MaxLength(100)]
        public string? RefreshToken { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? TokenExpiry { get; set; }

        // Bearer Token
        [MaxLength(100)]
        public string? BearerToken { get; set; }

        // JWT
        [MaxLength(100)]
        public string? JwtToken { get; set; }
        [MaxLength(200)]
        public string? JwtSecret { get; set; }

        // Certificate
        [MaxLength(2000)]
        public string? CertificatePath { get; set; }
        [MaxLength(200)]
        public string? CertificatePassword { get; set; }

        // AWS Signature
        [MaxLength(100)]
        public string? AwsAccessKey { get; set; }
        [MaxLength(100)]
        public string? AwsSecretKey { get; set; }
        [MaxLength(200)]
        public string? AwsRegion { get; set; }

        // Custom Headers
        public Dictionary<string, string> CustomAuthHeaders { get; set; } = new();
    }
}