# Wim Integrator
Since Microsoft always release every version system in seprated iso files and take large amount space.
I build this tool for integrate them in one single wim file. (in wim file, same data only count once)

# How to use
Simply extract all iso, select the folder contains all extracted iso content. Then hit [Integrate] button, chose a folder for save integrated wim file. (make sure have enough space)

Note: you need a imagex.exe put in the same folder contains wim_integrator.exe

# ToDo
- [x] be able to select mount point
- [x] be able to select imagex temp folder
- [x] be able to change compresson level
- [x] be able to change default search file name (default: *.wim)
- [x] be able to delete remove unwanted volumes from list before integration
- [ ] be able to filter out small wim files (eg: boot.wim)
- [x] status bar for display imagex output
- [x] move search wim to thread
- [ ] implement more error handlers

# Screenshot

Folder selected

![wim_integrator_0](https://raw.githubusercontent.com/424778940z/wim_integrator/master/screenshot/wim_integrator_0.png)

Mount volume (Gold)

![wim_integrator_1](https://raw.githubusercontent.com/424778940z/wim_integrator/master/screenshot/wim_integrator_1.png)

Imagex running (Aqua)

![wim_integrator_2](https://raw.githubusercontent.com/424778940z/wim_integrator/master/screenshot/wim_integrator_2.png)

Umount volume (DodgerBlue)

![wim_integrator_3](https://raw.githubusercontent.com/424778940z/wim_integrator/master/screenshot/wim_integrator_3.png)

Succeed (Lime), if fail (Red)

![wim_integrator_4](https://raw.githubusercontent.com/424778940z/wim_integrator/master/screenshot/wim_integrator_4.png)

Finished wim

![finished_wim](https://raw.githubusercontent.com/424778940z/wim_integrator/master/screenshot/finished_wim.png)