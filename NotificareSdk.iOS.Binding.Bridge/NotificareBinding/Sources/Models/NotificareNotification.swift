import Foundation
import NotificareKit

@objc
public class NotificareNotification : NSObject {
    @objc public let partial: Bool
    @objc public let notificationId: String
    @objc public let type: String
    @objc public let time: Date
    @objc public let title: String?
    @objc public let subtitle: String?
    @objc public let message: String
    @objc public let content: [NotificareNotificationContent]
    @objc public let actions: [NotificareNotificationAction]
    @objc public let attachments: [NotificareNotificationAttachment]
    @objc public let extra: [String : Any]
    @objc public let targetContentIdentifier: String?

    @objc public init(partial: Bool, notificationId: String, type: String, time: Date, title: String?, subtitle: String?, message: String, content: [NotificareNotificationContent], actions: [NotificareNotificationAction], attachments: [NotificareNotificationAttachment], extra: [String : Any], targetContentIdentifier: String?) {
        self.partial = partial
        self.notificationId = notificationId
        self.type = type
        self.time = time
        self.title = title
        self.subtitle = subtitle
        self.message = message
        self.content = content
        self.actions = actions
        self.attachments = attachments
        self.extra = extra
        self.targetContentIdentifier = targetContentIdentifier
    }

    public convenience init(from notification: NotificareKit.NotificareNotification) {
        self.init(
            partial: notification.partial,
            notificationId: notification.id,
            type: notification.type,
            time: notification.time,
            title: notification.title,
            subtitle: notification.subtitle,
            message: notification.message,
            content: notification.content.map { NotificareNotificationContent(from: $0) },
            actions: notification.actions.map { NotificareNotificationAction(from: $0) },
            attachments: notification.attachments.map { NotificareNotificationAttachment(from: $0) },
            extra: notification.extra,
            targetContentIdentifier: notification.targetContentIdentifier
        )
    }
}

@objc
public class NotificareNotificationContent : NSObject {
    @objc public let type: String
    @objc public let data: Any

    @objc public init(type: String, data: Any) {
        self.type = type
        self.data = data
    }

    public convenience init(from content: NotificareKit.NotificareNotification.Content) {
        self.init(
            type: content.type,
            data: content.data
        )
    }
}

@objc
public class NotificareNotificationAction : NSObject {
    @objc public let type: String
    @objc public let label: String
    @objc public let target: String?
    @objc public let keyboard: Bool
    @objc public let camera: Bool
    @objc public let destructive: Bool
    @objc public let icon: NotificareNotificationActionIcon?

    @objc public init(type: String, label: String, target: String?, keyboard: Bool, camera: Bool, destructive: Bool, icon: NotificareNotificationActionIcon?) {
        self.type = type
        self.label = label
        self.target = target
        self.keyboard = keyboard
        self.camera = camera
        self.destructive = destructive
        self.icon = icon
    }

    public convenience init(from action: NotificareKit.NotificareNotification.Action) {
        self.init(
            type: action.type,
            label: action.label,
            target: action.target,
            keyboard: action.keyboard,
            camera: action.camera,
            destructive: action.destructive ?? false,
            icon: action.icon.map { NotificareNotificationActionIcon(from: $0) }
        )
    }
}

@objc
public class NotificareNotificationActionIcon : NSObject {
    @objc public let android: String?
    @objc public let ios: String?
    @objc public let web: String?

    @objc public init(android: String?, ios: String?, web: String?) {
        self.android = android
        self.ios = ios
        self.web = web
    }

    public convenience init(from icon: NotificareKit.NotificareNotification.Action.Icon) {
        self.init(
            android: icon.android,
            ios: icon.ios,
            web: icon.web
        )
    }
}

@objc
public class NotificareNotificationAttachment : NSObject {
    @objc public let mimeType: String
    @objc public let uri: String

    @objc public init(mimeType: String, uri: String) {
        self.mimeType = mimeType
        self.uri = uri
    }

    public convenience init(from attachment: NotificareKit.NotificareNotification.Attachment) {
        self.init(
            mimeType: attachment.mimeType,
            uri: attachment.uri
        )
    }
}
