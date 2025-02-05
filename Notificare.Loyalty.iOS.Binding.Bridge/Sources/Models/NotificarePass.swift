import Foundation
import NotificareLoyaltyKit

@objc
public class NotificarePass : NSObject {
    @objc public let passId: String
    @objc public let type: String?
    @objc public let version: Int
    @objc public let passbook: String?
    @objc public let passTemplate: String?
    @objc public let serial: String
    @objc public let barcode: String
    @objc public let redeem: String
    @objc public let redeemHistory: [NotificarePassRedemption]
    @objc public let limit: Int
    @objc public let token: String
    @objc public let data: [String : Any]
    @objc public let date: Date

    @objc public init(passId: String, type: String?, version: Int, passbook: String?, passTemplate: String?, serial: String, barcode: String, redeem: String, redeemHistory: [NotificarePassRedemption], limit: Int, token: String, data: [String : Any], date: Date) {
        self.passId = passId
        self.type = type
        self.version = version
        self.passbook = passbook
        self.passTemplate = passTemplate
        self.serial = serial
        self.barcode = barcode
        self.redeem = redeem
        self.redeemHistory = redeemHistory
        self.limit = limit
        self.token = token
        self.data = data
        self.date = date
    }

    public convenience init(from pass: NotificareLoyaltyKit.NotificarePass) {
        self.init(
            passId: pass.id,
            type: pass.type?.rawValue,
            version: pass.version,
            passbook: pass.passbook,
            passTemplate: pass.template,
            serial: pass.serial,
            barcode: pass.barcode,
            redeem: pass.redeem.rawValue,
            redeemHistory: pass.redeemHistory.map { NotificarePassRedemption(from: $0) },
            limit: pass.limit,
            token: pass.token,
            data: pass.data,
            date: pass.date
        )
    }

    public func toNative() -> NotificareLoyaltyKit.NotificarePass {
        return NotificareLoyaltyKit.NotificarePass(
            id: passId,
            type: type.flatMap { NotificareLoyaltyKit.NotificarePass.PassType(rawValue: $0) },
            version: version,
            passbook: passbook,
            template: passTemplate,
            serial: serial,
            barcode: barcode,
            redeem: NotificareLoyaltyKit.NotificarePass.Redeem(rawValue: redeem)!,
            redeemHistory: redeemHistory.map { $0.toNative() },
            limit: limit,
            token: token,
            data: data,
            date: date
        )
    }
}

@objc
public class NotificarePassRedemption : NSObject {
    @objc public let comments: String?
    @objc public let date: Date

    @objc public init(comments: String?, date: Date) {
        self.comments = comments
        self.date = date
    }

    public convenience init(from redemption: NotificareLoyaltyKit.NotificarePass.Redemption) {
        self.init(
            comments: redemption.comments,
            date: redemption.date
        )
    }

    public func toNative() -> NotificareLoyaltyKit.NotificarePass.Redemption {
        return NotificareLoyaltyKit.NotificarePass.Redemption(
            comments: comments,
            date: date
        )
    }
}
