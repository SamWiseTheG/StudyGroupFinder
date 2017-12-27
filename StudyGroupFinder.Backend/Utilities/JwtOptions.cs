using System;
using System.Collections.Generic;

namespace StudyGroupFinder.Backend.Utilities
{
    public class JwtOptions
    {
        #region Required
        public string Issuer { get; set; }
        public string Audience { get; set; }
		public string SecretKey { get; set; }
        #endregion

        #region Set Optionally
        public Guid Id { get; set; } = Guid.NewGuid();           
		public string Subject { get; set; } = "Default";
        public Dictionary<string, string> PublicClaims = new Dictionary<string, string>();
        public int ExpiryInMinutes { get; set; } = 1;
        #endregion
    }
}
