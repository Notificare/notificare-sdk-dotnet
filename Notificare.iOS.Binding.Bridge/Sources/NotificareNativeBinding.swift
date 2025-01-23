import Foundation
import NotificareKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareNativeBinding)
public class NotificareNativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.delegate = self
    }

    // MARK: - Notificare

    @objc
    public var isConfigured: Bool {
        Notificare.shared.isConfigured
    }

    @objc
    public var isReady: Bool {
        Notificare.shared.isReady
    }

    @objc
    public var application: NotificareApplication? {
        Notificare.shared.application.map { NotificareApplication(from: $0) }
    }

    @objc
    public var canEvaluateDeferredLink: Bool {
        Notificare.shared.canEvaluateDeferredLink
    }

    @objc
    public weak var delegate: NotificareNativeBindingDelegate?

    @objc
    public func configure() {
        Notificare.shared.configure()
    }

    @objc
    public func configure(applicationKey: String, applicationSecret: String) {
        Notificare.shared.configure(
            servicesInfo: NotificareKit.NotificareServicesInfo(
                applicationKey: applicationKey,
                applicationSecret: applicationSecret
            )
        )
    }

    @objc
    public func launch(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.launch { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func unlaunch(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.unlaunch { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchApplication(_ onSuccess: @escaping SuccessBlock<NotificareApplication>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.fetchApplication { result in
            switch result {
            case let .success(application):
                onSuccess(NotificareApplication(from: application))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchNotification(_ id: String, _ onSuccess: @escaping SuccessBlock<NotificareNotification>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.fetchNotification(id) { result in
            switch result {
            case let .success(notification):
                onSuccess(NotificareNotification(from: notification))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchDynamicLink(_ url: String, _ onSuccess: @escaping SuccessBlock<NotificareDynamicLink>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.fetchDynamicLink(url) { result in
            switch result {
            case let .success(link):
                onSuccess(NotificareDynamicLink(from: link))
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func evaluateDeferredLink(_ onSuccess: @escaping SuccessBlock<Bool>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.evaluateDeferredLink { result in
            switch result {
            case let .success(evaluated):
                onSuccess(evaluated)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    // MARK: - Notificare Device

    @objc
    public var currentDevice: NotificareDevice? {
        return Notificare.shared.device().currentDevice.map { NotificareDevice(from: $0) }
    }

    @objc
    public func updateUser(userId: String?, userName: String?, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().updateUser(userId: userId, userName: userName) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchTags(_ onSuccess: @escaping SuccessBlock<[String]>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().fetchTags { result in
            switch result {
            case let .success(tags):
                onSuccess(tags)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func addTag(_ tag: String, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().addTag(tag) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func addTags(_ tags: [String], _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().addTags(tags) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func removeTag(_ tag: String, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().removeTag(tag) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func removeTags(_ tags: [String], _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().removeTags(tags) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func clearTags(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().clearTags { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public var preferredLanguage: String? {
        Notificare.shared.device().preferredLanguage
    }

    @objc
    public func updatePreferredLanguage(_ preferredLanguage: String?, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().updatePreferredLanguage(preferredLanguage) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchDoNotDisturb(_ onSuccess: @escaping SuccessBlock<NotificareDoNotDisturb?>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().fetchDoNotDisturb { result in
            switch result {
            case let .success(dnd):
                onSuccess(dnd.map { NotificareDoNotDisturb(from: $0) })
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func updateDoNotDisturb(_ dnd: NotificareDoNotDisturb, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        do {
            Notificare.shared.device().updateDoNotDisturb(try dnd.toNative()) { result in
                switch result {
                case .success:
                    onSuccess()
                case let .failure(error):
                    onFailure(error)
                }
            }
        } catch {
            onFailure(error)
        }
    }

    @objc
    public func clearDoNotDisturb(_ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().clearDoNotDisturb { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func fetchUserData(_ onSuccess: @escaping SuccessBlock<[String : String]>, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().fetchUserData { result in
            switch result {
            case let .success(userData):
                onSuccess(userData)
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    @objc
    public func updateUserData(_ userData: [String : String], _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.device().updateUserData(userData) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }

    // MARK: - Notificare Events

    @objc
    public func logCustom(_ eventName: String, data: [String : Any]?, _ onSuccess: @escaping VoidBlock, _ onFailure: @escaping ErrorBlock) {
        Notificare.shared.events().logCustom(eventName, data: data) { result in
            switch result {
            case .success:
                onSuccess()
            case let .failure(error):
                onFailure(error)
            }
        }
    }
}

extension NotificareNativeBinding : NotificareDelegate {
    public func notificare(_ notificare: NotificareKit.Notificare, onReady application: NotificareKit.NotificareApplication) {
        delegate?.notificare(self, onReady: NotificareApplication(from: application))
    }

    public func notificareDidUnlaunch(_ notificare: Notificare) {
        delegate?.notificareDidUnlaunch(self)
    }

    public func notificare(_ notificare: NotificareKit.Notificare, didRegisterDevice device: NotificareKit.NotificareDevice) {
        delegate?.notificare(self, didRegisterDevice: NotificareDevice(from: device))
    }
}

@objc
public protocol NotificareNativeBindingDelegate : NSObjectProtocol {
    func notificare(_ notificare: NotificareNativeBinding, onReady application: NotificareApplication)

    func notificareDidUnlaunch(_ notificare: NotificareNativeBinding)

    func notificare(_ notificare: NotificareNativeBinding, didRegisterDevice device: NotificareDevice)
}
