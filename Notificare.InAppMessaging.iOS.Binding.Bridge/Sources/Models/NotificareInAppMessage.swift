import Foundation
import NotificareInAppMessagingKit

@objc
public class NotificareInAppMessage : NSObject {
    @objc public let inAppMessageId: String
    @objc public let name: String
    @objc public let type: String
    @objc public let context: [String]
    @objc public let title: String?
    @objc public let message: String?
    @objc public let image: String?
    @objc public let landscapeImage: String?
    @objc public let delaySeconds: Int
    @objc public let primaryAction: NotificareInAppMessageAction?
    @objc public let secondaryAction: NotificareInAppMessageAction?

    @objc public init(inAppMessageId: String, name: String, type: String, context: [String], title: String?, message: String?, image: String?, landscapeImage: String?, delaySeconds: Int, primaryAction: NotificareInAppMessageAction?, secondaryAction: NotificareInAppMessageAction?) {
        self.inAppMessageId = inAppMessageId
        self.name = name
        self.type = type
        self.context = context
        self.title = title
        self.message = message
        self.image = image
        self.landscapeImage = landscapeImage
        self.delaySeconds = delaySeconds
        self.primaryAction = primaryAction
        self.secondaryAction = secondaryAction
    }

    public convenience init(from message: NotificareInAppMessagingKit.NotificareInAppMessage) {
        self.init(
            inAppMessageId: message.id,
            name: message.name,
            type: message.type,
            context: message.context,
            title: message.title,
            message: message.message,
            image: message.image,
            landscapeImage: message.landscapeImage,
            delaySeconds: message.delaySeconds,
            primaryAction: message.primaryAction.map { NotificareInAppMessageAction(from: $0) },
            secondaryAction: message.secondaryAction.map { NotificareInAppMessageAction(from: $0) }
        )
    }
}

@objc
public class NotificareInAppMessageAction : NSObject {
    @objc public let label: String?
    @objc public let destructive: Bool
    @objc public let url: String?

    @objc public init(label: String?, destructive: Bool, url: String?) {
        self.label = label
        self.destructive = destructive
        self.url = url
    }

    public convenience init(from action: NotificareInAppMessagingKit.NotificareInAppMessage.Action) {
        self.init(
            label: action.label,
            destructive: action.destructive,
            url: action.url
        )
    }
}
