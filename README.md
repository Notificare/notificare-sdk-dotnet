[<img src="https://raw.githubusercontent.com/notificare/notificare-sdk-dotnet/main/assets/logo.png"/>](https://notificare.com)

# Notificare .NET MAUI SDK

[![GitHub release](https://img.shields.io/github/v/release/notificare/notificare-sdk-dotnet)](https://github.com/notificare/notificare-sdk-dotnet/releases)
[![License](https://img.shields.io/github/license/notificare/notificare-sdk-dotnet)](https://github.com/notificare/notificare-sdk-dotnet/blob/main/LICENSE)

The Notificare .NET MAUI SDK makes it quick and easy to communicate efficiently with many of the Notificare API services and enables you to seamlessly integrate our various features, from Push Notifications to Contextualised Storage.

Get started with our [📚 integration guides](https://docs.notifica.re/sdk/v4/dotnet/setup) and [example projects](#examples).


Table of contents
=================

* [Features](#features)
* [Installation](#installation)
    * [Requirements](#requirements)
    * [Configuration](#configuration)
* [Getting Started](#getting-started)
* [Examples](#examples)


## Features

**Push notifications**: Receive push notifications and automatically track its engagement.

**Push notifications UI**: Use native screens and elements to display your push notifications and handle its actions with zero effort.

**In-app messaging**: Automatically show relevant in-app content to your users with zero effort.

**Inbox**: Apps with a built-in message inbox enjoy higher conversions due to its nature of keeping messages around that can be opened as many times as users want. The SDK gives you all the tools necessary to build your inbox UI.

**Geo**: Transform your user's location into relevant information, automate how you segment your users based on location behaviour and create truly contextual notifications.

**Loyalty**: Harness the power of digital cards that live beyond your app and are always in your customer’s pocket.

**Assets**: Add powerful contextual marketing features to your apps. Show the right content to the right users at the right time or location. Maximise the content you're already creating without increasing development costs.

**Scannables**: Unlock new content by scanning NFC tags or QR codes that integrate seamlessly in your mobile applications.


## Installation

### Requirements

* Android 6 (API level 23) and above
* iOS 13 and above

### Configuration

Add the .NET packages to your `pubspec.yaml` and follow the Getting Started guide.

```bash
# Required
dotnet add package Notificare

# Optional modules
dotnet add package Notificare.Assets
dotnet add package Notificare.Geo
dotnet add package Notificare.Inbox
dotnet add package Notificare.Loyalty
dotnet add package Notificare.Push
dotnet add package Notificare.Push.UI
dotnet add package Notificare.Scannables
dotnet add package Notificare.UserInbox
```

## Getting Started

### Integration
Get started with our [📚 integration guides](https://docs.notifica.re/sdk/v4/dotnet/setup) and [example projects](#examples).


### Examples
- The [example project](https://github.com/Notificare/notificare-sdk-dotnet/tree/main/Sample) demonstrates other integrations in a simplified fashion, to quickly understand how a given feature should be implemented.
