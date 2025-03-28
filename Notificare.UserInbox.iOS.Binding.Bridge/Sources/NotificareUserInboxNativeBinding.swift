import Foundation
import NotificareKit
import NotificareUserInboxKit
import NotificareBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareUserInboxNativeBinding)
public class NotificareUserInboxNativeBinding : NSObject {

    @objc
    public func parseResponseFromString(_ str: String, _ onSuccess: @escaping SuccessBlock<NotificareUserInboxResponse>, _ onFailure: @escaping ErrorBlock) {
        do {
            let response = try Notificare.shared.userInbox().parseResponse(string: str)
            onSuccess(NotificareUserInboxResponse(from: response))
        } catch {
            onFailure(error)
        }
    }

    @objc
    public func open(_ item: NotificareUserInboxItem, _ onSuccess: @escaping SuccessBlock<NotificareBinding.NotificareNotification>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.userInbox().open(item.toNative()) { result in
            switch result {
            case let .success(notification):
                onSuccess(NotificareNotification(from: notification))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func markAsRead(_ item: NotificareUserInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.userInbox().markAsRead(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func remove(_ item: NotificareUserInboxItem, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.userInbox().remove(item.toNative()) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}
