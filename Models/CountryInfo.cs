public class CountryInfo
{
    public string Country { get; set; } = string.Empty;
    public string Continent { get; set; } = string.Empty;
    public string Population { get; set; } = string.Empty;
    public string IMF_GDP { get; set; } = string.Empty;
    public string UN_GDP { get; set; } = string.Empty;
    public string GDP_per_capita { get; set; } = string.Empty;
    public string WomenPeaceAndSecurityIndex_Score_2023 { get; set; } = string.Empty;
    public string WomensDangerIndexWDI_TotalScore_2019 { get; set; } = string.Empty;
    public string WDIStreetSafety_2019 { get; set; } = string.Empty;
    public string WDIIntentionalHomicide_2019 { get; set; } = string.Empty;
    public string WDINonPartnerViolence_2019 { get; set; } = string.Empty;
    public required string WDIIntimatePartnerViolence_2019 { get; set; }
    public string WDILegalDiscrimination_2019 { get; set; } = string.Empty;
    public string WDIGlobalGenderGap_2019 { get; set; } = string.Empty;
    public string WDIGenderInequality_2019 { get; set; } = string.Empty;
    public string WDIAttitudesTowardViolence_2019 { get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
}
