- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

  _______                                  _______         _       _              
 |__   __|                                |__   __|       (_)     | |             
    | |  ___   _ __    __ _  _   _   ___     | |__      __ _  ___ | |_  ___  _ __ 
    | | / _ \ | '_ \  / _` || | | | / _ \    | |\ \ /\ / /| |/ __|| __|/ _ \| '__|
    | || (_) || | | || (_| || |_| ||  __/    | | \ V  V / | |\__ \| |_|  __/| |   
    |_| \___/ |_| |_| \__, | \__,_| \___|    |_|  \_/\_/  |_||___/ \__|\___||_|   
                       __/ |                                                      
                      |___/                                                       

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

ReadMe.txt
Version 1.3
Last Updated Mar. 31st, 2021


 _ _ _ _ _ _ _ _ _ _ _ _ _ 
/ Introduction             \
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

Thanks for using Tongue Twister! A localization and locale management project that's been in development since 2017.

Project documentation can be found here:
https://www.austephner.com/unity-assets/tongue-twister/index.html


 _ _ _ _ _ _ _ _ _ _ _ _ _ 
/ Changelog                \
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    + 1.3.0 - Mar. 31st, 2021
        - Updated TongueTwister window UI
        - Made the Locale's GUID visible/copyable in the locale editor
        - Refactored locale retrieval in LocalizationManager (more functions added, others made obsolete)
        - Fixed issue where TT icon wouldn't appear in window title until after building/compiling
        - Adjusted the "Home" / "About" window mode section's UI layout
        - Added announcements to help keep users in the loop about new tutorials and upcoming changes
        - Adjusted link font color
        - Fixed localization "Format" function to use "params"
        - Added localization image behaviour
        - Changed some documentation wording

    + 1.2.1 - Mar. 10th, 2021
        - Fixed "undo" issue with localization manager selection
        - Fixed broken layout when filtering localizations from DKE
        - Made texture icon for localizations left align instead of right align
        - Fixed localization's "Unity Object" field not allowing scene items but the ALC items were allowing scene items
        - Fixed localization manager's locale collection being reverted when a new default locale is assigned
        - Fixed debug issue while viewing a localization with no locale assigned
        - Removed some redundant locale editor code to reduce call stack size and execution time (performance boost)
        - Locale editor "display name" column now has much larger default width
        - Fixed issue where changing a locale's name wasn't reflected upon localizations using the locale

    + 1.2.0 - Mar. 4th, 2021
        - Extended localization data to include a configurable array of unity objects
        - Made some LocalizationManager accessors/getters obsolete as code moves to StringExtensions
        - Refactored some StringExtensions functions to rely less on LocalizationManager code
        - Added more StringExtensions functions to include the new additional localized content data
        - Fixed version label (wasn't updated in 1.1, oops)
        - Made child DK's, Groups, and Localizations appear better within the inline editor by indexing them 
        - Users can now hide the "create prefab" warning when editing a scene object that isn't based on a prefab
        - Validation rules now have a name/description which can be displayed as seen fit
        - Validation rules can now be enabled/disabled so users can pick and choose what errors and warnings they see
        - Changed some wording/naming of validation rules to make it more obvious what they do

    + 1.1.0 - Feb. 2nd, 2021
        - Major code clean up
        - Removed some stale and inefficient code
        - Refactored some processes and LocalizationManager algorithms
        - Improved the locale editor
        - Consolidated tools with imports and updated the presentation of tool UI
        - Fixed a "default locale" setting issue
        - Added the ISO Localization Code window
        - Major changes to locale serialization field naming schemes
        - Locales now contain "metadata" objects instead of holding their own properties
        - Major data structuring change to provide better OOP design
        - Some improvements to merging localization dictionaries
        - Some improvements to the CSV Importer
        - Fixed issue with editor locales not updating after merging a localization dictionary

    + 1.0.0 - Nov. 10th, 2020
        - First Version

 _ _ _ _ _ _ _ _ _ _ _ _ _ 
/ License Usage            \
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

As per the Unity Store Asset licensing system, one copy of TongueTwister is required for every user / team member.

If multiple users interact with TongueTwister in one project, please ensure each have acquired a license.


 _ _ _ _ _ _ _ _ _ _ _ _ _ 
/ Credits                  \
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

Lead Developer: Austin Renner
www.austephner.com

- Special thanks to my family and friends for pushing me to work so hard. 
- Especially Akram El Hadri, for your extraordinary advice.
- David for your consistent patience and excellent feedback.
- ASCII art from http://patorjk.com/ - "Big Font"

Copy Right © 2021 Austephner LLC. All rights reserved.