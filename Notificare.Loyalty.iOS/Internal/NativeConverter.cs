using NotificareSdk.iOS.Internal;
using NotificareSdk.Loyalty.Core.Models;

namespace NotificareSdk.Loyalty.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificarePass FromNativePass(NotificareSdk.Loyalty.iOS.Binding.NotificarePass pass)
    {
        return new NotificarePass(
            id: pass.PassId,
            type: pass.Type,
            version: pass.Version.ToInt32(),
            passbook: pass.Passbook,
            template: pass.PassTemplate,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: pass.Redeem,
            redeemHistory: pass.RedeemHistory.Select(FromNativePassRedemption).ToList(),
            limit: pass.Limit.ToInt32(),
            token: pass.Token,
            data: NotificareNativeConverter.FromNativeExtraDictionary(pass.Data),
            date: DateTimeOffset.FromUnixTimeSeconds((long)pass.Date.SecondsSince1970).DateTime,
            googlePaySaveLink: null
        );
    }

    private static NotificarePassRedemption FromNativePassRedemption(NotificareSdk.Loyalty.iOS.Binding.NotificarePassRedemption redemption)
    {
        return new NotificarePassRedemption(
            comments: redemption.Comments,
            date: DateTimeOffset.FromUnixTimeSeconds((long)redemption.Date.SecondsSince1970).DateTime
        );
    }

    internal static NotificareSdk.Loyalty.iOS.Binding.NotificarePass ToNativePass(NotificarePass pass)
    {
        return new NotificareSdk.Loyalty.iOS.Binding.NotificarePass(
            passId: pass.Id,
            type: pass.Type,
            version: pass.Version,
            passbook: pass.Passbook,
            passTemplate: pass.Template,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: pass.Redeem,
            redeemHistory: pass.RedeemHistory.Select(ToNativePassRedemption).ToArray(),
            limit: pass.Limit,
            token: pass.Token,
            data: NotificareNativeConverter.ToNativeExtraDictionary(pass.Data),
            date: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(pass.Date).ToUnixTimeSeconds())
        );
    }

    private static NotificareSdk.Loyalty.iOS.Binding.NotificarePassRedemption ToNativePassRedemption(
        NotificarePassRedemption redemption)
    {
        return new NotificareSdk.Loyalty.iOS.Binding.NotificarePassRedemption(
            comments: redemption.Comments,
            date: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(redemption.Date).ToUnixTimeSeconds())
        );
    }
}
