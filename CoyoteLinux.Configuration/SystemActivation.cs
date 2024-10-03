namespace CoyoteLinux.Configuration {
    public enum CoyoteLicenseLevel {
        vlInvalid,
        vlExpired,
        vlTrial,
        vlPersonal,
        vlEducational,
        vlCommercial,
        vlEnterprise,
        vlUnrestricted
    }

    internal class SystemActivation {
        internal CoyoteLicenseLevel ValidateLicense(string LicenseCode) {
            return CoyoteLicenseLevel.vlInvalid;
        }
    }
}
