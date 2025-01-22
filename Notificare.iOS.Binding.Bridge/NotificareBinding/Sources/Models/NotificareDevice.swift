import Foundation
import NotificareKit

@objc
public class NotificareDevice : NSObject {
    @objc public let deviceId: String
    @objc public let userId: String?
    @objc public let userName: String?
    @objc public let timeZoneOffset: Float
    @objc public let dnd: NotificareDoNotDisturb?
    @objc public let userData: [String : String]
    @objc public let backgroundAppRefresh: Bool

    @objc public init(deviceId: String, userId: String?, userName: String?, timeZoneOffset: Float, dnd: NotificareDoNotDisturb?, userData: [String : String], backgroundAppRefresh: Bool) {
        self.deviceId = deviceId
        self.userId = userId
        self.userName = userName
        self.timeZoneOffset = timeZoneOffset
        self.dnd = dnd
        self.userData = userData
        self.backgroundAppRefresh = backgroundAppRefresh
    }

    public convenience init(from device: NotificareKit.NotificareDevice) {
        self.init(
            deviceId: device.id,
            userId: device.userId,
            userName: device.userName,
            timeZoneOffset: device.timeZoneOffset,
            dnd: device.dnd.map { NotificareDoNotDisturb(from: $0) },
            userData: device.userData,
            backgroundAppRefresh: device.backgroundAppRefresh
        )
    }
}
