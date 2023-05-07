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
## Tips
1. You can press keys on the main window, it will print your current keys combination.
2. "Copy keys" button will copy your current display key combination into your clipboard (CTRL+C).
3. "Auth refresh" checkbox changes the display key combination behaviour. If enabled then it will display the exactly keys you are pressing, so if you stop pressing it then it will be out. 

## Warning
**Modifier keys (CTRL+C, ALT+Z) are harder to reproduce due to Windows O.S. behaviour.**
