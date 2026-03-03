# SETUP

## Team Fortress 2 Launch Properties
Add the following commands to your Team Fortress 2 launch properties:

**-condebug**: Generates the console.log file in the /tf/ folder of Team Fortress 2 that is read for events when launching Team Fortress 2.

**-conclearlog**: Removes the previous console.log file when launching Team Fortress 2 to prevent excessive file size.

Launch properties for Team Fortress 2 can be found by:
1. Open **Steam**.
2. Open the **Library** tab.
3. Right Click **Team Fortress 2**.
4. Select **Properties**.
5. In the **General** tab, scroll down to Launch Options.
6. Add the commands to the **Launch Options** text box.

<img width="840" height="596" alt="image" src="https://github.com/user-attachments/assets/52c21111-09fc-4f45-8799-75224764e56a" />

## Team Fortress 2 /tf/ Path

Add your /tf/ folder path for Team Fortress 2 to the LovenseFortress app.

The Team Fortress 2 /tf/ folder can be found by:
1. Open **Steam**.
2. Open the **Library** tab.
3. Right Click **Team Fortress 2**,
4. Select **Manage**.
5. Select **Browse local files**.
6. Open the **tf** folder.

Copy the path for the /tf/ folder and add it to the LovenseFortress app in the **Config** tab & press **Set** to update the path.

## Lovense Remote Linking
The Lovense Remote mobile app is required for connecting & activating your toys through LovenseFortress.

The Lovense Remote app can be set up by:
1. Download the **Lovense Remote** app found on the Android and iOS app store.
2. Connect all toys intended to be used to the **Lovense Remote** app.
3. Open the **Discover** tab on the bottom of the app.
4. Scroll down & select **Game Mode**.
5. Toggle the **Enable LAN** button to on.
6. Copy the **Local IP** to the LovenseFortress app in the Config tab.
7. Copy the **Port** to the Lovense Fortress app in the Config tab.

**It is recommended to set your mobile device to use a static IP to prevent having to update your local ip that the Lovense Remote app will be showing. Instructions for setting a static IP on your mobile device can be found online.**

Once the **Local IP** & **Port** for the Lovense Remote app have been set in the LovenseFortress app, you can press the **Test Lovense** button to ensure the application is connected.

## Customizing Team Fortress 2 Binds
It is recommended to bind the following commands to your Team Fortress 2 client for maximum pleasure.

Type the following in the Team Fortress 2 developer console:

**bind key "echo SteamDisplayName lovense"**

- Replace **key** with the key you would like to have this trigger.
- Replace **SteamDisplayName** with your actual Steam display name.
- This will send an echo command to the developer console that is ultimately output to the console.log file & triggers your Lovense toy on demand.
- For example, **bind mouse3 "echo SmoothDagger lovense"**

**bind key explode**

- Replace **key** with the key you would like to have this trigger.
- This will trigger an explosion suicide in Team Fortress 2, ultimately triggering your Lovense toy.
- For example, **bind p explode**

**bind key kill**

- Replace **key** with the key you would like to have this trigger.
- This will trigger a ragdoll suicide in Team Fortress 2, ultimately triggering your Lovense toy.
- For example, **bind o kill**

Enable the Team Fortress 2 developer console by the following:
1. Open **Options**.
2. Select **Advanced...**
3. Check the **Enable developer console** box.
4. Hit the **~** key to open the developer console.
5. Enter the above commands.
