### Meu nobreask SMS não conecta mais pela USB!!!

O que costumo fazer:

* Instalo o drive PL2303_64bit_Installer.exe que está nesse mesmo diretório. Logo alí em cima vc baixa ele.
* Conecto o cabo do nobreak e ligo ele

![image](https://user-images.githubusercontent.com/12012626/116078689-4ee1d080-a66d-11eb-88f8-a39e22a64852.png)
* No Gerenciador de dispositivo, navego em Portas COM e seleciono o Prolict USB-to-Serial que é o dispositivo do nobreak

![image](https://user-images.githubusercontent.com/12012626/116079133-d596ad80-a66d-11eb-974c-424c1491a0a1.png)
* Clique duplo nele e *Atualizar Driver*

![image](https://user-images.githubusercontent.com/12012626/116078950-a1bb8800-a66d-11eb-8c51-ce6a42cd654a.png)
* Procurar drivers no meu computador


![image](https://user-images.githubusercontent.com/12012626/116079237-ef37f500-a66d-11eb-88ca-61ad06a7d366.png)
* Permitir que eu escolha em uma lista

![image](https://user-images.githubusercontent.com/12012626/116079294-01b22e80-a66e-11eb-872d-dca5eae85652.png)
* Seleciono uma versão antiga
* Clico em continuar e concluír
* Instalo novamente o drive PL2303_64bit_Installer.exe (importante)
* Reinicio o computador
* Pronto, funciona mesmo depois da próxima atualização do windows





## Abaixo segue as fontes e dados referentes ao problema e como resolver (em inglês).

http://nilcemar.blogspot.com.br/2012/08/resolvido-usb-to-serial-prolific-no.html
e
http://www.ifamilysoftware.com/news37.html
-------------------------------------------------------------------
Prolific USB To Serial Driver Fix!

Windows 32 and 64-bit Operating Systems - Prolific PL-2303 Driver Fix (VID_067B&PID_2303)

For Windows XP, Windows 7, Windows 8, Windows 8.1 and Windows 10!

"This Device cannot start (Code 10)"

"No driver installed for this device"

"Device driver was not successfully installed"

The Internet is full of web pages discussing this problem, but no one really seems to understand what causes it. Even the companies selling these USB to Serial adapters appear to be either dumbfounded or have selective amnesia. After all, it's absolutely great for business. There are many "backyard" fixes out there, but none of them are done properly and if they work it's just a temporary "Band-Aid" to the problem. It will come back to haunt you.

What has happened is that there have been counterfeit "Prolific" chips coming from China. The counterfeit chips use the same Vendor ID (VID_067B) and Product ID (PID_2303) as the authentic Prolific chips. So, Prolific made changes to their newest drivers to render the adapters using counterfeit chips unusable. Unfortunately, it renders all earlier adapters inoperative and so you have to go out and buy new ones. Planned obsolescence? Getting a working driver installed by the average user is almost impossible.

If one of these Prolific 64-bit drivers gets installed to your computer then your legacy device will no longer work and will issue the generic "Code 10" error. Driver Versions:

3.3.5.122, 3.3.11.152, 3.3.17.203, 3.4.25.218, 3.4.31.231, 3.4.36.247
3.4.48.272, 3.4.62.293, 3.4.67.325, 3.4.67.333, 3.6.78.350, 3.6.81.357 

Or, you may get no error at all, but your device will not work. If your adapter was working prior to going to Windows Update, you may be able to "roll back" to the previous installed driver and all will be well once again. However, if you didn't have a previous driver installed that worked, you'll have to go through the process of removing any PL-2303 driver installation programs,the actual driver files, and the information (.INF) file BEFORE you are able to successfully install the correct driver. What aggravates the issue is that there are many "Prolific Driver Removal Tools" that do not work properly as well! And Windows 8, 8.1, and Window 10 are set by default to automatically update your drivers without your permission or even notifying you of the update. So, no matter how many times you remove the driver files and reboot, the next time you insert the USB-To-Serial adapter, Windows installs the newest non-working version again. Yes, it's frustrating as hell.

The only 64-bit driver I have ever found that works with all the "Prolific" adapters is Version 3.3.2.102. The below installer program will remove all of the incompatible drivers, make a change so that Windows can never update the driver without your okay, and install the Version 3.3.2.102 compatible drivers.

Windows 64-bit Fix for:
- All Windows 64-bit operating systems including Windows 10 
.Prolific USB to Serial Adapter OR other device. 
.Device using PL-2303 H, HX, HXA, HXD, X, XA, EA, RA, SA, TA, TD version chips
.Driver Version: 3.3.2.102
.Driver Date: 09/29/08
.Supported device ID and product strings: . VID_067B&PID_2303 for "Prolific USB-to-Serial Comm Port"

1. Download and Save the "PL2303_64bit_ Installer.exe" at the link below: 

http://www.ifamilysoftware.com/Drivers/PL2303_64bit_Installer.exe

You can just Save it to your Desktop to make it easier. Norton's 360 won't like it, so to save all the grief of dealing with that beast you might want to disable your anti-virus before running the installer.

2. Unplug all USB-To-Serial adapters and Double click on the installer "PL2303_64bit_Installer.exe"

3. When it prompts you, plug in one (1) of your USB-To-Serial adapters and click "Continue".

4. Reboot your computer.

5. Unplug the adapter and plug back in again. That's it!

Trouble Shooting: You must follow ever step in the process precisely. If you still receive an error after running the PL-2303_64bit_Installer.exe and your device is plugged in, go to the Windows Device Manager. Scroll down to Ports (Com & LPT) and Double-Click on "Prolific USB-to-Serial Comm Port (COM#)". In the Properties Window, Click on "Driver". The "Driver Version" must say "3.3.2.102" dated 09/24/08. If not, then the correct driver is not installed. Unplug the USB-To-Serial adapter and run the "PL2303_64bit_Installer.exe" again, following the directions precisely until you get it right. 

Removal: There is no "Uninstaller" for this file. The installer itself is just an executable ".EXE" file that can be deleted when you are done. Any additional files that it uses are temporary only and are stored in the "C:\Windows\Temp" folder.

Windows 32-bit Fix for:
- All Windows 32-bit operating systems from XP up
.Prolific USB to Serial Adapter . 
.Device using PL-2303 H/HXA/HX/X version chips 
.Driver Version: 2.0.2.8
.Driver Date: 11/20/07
.Supported device ID and product strings: . VID_067B&PID_2303 for "Prolific USB-to-Serial Comm Port"

1. Download and Save the "PL-2303_Driver_ Installer.exe" program at the link below: 

http://www.ifamilysoftware.com/Drivers/PL-2303_Driver_Installer.exe

You can just Save it to your Desktop to make things easy.

2. Run the installer program. If it offers a choice to remove the driver, then select to remove the current "bad" driver. Then run the installer again to install the correct driver. 

Trouble Shooting: If you still receive an error after running the PL-2303_Driver_Installer.exe and your device is plugged in, go to the Windows Device Manager. Scroll down to Ports (Com & LPT) and Double-Click on "Prolific USB-to-Serial Comm Port (COM#)". In the Properties Window, Click on "Driver". The "Driver Version" must say "2.0.2.8" dated 11/20/07. If not, then the correct driver is not installed. Unplug the USB-To-Serial adapter and run the "PL2303_Driver_Installer.exe" again, following the directions precisely until the correct driver appears in the Device Manager.

Removal: The Prolific 32-bit PL-2303_Driver_Installer is a "Program" file and therefore installed to your computer and must stay. If you want to delete it, use the Windows "Uninstall a Program" or "Add or Remove Program" feature, however this will also uninstall the driver itself.

Windows 32-bit Files Direct Install:
For XP, Vista, and Windows 7 users that cannot get the driver files installed using the PL-2303_Driver_Installer above, you can also try having Windows install the files directly. Download and run the file "PL2303Win32.exe" installer program from the link below:

http://www.ifamilysoftware.com/Drivers/PL-2303_Win32.exe

This will install the driver files in a directory on your hard drive named, "C:\PL2303Win32". Plug in your Prolific USB to Serial adapter, go into Device Manger, and then Scroll down to Ports (Com & LPT) and Double-Click on "Prolific USB-to-Serial Comm Port (COM#)", or in some cases, "Other Device". Click on "Update Driver' and then choose to locate the driver on your computer. Click on "Browse" and direct Windows to the folder "C:\PL2303Win32" where the files are located and click OK. Windows will copy the files directly into the Driver Store for the system to use. Next, unplug your Prolific adapter and plug it in again and the driver will be installed from the Driver Store to Windows/System32/Drivers and your device should be working properly.

Key words: Sabrent, SBT-USC1M, Prolific, PL-2303, PL2303, USB to Serial, VID_067B, PID_2303, Code 10 

