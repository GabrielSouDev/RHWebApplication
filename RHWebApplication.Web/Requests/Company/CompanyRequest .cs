namespace RHWebApplication.Web.Requests;

public class CompanyRequest
{
    public CompanyRequest() { }
    public CompanyRequest(string tradeName, string corporateName, int cnpj)
    {
        TradeName = tradeName;
        CorporateName = corporateName;
        CNPJ = cnpj;
    }
    public string TradeName { get; set; }
    public string CorporateName { get; set; }
    public int CNPJ { get; set; }
}


