# CHANGELOG

## Upcoming release

- Add required referrer for embedded YouTube videos

## 4.2.3

#### Native changes

##### Android

- Update dependencies

## 4.2.2

#### Native changes

##### iOS

- Fix Swift Concurrency issues when synchronizing inbox items

## 4.2.1

#### Native changes

##### iOS

- Refactor the integration with CoreData, fixing threading fatal errors

## 4.2.0

- Recover when a device is removed remotely

#### Native changes

##### iOS

- Reload action categories during launch

## 4.1.0

- Allow unsetting user data fields

#### Native changes

##### Android

- Warnings related to missing requirements for Beacons functionality only logged once during `enableLocationUpdates()` flow
- Fix UI behaviour after screen rotation when presenting notifications
- Fix crash when rotating the screen when presenting a `NotificareCallbackActionFragment`
- Update compile SDK to 35

##### iOS

- Fix issue where the notification UI was not dismissed when some actions were executed
- Fix callback with keyboard safe area insets
- Improve CoreData threading
- Change NotificareRegion.timeZoneOffset attribute from int to float to support half-hour & 45 minute timezones
