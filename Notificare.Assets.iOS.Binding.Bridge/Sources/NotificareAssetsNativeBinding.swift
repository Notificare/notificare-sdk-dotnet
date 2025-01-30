import Foundation
import NotificareKit
import NotificareAssetsKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareAssetsNativeBinding)
public class NotificareAssetsNativeBinding : NSObject {

    @objc
    public func fetch(_ group: String, _ onSuccess: @escaping SuccessBlock<[NotificareAsset]>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.assets().fetch(group: group) { result in
            switch result {
            case let .success(assets):
                onSuccess(assets.map { NotificareAsset(from: $0) })
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}
