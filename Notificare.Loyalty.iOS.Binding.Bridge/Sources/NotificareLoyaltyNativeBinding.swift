import Foundation
import UIKit
import NotificareKit
import NotificareLoyaltyKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareLoyaltyNativeBinding)
public class NotificareLoyaltyNativeBinding : NSObject {

    @objc
    public func fetchPassBySerial(_ serial: String, _ onSuccess: @escaping SuccessBlock<NotificarePass>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.loyalty().fetchPass(serial: serial) { result in
            switch result {
            case let .success(pass):
                onSuccess(NotificarePass(from: pass))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchPassByBarcode(_ barcode: String, _ onSuccess: @escaping SuccessBlock<NotificarePass>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.loyalty().fetchPass(barcode: barcode) { result in
            switch result {
            case let .success(pass):
                onSuccess(NotificarePass(from: pass))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func present(_ pass: NotificarePass, in controller: UIViewController) {
        Notificare.shared.loyalty().present(pass: pass.toNative(), in: controller)
    }
}
