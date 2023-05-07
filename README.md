# RemapKeyboard
Application to remap keyboard keys or create text macros.
This application is useful for me, because i'm using a 75% mechanical keyboard with US layout and Brasil ABNT2 on Windows. So I need this to reproduce pipe ("|") and backslash ("\\") to code. xD

I'm currently out of time to develop a proper GUI, so you should place a file called "hotkeys.xml" on application's folder, which must use this pattern. So the application will load that file to hotkeys and remap.
```
<Hotkeys>
	<Hotkey type="text" keys="LCONTROL+RMENU+KEY_Z" value="|" />
	<Hotkey type="keypress" keys="LCONTROL+RMENU+RSHIFT+KEY_C" value="CONTROL+KEY_C" />
</Hotkeys>
```
**Modifier keys (CTRL+C) are bit harder to reproduce.**
