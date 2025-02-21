import Foundation
import UserNotifications
import NotificareKit
import NotificarePushKit
import NotificareBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificarePushNativeBinding)
public class NotificarePushNativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.push().delegate = self
    }

    @objc
    public var authorizationOptions: UNAuthorizationOptions {
        get {
            Notificare.shared.push().authorizationOptions
        }
        set {
            Notificare.shared.push().authorizationOptions = newValue
        }
    }

    @objc
    public var categoryOptions: UNNotificationCategoryOptions {
        get {
            Notificare.shared.push().categoryOptions
        }
        set {
            Notificare.shared.push().categoryOptions = newValue
        }
    }

    @objc
    public var presentationOptions: UNNotificationPresentationOptions {
        get {
            Notificare.shared.push().presentationOptions
        }
        set {
            Notificare.shared.push().presentationOptions = newValue
        }
    }

    @objc
    public var hasRemoteNotificationsEnabled: Bool {
        Notificare.shared.push().hasRemoteNotificationsEnabled
    }

    @objc
    public var transport: NotificareTransport {
        NotificareTransport(from: Notificare.shared.push().transport)
    }

    @objc
    public var subscription: NotificarePushSubscription? {
        Notificare.shared.push().subscription.map { NotificarePushSubscription(from: $0) }
    }

    @objc
    public var allowedUI: Bool {
        Notificare.shared.push().allowedUI
    }

    @objc
    public weak var delegate: NotificarePushNativeBindingDelegate?

    @objc
    public func enableRemoteNotifications(_ onSuccess: @escaping SuccessBlock<Bool>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.push().enableRemoteNotifications { result in
            switch result {
            case let .success(granted):
                onSuccess(granted)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func disableRemoteNotifications(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.push().disableRemoteNotifications { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension NotificarePushNativeBinding : NotificarePushDelegate {
    public func notificare(_ notificarePush: any NotificarePush, didChangeSubscription subscription: NotificarePushKit.NotificarePushSubscription?) {
        delegate?.notificare(self, didChangeSubscription: subscription.map { NotificarePushSubscription.init(from: $0) })
    }

    public func notificare(_ notificarePush: any NotificarePush, didChangeNotificationSettings allowedUI: Bool) {
        delegate?.notificare(self, didChangeNotificationSettings: allowedUI)
    }

    public func notificare(_ notificarePush: any NotificarePush, didReceiveNotification notification: NotificareKit.NotificareNotification, deliveryMechanism: NotificarePushKit.NotificareNotificationDeliveryMechanism) {
        delegate?.notificare(self, didReceiveNotification: NotificareBinding.NotificareNotification(from: notification), deliveryMechanism: NotificareNotificationDeliveryMechanism(from: deliveryMechanism))
    }

    public func notificare(_ notificarePush: any NotificarePush, didReceiveSystemNotification notification: NotificarePushKit.NotificareSystemNotification) {
        delegate?.notificare(self, didReceiveSystemNotification: NotificareSystemNotification(from: notification))
    }

    public func notificare(_ notificarePush: any NotificarePush, didReceiveUnknownNotification userInfo: [AnyHashable : Any]) {
        delegate?.notificare(self, didReceiveUnknownNotification: userInfo)
    }

    public func notificare(_ notificarePush: any NotificarePush, shouldOpenSettings notification: NotificareKit.NotificareNotification?) {
        delegate?.notificare(self, shouldOpenSettings: notification.map { NotificareNotification(from: $0) })
    }

    public func notificare(_ notificarePush: any NotificarePush, didOpenNotification notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didOpenNotification: NotificareBinding.NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePush: any NotificarePush, didOpenUnknownNotification userInfo: [AnyHashable : Any]) {
        delegate?.notificare(self, didOpenUnknownNotification: userInfo)
    }

    public func notificare(_ notificarePush: any NotificarePush, didOpenAction action: NotificareKit.NotificareNotification.Action, for notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didOpenAction: NotificareBinding.NotificareNotificationAction(from: action), for: NotificareBinding.NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePush: any NotificarePush, didOpenUnknownAction action: String, for notification: [AnyHashable : Any], responseText: String?) {
        delegate?.notificare(self, didOpenUnknownAction: action, for: notification, responseText: responseText)
    }
}

@objc
public protocol NotificarePushNativeBindingDelegate : NSObjectProtocol {
    // func notificare(_ notificarePush: NotificarePushNativeBinding, didFailToRegisterForRemoteNotificationsWithError error: any Error)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didChangeSubscription subscription: NotificarePushSubscription?)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didChangeNotificationSettings allowedUI: Bool)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didReceiveUnknownNotification userInfo: [AnyHashable : Any])

    func notificare(_ notificarePush: NotificarePushNativeBinding, didReceiveNotification notification: NotificareBinding.NotificareNotification, deliveryMechanism: NotificareNotificationDeliveryMechanism)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didReceiveSystemNotification notification: NotificareSystemNotification)

    func notificare(_ notificarePush: NotificarePushNativeBinding, shouldOpenSettings notification: NotificareBinding.NotificareNotification?)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didOpenNotification notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didOpenUnknownNotification userInfo: [AnyHashable : Any])

    func notificare(_ notificarePush: NotificarePushNativeBinding, didOpenAction action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePush: NotificarePushNativeBinding, didOpenUnknownAction action: String, for notification: [AnyHashable : Any], responseText: String?)
}
