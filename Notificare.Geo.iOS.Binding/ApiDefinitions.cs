using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace NotificareSdk.Geo.iOS.Binding
{
	// @interface NotificareBeacon : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding16NotificareBeacon")]
	[DisableDefaultCtor]
	interface NotificareBeacon
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull beaconId;
		[Export ("beaconId")]
		string BeaconId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSInteger major;
		[Export ("major")]
		nint Major { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable minor;
		[NullAllowed, Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic) BOOL triggers;
		[Export ("triggers")]
		bool Triggers { get; }

		// @property (readonly, nonatomic) enum NotificareBeaconProximity proximity;
		[Export ("proximity")]
		NotificareBeaconProximity Proximity { get; }

		// -(instancetype _Nonnull)initWithBeaconId:(NSString * _Nonnull)beaconId name:(NSString * _Nonnull)name major:(NSInteger)major minor:(NSNumber * _Nullable)minor triggers:(BOOL)triggers proximity:(enum NotificareBeaconProximity)proximity __attribute__((objc_designated_initializer));
		[Export ("initWithBeaconId:name:major:minor:triggers:proximity:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string beaconId, string name, nint major, [NullAllowed] NSNumber minor, bool triggers, NotificareBeaconProximity proximity);
	}

	// @interface NotificareGeoNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface NotificareGeoNativeBinding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		NotificareGeoNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<NotificareGeoNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, nonatomic) BOOL hasLocationServicesEnabled;
		[Export ("hasLocationServicesEnabled")]
		bool HasLocationServicesEnabled { get; }

		// @property (readonly, nonatomic) BOOL hasBluetoothEnabled;
		[Export ("hasBluetoothEnabled")]
		bool HasBluetoothEnabled { get; }

		// @property (readonly, copy, nonatomic) NSArray<NotificareRegion *> * _Nonnull monitoredRegions;
		[Export ("monitoredRegions", ArgumentSemantic.Copy)]
		NotificareRegion[] MonitoredRegions { get; }

		// @property (readonly, copy, nonatomic) NSArray<NotificareRegion *> * _Nonnull enteredRegions;
		[Export ("enteredRegions", ArgumentSemantic.Copy)]
		NotificareRegion[] EnteredRegions { get; }

		// -(void)enableLocationUpdates;
		[Export ("enableLocationUpdates")]
		void EnableLocationUpdates ();

		// -(void)disableLocationUpdates;
		[Export ("disableLocationUpdates")]
		void DisableLocationUpdates ();
	}

	// @protocol NotificareGeoNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP20NotificareGeoBinding34NotificareGeoNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP20NotificareGeoBinding34NotificareGeoNativeBindingDelegate_")]
	interface NotificareGeoNativeBindingDelegate
	{
		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didUpdateLocations:(NSArray<NotificareLocation *> * _Nonnull)locations;
		[Abstract]
		[Export ("notificare:didUpdateLocations:")]
		void DidUpdateLocations (NotificareGeoNativeBinding notificareGeo, NotificareLocation[] locations);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didFailWith:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("notificare:didFailWith:")]
		void DidFailWith (NotificareGeoNativeBinding notificareGeo, NSError error);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didStartMonitoringForRegion:(NotificareRegion * _Nonnull)region;
		[Abstract]
		[Export ("notificare:didStartMonitoringForRegion:")]
		void DidStartMonitoringForRegion (NotificareGeoNativeBinding notificareGeo, NotificareRegion region);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didStartMonitoringForBeacon:(NotificareBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("notificare:didStartMonitoringForBeacon:")]
		void DidStartMonitoringForBeacon (NotificareGeoNativeBinding notificareGeo, NotificareBeacon beacon);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo monitoringDidFailForRegion:(NotificareRegion * _Nonnull)region with:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("notificare:monitoringDidFailForRegion:with:")]
		void MonitoringDidFailForRegion (NotificareGeoNativeBinding notificareGeo, NotificareRegion region, NSError error);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo monitoringDidFailForBeacon:(NotificareBeacon * _Nonnull)beacon with:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("notificare:monitoringDidFailForBeacon:with:")]
		void MonitoringDidFailForBeacon (NotificareGeoNativeBinding notificareGeo, NotificareBeacon beacon, NSError error);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didDetermineState:(CLRegionState)state forRegion:(NotificareRegion * _Nonnull)region;
		[Abstract]
		[Export ("notificare:didDetermineState:forRegion:")]
		void DidDetermineState (NotificareGeoNativeBinding notificareGeo, CLRegionState state, NotificareRegion region);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didDetermineState:(CLRegionState)state forBeacon:(NotificareBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("notificare:didDetermineState:forBeacon:")]
		void DidDetermineState (NotificareGeoNativeBinding notificareGeo, CLRegionState state, NotificareBeacon beacon);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didEnterRegion:(NotificareRegion * _Nonnull)region;
		[Abstract]
		[Export ("notificare:didEnterRegion:")]
		void DidEnterRegion (NotificareGeoNativeBinding notificareGeo, NotificareRegion region);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didEnterBeacon:(NotificareBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("notificare:didEnterBeacon:")]
		void DidEnterBeacon (NotificareGeoNativeBinding notificareGeo, NotificareBeacon beacon);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didExitRegion:(NotificareRegion * _Nonnull)region;
		[Abstract]
		[Export ("notificare:didExitRegion:")]
		void DidExitRegion (NotificareGeoNativeBinding notificareGeo, NotificareRegion region);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didExitBeacon:(NotificareBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("notificare:didExitBeacon:")]
		void DidExitBeacon (NotificareGeoNativeBinding notificareGeo, NotificareBeacon beacon);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didVisit:(NotificareVisit * _Nonnull)visit;
		[Abstract]
		[Export ("notificare:didVisit:")]
		void DidVisit (NotificareGeoNativeBinding notificareGeo, NotificareVisit visit);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didUpdateHeading:(NotificareHeading * _Nonnull)heading;
		[Abstract]
		[Export ("notificare:didUpdateHeading:")]
		void DidUpdateHeading (NotificareGeoNativeBinding notificareGeo, NotificareHeading heading);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didRange:(NSArray<NotificareBeacon *> * _Nonnull)beacons in:(NotificareRegion * _Nonnull)region;
		[Abstract]
		[Export ("notificare:didRange:in:")]
		void DidRange (NotificareGeoNativeBinding notificareGeo, NotificareBeacon[] beacons, NotificareRegion region);

		// @required -(void)notificare:(NotificareGeoNativeBinding * _Nonnull)notificareGeo didFailRangingFor:(NotificareRegion * _Nonnull)region with:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("notificare:didFailRangingFor:with:")]
		void DidFailRangingFor (NotificareGeoNativeBinding notificareGeo, NotificareRegion region, NSError error);
	}

	// @interface NotificareHeading : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding17NotificareHeading")]
	[DisableDefaultCtor]
	interface NotificareHeading
	{
		// @property (readonly, nonatomic) double magneticHeading;
		[Export ("magneticHeading")]
		double MagneticHeading { get; }

		// @property (readonly, nonatomic) double trueHeading;
		[Export ("trueHeading")]
		double TrueHeading { get; }

		// @property (readonly, nonatomic) double headingAccuracy;
		[Export ("headingAccuracy")]
		double HeadingAccuracy { get; }

		// @property (readonly, nonatomic) double x;
		[Export ("x")]
		double X { get; }

		// @property (readonly, nonatomic) double y;
		[Export ("y")]
		double Y { get; }

		// @property (readonly, nonatomic) double z;
		[Export ("z")]
		double Z { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull timestamp;
		[Export ("timestamp", ArgumentSemantic.Copy)]
		NSDate Timestamp { get; }

		// -(instancetype _Nonnull)initWithMagneticHeading:(double)magneticHeading trueHeading:(double)trueHeading headingAccuracy:(double)headingAccuracy x:(double)x y:(double)y z:(double)z timestamp:(NSDate * _Nonnull)timestamp __attribute__((objc_designated_initializer));
		[Export ("initWithMagneticHeading:trueHeading:headingAccuracy:x:y:z:timestamp:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double magneticHeading, double trueHeading, double headingAccuracy, double x, double y, double z, NSDate timestamp);
	}

	// @interface NotificareLocation : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding18NotificareLocation")]
	[DisableDefaultCtor]
	interface NotificareLocation
	{
		// @property (readonly, nonatomic) double latitude;
		[Export ("latitude")]
		double Latitude { get; }

		// @property (readonly, nonatomic) double longitude;
		[Export ("longitude")]
		double Longitude { get; }

		// @property (readonly, nonatomic) double altitude;
		[Export ("altitude")]
		double Altitude { get; }

		// @property (readonly, nonatomic) double course;
		[Export ("course")]
		double Course { get; }

		// @property (readonly, nonatomic) double speed;
		[Export ("speed")]
		double Speed { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable floor;
		[NullAllowed, Export ("floor", ArgumentSemantic.Strong)]
		NSNumber Floor { get; }

		// @property (readonly, nonatomic) double horizontalAccuracy;
		[Export ("horizontalAccuracy")]
		double HorizontalAccuracy { get; }

		// @property (readonly, nonatomic) double verticalAccuracy;
		[Export ("verticalAccuracy")]
		double VerticalAccuracy { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull timestamp;
		[Export ("timestamp", ArgumentSemantic.Copy)]
		NSDate Timestamp { get; }

		// -(instancetype _Nonnull)initWithLatitude:(double)latitude longitude:(double)longitude altitude:(double)altitude course:(double)course speed:(double)speed floor:(NSNumber * _Nullable)floor horizontalAccuracy:(double)horizontalAccuracy verticalAccuracy:(double)verticalAccuracy timestamp:(NSDate * _Nonnull)timestamp __attribute__((objc_designated_initializer));
		[Export ("initWithLatitude:longitude:altitude:course:speed:floor:horizontalAccuracy:verticalAccuracy:timestamp:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double latitude, double longitude, double altitude, double course, double speed, [NullAllowed] NSNumber floor, double horizontalAccuracy, double verticalAccuracy, NSDate timestamp);
	}

	// @interface NotificareRegion : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding16NotificareRegion")]
	[DisableDefaultCtor]
	interface NotificareRegion
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull regionId;
		[Export ("regionId")]
		string RegionId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable regionDescription;
		[NullAllowed, Export ("regionDescription")]
		string RegionDescription { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable referenceKey;
		[NullAllowed, Export ("referenceKey")]
		string ReferenceKey { get; }

		// @property (readonly, nonatomic, strong) NotificareRegionGeometry * _Nonnull geometry;
		[Export ("geometry", ArgumentSemantic.Strong)]
		NotificareRegionGeometry Geometry { get; }

		// @property (readonly, nonatomic, strong) NotificareRegionAdvancedGeometry * _Nullable advancedGeometry;
		[NullAllowed, Export ("advancedGeometry", ArgumentSemantic.Strong)]
		NotificareRegionAdvancedGeometry AdvancedGeometry { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable major;
		[NullAllowed, Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; }

		// @property (readonly, nonatomic) double distance;
		[Export ("distance")]
		double Distance { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull timeZone;
		[Export ("timeZone")]
		string TimeZone { get; }

		// @property (readonly, nonatomic) double timeZoneOffset;
		[Export ("timeZoneOffset")]
		double TimeZoneOffset { get; }

		// -(instancetype _Nonnull)initWithRegionId:(NSString * _Nonnull)regionId name:(NSString * _Nonnull)name regionDescription:(NSString * _Nullable)regionDescription referenceKey:(NSString * _Nullable)referenceKey geometry:(NotificareRegionGeometry * _Nonnull)geometry advancedGeometry:(NotificareRegionAdvancedGeometry * _Nullable)advancedGeometry major:(NSNumber * _Nullable)major distance:(double)distance timeZone:(NSString * _Nonnull)timeZone timeZoneOffset:(double)timeZoneOffset __attribute__((objc_designated_initializer));
		[Export ("initWithRegionId:name:regionDescription:referenceKey:geometry:advancedGeometry:major:distance:timeZone:timeZoneOffset:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string regionId, string name, [NullAllowed] string regionDescription, [NullAllowed] string referenceKey, NotificareRegionGeometry geometry, [NullAllowed] NotificareRegionAdvancedGeometry advancedGeometry, [NullAllowed] NSNumber major, double distance, string timeZone, double timeZoneOffset);
	}

	// @interface NotificareRegionAdvancedGeometry : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding32NotificareRegionAdvancedGeometry")]
	[DisableDefaultCtor]
	interface NotificareRegionAdvancedGeometry
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSArray<NotificareRegionCoordinate *> * _Nonnull coordinates;
		[Export ("coordinates", ArgumentSemantic.Copy)]
		NotificareRegionCoordinate[] Coordinates { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type coordinates:(NSArray<NotificareRegionCoordinate *> * _Nonnull)coordinates __attribute__((objc_designated_initializer));
		[Export ("initWithType:coordinates:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, NotificareRegionCoordinate[] coordinates);
	}

	// @interface NotificareRegionCoordinate : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding26NotificareRegionCoordinate")]
	[DisableDefaultCtor]
	interface NotificareRegionCoordinate
	{
		// @property (readonly, nonatomic) double latitude;
		[Export ("latitude")]
		double Latitude { get; }

		// @property (readonly, nonatomic) double longitude;
		[Export ("longitude")]
		double Longitude { get; }

		// -(instancetype _Nonnull)initWithLatitude:(double)latitude longitude:(double)longitude __attribute__((objc_designated_initializer));
		[Export ("initWithLatitude:longitude:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double latitude, double longitude);
	}

	// @interface NotificareRegionGeometry : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding24NotificareRegionGeometry")]
	[DisableDefaultCtor]
	interface NotificareRegionGeometry
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic, strong) NotificareRegionCoordinate * _Nonnull coordinate;
		[Export ("coordinate", ArgumentSemantic.Strong)]
		NotificareRegionCoordinate Coordinate { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type coordinate:(NotificareRegionCoordinate * _Nonnull)coordinate __attribute__((objc_designated_initializer));
		[Export ("initWithType:coordinate:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, NotificareRegionCoordinate coordinate);
	}

	// @interface NotificareVisit : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC20NotificareGeoBinding15NotificareVisit")]
	[DisableDefaultCtor]
	interface NotificareVisit
	{
		// @property (readonly, copy, nonatomic) NSDate * _Nonnull departureDate;
		[Export ("departureDate", ArgumentSemantic.Copy)]
		NSDate DepartureDate { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull arrivalDate;
		[Export ("arrivalDate", ArgumentSemantic.Copy)]
		NSDate ArrivalDate { get; }

		// @property (readonly, nonatomic) double latitude;
		[Export ("latitude")]
		double Latitude { get; }

		// @property (readonly, nonatomic) double longitude;
		[Export ("longitude")]
		double Longitude { get; }

		// -(instancetype _Nonnull)initWithDepartureDate:(NSDate * _Nonnull)departureDate arrivalDate:(NSDate * _Nonnull)arrivalDate latitude:(double)latitude longitude:(double)longitude __attribute__((objc_designated_initializer));
		[Export ("initWithDepartureDate:arrivalDate:latitude:longitude:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSDate departureDate, NSDate arrivalDate, double latitude, double longitude);
	}
}
