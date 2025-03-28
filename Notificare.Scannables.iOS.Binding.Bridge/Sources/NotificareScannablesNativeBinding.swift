import Foundation
import UIKit
import NotificareKit
import NotificareScannablesKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareScannablesNativeBinding)
public class NotificareScannablesNativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.scannables().delegate = self
    }

    @objc
    public var canStartNfcScannableSession: Bool {
        Notificare.shared.scannables().canStartNfcScannableSession
    }

    @objc
    public weak var delegate: NotificareScannablesNativeBindingDelegate?

    @objc
    public func startScannableSession(_ controller: UIViewController) {
        Notificare.shared.scannables().startScannableSession(controller: controller)
    }

    @objc
    public func startNfcScannableSession() {
        Notificare.shared.scannables().startNfcScannableSession()
    }

    @objc
    public func startQrCodeScannableSession(_ controller: UIViewController, modal: Bool) {
        Notificare.shared.scannables().startQrCodeScannableSession(controller: controller, modal: modal)
    }

    @objc
    public func fetch(_ tag: String, _ onSuccess: @escaping SuccessBlock<NotificareScannable>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.scannables().fetch(tag: tag) { result in
            switch result {
            case let .success(scannable):
                onSuccess(NotificareScannable(from: scannable))
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension NotificareScannablesNativeBinding : NotificareScannablesDelegate {
    public func notificare(_ notificareScannables: any NotificareScannablesKit.NotificareScannables, didInvalidateScannerSession error: any Error) {
        delegate?.notificare(self, didInvalidateScannerSession: error)
    }

    public func notificare(_ notificareScannables: any NotificareScannablesKit.NotificareScannables, didDetectScannable scannable: NotificareScannablesKit.NotificareScannable) {
        delegate?.notificare(self, didDetectScannable: NotificareScannable(from: scannable))
    }
}

@objc
public protocol NotificareScannablesNativeBindingDelegate : NSObjectProtocol {
    func notificare(_ notificareScannables: NotificareScannablesNativeBinding, didInvalidateScannerSession error: any Error)

    func notificare(_ notificareScannables: NotificareScannablesNativeBinding, didDetectScannable scannable: NotificareScannable)
}
