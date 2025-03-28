namespace NotificareSdk.Loyalty.Core.Models;

public class NotificarePass
{
    public string Id { get; }
    public string? Type { get; }
    public int Version { get; }
    public string? Passbook { get; }
    public string? Template { get; }
    public string Serial { get; }
    public string Barcode { get; }
    public string Redeem { get; }
    public IList<NotificarePassRedemption> RedeemHistory { get; }
    public int Limit { get; }
    public string Token { get; }
    public IDictionary<string, object> Data { get; }
    public DateTime Date { get; }
    public string? GooglePaySaveLink { get; }

    public NotificarePass(string id, string? type, int version, string? passbook, string? template, string serial,
        string barcode, string redeem, IList<NotificarePassRedemption> redeemHistory, int limit, string token,
        IDictionary<string, object> data, DateTime date, string? googlePaySaveLink)
    {
        Id = id;
        Type = type;
        Version = version;
        Passbook = passbook;
        Template = template;
        Serial = serial;
        Barcode = barcode;
        Redeem = redeem;
        RedeemHistory = redeemHistory;
        Limit = limit;
        Token = token;
        Data = data;
        Date = date;
        GooglePaySaveLink = googlePaySaveLink;
    }
}

public class NotificarePassRedemption
{
    public string? Comments { get; }
    public DateTime Date { get; }

    public NotificarePassRedemption(string? comments, DateTime date)
    {
        Comments = comments;
        Date = date;
    }
}
