using NotificareSdk.Android.Internal;
using NotificareSdk.Loyalty.Core.Models;

namespace NotificareSdk.Loyalty.Android.Internal;

internal static class NativeConverter
{
    internal static NotificarePass FromNativePass(Binding.Models.NotificarePass pass)
    {
        var type = pass.Type;

        return new NotificarePass(
            id: pass.Id,
            type: type == null ? null : FromNativePassType(type),
            version: pass.Version,
            passbook: pass.Passbook,
            template: pass.Template,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: FromNativePassRedeem(pass.GetRedeem()),
            redeemHistory: pass.RedeemHistory.Select(FromNativePassRedemption).ToList(),
            limit: pass.Limit,
            token: pass.Token,
            data: NotificareNativeConverter.FromNativeExtraDictionary(pass.Data),
            date: DateTimeOffset.FromUnixTimeMilliseconds(pass.Date.Time).DateTime,
            googlePaySaveLink: pass.GooglePaySaveLink
        );
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static string FromNativePassType(Binding.Models.NotificarePass.PassType type)
    {
        if (type == Binding.Models.NotificarePass.PassType.Boarding)
            return "boarding";

        if (type == Binding.Models.NotificarePass.PassType.Coupon)
            return "coupon";

        if (type == Binding.Models.NotificarePass.PassType.Ticket)
            return "ticket";

        if (type == Binding.Models.NotificarePass.PassType.Generic)
            return "generic";

        if (type == Binding.Models.NotificarePass.PassType.Card)
            return "card";

        throw new ArgumentException($"Unknown pass type: {type}");
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static string FromNativePassRedeem(Binding.Models.NotificarePass.Redeem redeem)
    {
        if (redeem == Binding.Models.NotificarePass.Redeem.Once)
            return "once";

        if (redeem == Binding.Models.NotificarePass.Redeem.Limit)
            return "limit";

        if (redeem == Binding.Models.NotificarePass.Redeem.Always)
            return "always";

        throw new ArgumentException($"Unknown pass redeem: {redeem}");
    }

    private static NotificarePassRedemption FromNativePassRedemption(
        Binding.Models.NotificarePass.Redemption redemption
    )
    {
        return new NotificarePassRedemption(
            comments: redemption.Comments,
            date: DateTimeOffset.FromUnixTimeMilliseconds(redemption.Date.Time).DateTime
        );
    }

    internal static Binding.Models.NotificarePass ToNativePass(NotificarePass pass)
    {
        return new Binding.Models.NotificarePass(
            id: pass.Id,
            type: pass.Type == null ? null : ToNativePassType(pass.Type),
            version: pass.Version,
            passbook: pass.Passbook,
            template: pass.Template,
            serial: pass.Serial,
            barcode: pass.Barcode,
            redeem: ToNativePassRedeem(pass.Redeem),
            redeemHistory: pass.RedeemHistory.Select(ToNativePassRedemption).ToList(),
            limit: pass.Limit,
            token: pass.Token,
            data: NotificareNativeConverter.ToNativeExtraDictionary(pass.Data),
            date: new Java.Util.Date(new DateTimeOffset(pass.Date).ToUnixTimeMilliseconds()),
            googlePaySaveLink: pass.GooglePaySaveLink
        );
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static Binding.Models.NotificarePass.PassType ToNativePassType(string type)
    {
        return type switch
        {
            "boarding" => Binding.Models.NotificarePass.PassType.Boarding!,
            "coupon" => Binding.Models.NotificarePass.PassType.Coupon!,
            "ticket" => Binding.Models.NotificarePass.PassType.Ticket!,
            "generic" => Binding.Models.NotificarePass.PassType.Generic!,
            "card" => Binding.Models.NotificarePass.PassType.Card!,
            _ => throw new ArgumentException($"Unknown pass type: {type}")
        };
    }

    // TODO: Add a rawValue in the native SDK and use that instead.
    private static Binding.Models.NotificarePass.Redeem ToNativePassRedeem(string redeem)
    {
        return redeem switch
        {
            "once" => Binding.Models.NotificarePass.Redeem.Once!,
            "limit" => Binding.Models.NotificarePass.Redeem.Limit!,
            "always" => Binding.Models.NotificarePass.Redeem.Always!,
            _ => throw new ArgumentException($"Unknown pass redeem: {redeem}")
        };
    }

    private static Binding.Models.NotificarePass.Redemption ToNativePassRedemption(
        NotificarePassRedemption redemption
    )
    {
        return new Binding.Models.NotificarePass.Redemption(
            comments: redemption.Comments,
            date: new Java.Util.Date(new DateTimeOffset(redemption.Date).ToUnixTimeMilliseconds())
        );
    }
}
