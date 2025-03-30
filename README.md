# UbiApp - Cross-Platform VoIP Communication Application

## Overview
UbiApp is a cross-platform communication application designed to distribute telephone service in a help desk center, transfer VoIP calls to an attendant, and transcribe conversations so that AI systems can assist the attendant during the service. It performs sentiment analysis of calls and provides other assistance features. It was developed in C# using the Xamarin.Forms framework. The application offers VoIP (Voice over IP) telephony functionalities, messaging, dashboard, and social features, allowing users to communicate through voice calls, video conferences, and text messages.

## Development Period
2018-12-12 to 2019-09-11

## Main Features
- VoIP voice calls
- Video calls
- Text messaging
- Real-time chat
- Dashboard for data visualization
- Social features (user profiles)
- Support for multiple languages (including Arabic)
- Customizable themes

## Technologies Used

### Programming Languages
- **C#**: Main language for application development
- **XAML**: Used for user interface definition

### Frameworks and Platforms
- **Xamarin.Forms**: Framework for cross-platform development (iOS and Android)
- **Xamarin.Android**: Android-specific implementation
- **Xamarin.iOS**: iOS-specific implementation
- **.NET Standard 2.0**: Shared class library

### VoIP Telephony Library
- **Liblinphone**: C++ library for VoIP communication, integrated via Xamarin binding
- **LinphoneXamarin**: C# wrapper for the Liblinphone library

### Database
- **Realm**: Object-oriented NoSQL database for local storage

### Persistence Layer
- **Realm**: Used for local data persistence
- **Repository Pattern**: Implemented for data access

### UI/UX
- **UXDivers.Grial**: Framework for advanced UI components
- **Xamarin.FFImageLoading**: Library for efficient image loading and caching
- **Microcharts**: Library for data visualization in charts
- **CarouselView.FormsPlugin**: Component for carousel display
- **Rg.Plugins.Popup**: Library for displaying popups
- **Xamanimation**: Library for interface animations

### Testing Methodology
- **xUnit**: Framework for unit tests
- **Moq**: Framework for creating mocks in tests
- **Integration Tests**: Implemented in the XUnitTestModelntegration.cs project
- **Unit Tests**: Implemented in the XUnitTestModel project

## Architecture and Design Patterns
- **MVVM (Model-View-ViewModel)**: Main architectural pattern
- **Dependency Injection**: Used for dependency injection
- **Repository Pattern**: For data access
- **Factory Pattern**: For object creation
- **Observer Pattern**: For notifications and events

## Project Structure
- **Ubi**: Main project with business logic and shared UI
- **Ubi.Droid**: Android-specific implementation
- **Ubi.iOS**: iOS-specific implementation
- **UbiModel**: Shared data models
- **Liblinphone**: Binding of the Liblinphone library for Xamarin
- **LinphoneXamarin**: Shared code for integration with Liblinphone
- **XUnitTestModel**: Unit tests
- **XUnitTestModelntegration.cs**: Integration tests

## Main Modules
1. **VoIP Telephony**
   - Voice calls
   - Video calls
   - Call transfer
   - Contact management

2. **Messages**
   - Real-time chat
   - Emoji support
   - Notifications

3. **Dashboard**
   - Data visualization
   - Charts and statistics

4. **Social**
   - User profiles
   - Status updates

5. **Onboarding**
   - Welcome screens
   - Tutorial for new users

## Code Highlights

### Integration with Liblinphone
The application uses the Liblinphone library to implement VoIP functionalities, offering a high-quality call experience. Integration is done through a C# wrapper that facilitates the use of the native library.

### MVVM Architecture
The project follows the MVVM (Model-View-ViewModel) pattern, clearly separating business logic from the user interface, which facilitates maintenance and testing.

### Comprehensive Tests
The application has a suite of unit and integration tests using xUnit and Moq, ensuring code quality and stability.

### Multiple Language Support
The application supports multiple languages, including Arabic, with integrated localization features.

### Customizable Themes
The application offers various visual themes that can be changed by the user, including light and dark themes.

## Code Quality
- **Organization**: The code is well organized in logical projects and namespaces
- **Modularity**: Components are modularized to facilitate maintenance
- **Testability**: The MVVM architecture and dependency injection facilitate testing
- **Documentation**: Comments and documentation in critical parts of the code
- **Patterns**: Consistent use of design patterns and best practices

## Improvement Opportunities
- Some unit tests are incomplete (methods with NotImplementedException)
- Some parts of the code contain TODOs indicating features to be implemented
- Greater test coverage would be beneficial to ensure application stability

## Conclusion
UbiApp is a robust and well-structured communication application, developed with modern technologies and following good development practices. The combination of Xamarin.Forms with the Liblinphone library allows creating a high-quality VoIP communication experience on multiple platforms.

The project demonstrates a well-thought-out architecture, with clear separation of responsibilities and use of appropriate design patterns. The inclusion of unit and integration tests indicates a commitment to code quality and application stability.
