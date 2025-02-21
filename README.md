# RestartUtility
Restart button alternative used in environment like VDI desktops where policy to shutdown the system is not allowed to the users.
Specifically when the following policy is configured only for administrators, the Shutdown and Restart buttons will be hidden from the users.
This utility serves as the alternative app for allowing the users to restart the system but not shutdown. 

```
Computer Configuration->Windows Settings->Security Settings->Local Policies->User Rights Assignment->Shut down the system
```
![RestartUtility](https://github.com/legoj/RestartUtility/blob/main/screenshot.JPG)

Sample WIX build configuration file is included to create the MSI installer package for deployment to these locked-down environments. It also includes an external resource file to localize the UI text to Japanese and can be extended to other languages if necessary. 
