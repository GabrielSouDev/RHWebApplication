namespace RHWebApplication.Web.Requests;

public class CompanyEditRequest
{
    public CompanyEditRequest(int id, string tradeName, string corporateName, int cnpj)
    {
        Id = id;
        TradeName = tradeName;
        CorporateName = corporateName;
        CNPJ = cnpj;
    }
    public int Id { get; set; } 
    public string TradeName { get; set; }
    public string CorporateName { get; set; }
    public int CNPJ { get; set; }
}