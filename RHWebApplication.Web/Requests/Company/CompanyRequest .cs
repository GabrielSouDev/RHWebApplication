namespace RHWebApplication.Web.Requests;

public class CompanyRequest
{
    public CompanyRequest() {}
    public CompanyRequest(string tradeName, string corporateName, int cnpj)
    {
        TradeName = tradeName;
        CorporateName = corporateName;
        CNPJ = cnpj;
    }
    public string TradeName { get; set; } = string.Empty;
    public string CorporateName { get; set; } = string.Empty;
    public int CNPJ { get; set; }
}