import Foundation
import NotificareNotificationServiceExtensionKit
import UserNotifications

public typealias SuccessBlock<T> = (T) -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareNotificationServiceExtensionNativeBinding)
public class NotificareNotificationServiceExtensionNativeBinding : NSObject {

    @objc
    public static func handleNotificationRequest(_ request: UNNotificationRequest, _ onSuccess: @escaping SuccessBlock<UNNotificationContent>, _ onFailure: @escaping ErrorBlock) {
        NotificareNotificationServiceExtension.handleNotificationRequest(request) { result in
            switch result {
            case let .success(content):
                onSuccess(content)

            case let .failure(error):
                onFailure(error)
            }
        }
    }
}
