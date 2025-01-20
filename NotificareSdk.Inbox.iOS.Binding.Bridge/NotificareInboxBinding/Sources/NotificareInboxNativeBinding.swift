import Foundation
import UserNotifications
import NotificareKit
import NotificareInboxKit
import NotificareBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareInboxNativeBinding)
public class NotificareInboxNativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.inbox().delegate = self
    }

    @objc
    public var items: [NotificareInboxItem] {
        Notificare.shared.inbox().items.map { NotificareInboxItem(from: $0) }
    }

    @objc
    public var badge: Int {
        Notificare.shared.inbox().badge
    }

    @objc
    public weak var delegate: NotificareInboxNativeBindingDelegate?

    @objc
    public func refresh() {
        Notificare.shared.inbox().refresh()
    }

    @objc
    public func refreshBadge(_ onSuccess: @escaping SuccessBlock<Int>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.inbox().refreshBadge { result in
            switch result {
            case let .success(badge):
                onSuccess(badge)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func open(_ item: NotificareInboxItem, _ onSuccess: @escaping SuccessBlock<NotificareBinding.NotificareNotification>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.inbox().open(item.toNative()) { result in
            switch result {
            case let .success(notification):
                onSuccess(NotificareNotification(from: notification))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func markAsRead(_ item: NotificareInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.inbox().markAsRead(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func markAllAsRead(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.inbox().markAllAsRead { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func remove(_ item: NotificareInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.inbox().remove(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func clear(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.inbox().clear { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension NotificareInboxNativeBinding : NotificareInboxDelegate {
    public func notificare(_ notificareInbox: any NotificareInboxKit.NotificareInbox, didUpdateInbox items: [NotificareInboxKit.NotificareInboxItem]) {
        delegate?.notificare(self, didUpdateInbox: items.map { NotificareInboxItem(from: $0) })
    }

    public func notificare(_ notificareInbox: any NotificareInboxKit.NotificareInbox, didUpdateBadge badge: Int) {
        delegate?.notificare(self, didUpdateBadge: badge)
    }
}

@objc
public protocol NotificareInboxNativeBindingDelegate : NSObjectProtocol {
    func notificare(_ notificareInbox: NotificareInboxNativeBinding, didUpdateInbox items: [NotificareInboxItem])

    func notificare(_ notificareInbox: NotificareInboxNativeBinding, didUpdateBadge badge: Int)
}
