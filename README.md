# SkypeXmlController

This windows based tools written in VB.net is designed to control skype from another application (especially HTPC softwares).

It includes 3 main features
- Maintain XML files with friends / profile / missed calls
- Ability to create a new call, accept a call, end a call
- Skype window wrapper to display skype in fullscreen ONLY where a call is active

## Windows App

The windows app displays a notification icon (skull head)

## Skype directory 

Once the app is launched, it keeps up to date 3 files : 
- var\skype2kodi\friends.xml : contains connected friends list with handle, fullname and avatar imagepath
- var\skype2kodi\profile.xml : contains user information such as handle, fullname, and avatar image path
- var\skype2kodi\missed_calls.xml : contains missed calls
- var\skype2kodi\call.xml : available ONLY if a call is pending / ringing / active
 
## Skype action

To send a command to skype, you must create file  var\kodi2skype\action.xml :

<?xml version="1.0" encoding="utf-8"?>
  <action><method>X</method>
  <param>Y</param>
</action>

Available actions are : 
- call_friend : param contains the friend handle
- call_accept : no param, accept a call
- call_end : no param, end or refuse a call

## Skype window

When you launch the app, it catches and hide the skype windows
On end, it releases the skype windows
You can manually release the skype windows using the notificatin icon action : SkypeWindow > Release
When a call starts, the skype window is automatically displayed in fullscreen at the first position
When a call stops, the skype window is hidded

## Change log

2016-02-07 : first release

